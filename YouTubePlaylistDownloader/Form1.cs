using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaToolkit;
using MediaToolkit.Model;
using Newtonsoft.Json;
using VideoLibrary;
using Mp3Lib;
using System.Drawing;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string playListId;
        int errors;
        int done;
        bool stop = false;

        private async void startBtn_Click(object sender, EventArgs e)
        {
            if(startBtn.Text == "Stop")
            {
                stop = true;
                return;
            }
            try
            {

                playListId = linkTextbox.Text.Replace("https://www.youtube.com/playlist?list=", "");

                var theresMore = false;
                string nextPageToken = null;
                var pageNumber = 0;
                const int MaxResults = 50;

                do
                {
                    var task = GetVideosInPlaylistAsync(playListId, nextPageToken);
                    var result = await task.ConfigureAwait(false);

                    if (result.pageInfo.totalResults == 0)
                    {
                        Console.WriteLine("No videos found in playlist.");
                        return;
                    }

                    pageNumber++;
                    nextPageToken = result.nextPageToken;
                    theresMore = (nextPageToken != null);
                    var from = (pageNumber - 1) * MaxResults + 1;
                    var to = from + result.items.Count - 1;

                    SetControlPropertyValue(progressBar, "maximum", (int)result.pageInfo.totalResults);
                    SetControlPropertyValue(progressBar, "value", 0);
                    SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                    SetControlPropertyValue(startBtn, "text", "Stop");

                    CreateFiles(result, from, to, convertCheckbox.Checked, pathTextbox.Text);
                } while (theresMore);
                MessageBox.Show($"All Videos, except for {errors}, have been successfully downloaded!");
            }
            catch (AggregateException agg)
            {
                foreach (var j in agg.Flatten().InnerExceptions)
                    Console.WriteLine(j.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SetControlPropertyValue(progressBar, "maximum", 0);
                SetControlPropertyValue(progressBar, "value", 0);
                SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                SetControlPropertyValue(startBtn, "text", "Start");
            }
        }

        private void UpdateFilename(string pathToFile)
        {
            string[] lines = File.ReadAllLines(Application.StartupPath + "/strings.txt");
            string newPathToFile = pathToFile;
            foreach (string line in lines)
            {
                if (newPathToFile.Contains(line))
                    newPathToFile = newPathToFile.Replace(line, "");
            }
            File.Move(pathToFile, newPathToFile);
        }

        private void UpdateID3Tags(string pathToFile)
        {
            try
            {
                Mp3File file = new Mp3File(pathToFile);
                file.TagHandler.Album = albumTextbox.Text;
                file.TagHandler.Artist = artistTextbox.Text;
                file.TagHandler.Year = yearTextbox.Text;
                file.TagHandler.Picture = albumPicture.Image;
                try
                {
                    file.Update();
                }
                catch
                {
                    file.Update();
                }
                pathToFile = pathToFile.Replace("\\", "/");
                string fileToDelete = pathToFile.Replace(".mp3", ".bak");
                if (File.Exists(fileToDelete))
                    File.Delete(fileToDelete);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                errors++;
                done++;
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = downloadPathDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                pathTextbox.Text = downloadPathDialog.SelectedPath;
            }
        }

        delegate void SetControlValueCallback(Control control, string propName, object propValue);
        private void SetControlPropertyValue(Control control, string propName, object propValue)
        {
            if (control.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                control.Invoke(d, new object[] { control, propName, propValue });
            }
            else
            {
                Type t = control.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach(PropertyInfo p in props)
                {
                    if(p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(control, propValue, null);
                    }
                }

            }
        }

        private void CreateFiles(dynamic result, int from, int to, bool convert, string pathToFolder)
        {
            if (result.items.Count > 0)
            {
                var exceptionMessage = string.Format($"The value of the argument '{nameof(from)}' must be less than or the same as '{nameof(to)}'.");

                if (from > to) throw new ArgumentOutOfRangeException(exceptionMessage);

                var i = from;
                SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");

                foreach (var item in result.items)
                {
                    if (stop)
                        return;

                    try
                    {
                        var youtube = YouTube.Default;
                        string id = item.snippet.resourceId.videoId;
                        Console.WriteLine(item.snippet.title);
                        var vid = youtube.GetVideo("https://www.youtube.com/watch?v=" + id);
                        string pathToMP4File = Path.Combine(pathToFolder, vid.FullName);
                        byte[] bytes = vid.GetBytes();
                        File.WriteAllBytes(pathToMP4File, vid.GetBytes());

                        if (convert)
                        {
                            MediaFile inputFile = new MediaFile { Filename = pathToMP4File };
                            MediaFile outputFile = new MediaFile { Filename = $"{ pathToMP4File.Replace(".mp4", ".mp3") }" };

                            using (Engine engine = new Engine())
                            {
                                engine.GetMetadata(inputFile);
                                engine.Convert(inputFile, outputFile);
                            }

                            string pathToMP3File = outputFile.Filename;

                            pathToMP3File = pathToMP3File.Replace("_", "");
                            pathToMP3File = pathToMP3File.Replace(" - YouTube", "");
                            pathToMP3File = pathToMP3File.Replace("\\", "/");

                            File.Move(outputFile.Filename, pathToMP3File);
                            File.Delete(pathToMP4File);

                            UpdateID3Tags(pathToMP3File);
                            UpdateFilename(pathToMP3File);
                        }
                        
                    }
                    catch
                    {
                        errors++;
                        continue;
                    }
                    finally
                    {
                        done++;
                        SetControlPropertyValue(progressBar, "value", done);
                        SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                    }


                }
            }
        }

        private static async Task<dynamic> GetVideosInPlaylistAsync(string playlistId, string nextPageToken)
        {
            var parameters = new Dictionary<string, string>
            {
                ["key"] = ConfigurationManager.AppSettings["APIKey"],
                ["playlistId"] = playlistId,
                ["part"] = "snippet",
                ["fields"] = "nextPageToken, pageInfo, items/snippet(title, resourceId/videoId)",
                ["maxResults"] = "50"
            };

            if (!string.IsNullOrEmpty(nextPageToken))
                parameters.Add("pageToken", nextPageToken);

            var baseUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
            var fullUrl = MakeUrlWithQuery(baseUrl, parameters);

            var result = await new HttpClient().GetStringAsync(fullUrl);

            if (result != null)
            {
                return JsonConvert.DeserializeObject(result);
            }

            return default(dynamic);
        }

        private static string MakeUrlWithQuery(string baseUrl,
            IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (parameters == null || parameters.Count() == 0)
                return baseUrl;

            return parameters.Aggregate(baseUrl,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }

        private void browseImageBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = imageDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                albumPicture.Image = Image.FromFile(imageDialog.FileName);
            }
        }
    }
}

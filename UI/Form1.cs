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
using System.Diagnostics;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string playListId;

        private async void startBtn_Click(object sender, EventArgs e)
        {
            playListId = linkTextbox.Text.Replace("https://www.youtube.com/playlist?list=", "");
            try
            {
                progressBar.Value = 0;
                startBtn.Text = "Stop";
                var task = GetVideosInPlaylistAsync(playListId);
                var result = await task.ConfigureAwait(false);
                CreateFiles(result, convertCheckbox.Checked, pathTextbox.Text, artistTextbox.Text, albumTextbox.Text, yearTextbox.Text, albumPicture.Image);
            }
            catch (AggregateException agg)
            {
                startBtn.Text = "Start";
                foreach (var f in agg.Flatten().InnerExceptions)
                    Console.WriteLine(f.Message);
                MessageBox.Show("Oops Something went wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception g)
            {
                startBtn.Text = "Start";
                Console.WriteLine(g.Message);
                MessageBox.Show("Oops Something went wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CreateFiles(dynamic result, bool convert, string source, string artist, string album, string year, Image picture)
        {
            if (result.items.Count > 0)
            {
                int errors = 0;
                int done = 0;

                SetControlPropertyValue(progressBar, "maximum", result.items.Count);
                SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                foreach (var item in result.items)
                {
                    try
                    {
                        var youtube = YouTube.Default;
                        string id = item.snippet.resourceId.videoId;
                        Console.WriteLine(item.snippet.title);
                        var vid = youtube.GetVideo("https://www.youtube.com/watch?v=" + id);
                        string path = Path.Combine(source, vid.FullName);
                        File.WriteAllBytes(path, vid.GetBytes());

                        var inputFile = new MediaFile { Filename = path };
                        var outputFile = new MediaFile { Filename = $"{path.Replace(".mp4", "")}.mp3" };
                        if (convert)
                        {

                            using (var engine = new Engine())
                            {
                                engine.GetMetadata(inputFile);

                                engine.Convert(inputFile, outputFile);
                            }
                            string pathToFile = outputFile.Filename;
                            pathToFile = pathToFile.Replace("_", "");
                            pathToFile = pathToFile.Replace(" - YouTube", "");
                            pathToFile = pathToFile.Replace("\\", "/");
                            File.Move(outputFile.Filename, pathToFile);
                            File.Delete(path);
                            Mp3File file = new Mp3File(pathToFile);
                            file.TagHandler.Album = "test";
                            file.TagHandler.Artist = "test";
                            file.TagHandler.Year = "2019";
                            file.TagHandler.Picture = picture;
                            file.Update();
                            
                        }
                        

                        done += 1;
                        SetControlPropertyValue(progressBar, "value", done);
                        SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum.ToString() + " Done"); 
                    }


                    catch
                    {
                        errors++;
                        continue;
                    }
                    finally
                    {
                        if (done == result.items.Count)
                        {
                            SetControlPropertyValue(startBtn, "text", "Start");
                            MessageBox.Show("All Videos, except for " + errors + ", were downloaded succesfully!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private static async Task<dynamic> GetVideosInPlaylistAsync(string playListId)
        {
            var parameters = new Dictionary<string, string>
            {
                ["key"] = ConfigurationManager.AppSettings["APIKey"],
                ["playlistId"] = playListId,
                ["part"] = "snippet",
                ["fields"] = "pageInfo, items/snippet(title, resourceId/videoId)",
                ["maxResults"] = "50"
            };

            var baseUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
            var fullUrl = makeUrlWithQuery(baseUrl, parameters);

            var result = await new HttpClient().GetStringAsync(fullUrl);

            if (result != null)
            {
                return JsonConvert.DeserializeObject(result);
            }

            return default(dynamic);
        }

        private static string makeUrlWithQuery(string baseUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return parameters.Aggregate(baseUrl, (accumalated, kvp) => string.Format($"{accumalated}{kvp.Key}={kvp.Value}&"));
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

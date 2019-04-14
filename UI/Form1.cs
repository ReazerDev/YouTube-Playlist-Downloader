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
        List<string> paths;
        int errors;
        int done;
        bool stop = false;

        private async void startBtn_Click(object sender, EventArgs e)
        {
            var task = GetVideosInPlaylistAsync(playListId);
            if (startBtn.Text == "Stop")
            {
                stop = true;
                SetControlPropertyValue(startBtn, "text", "Start");
            }
            playListId = linkTextbox.Text.Replace("https://www.youtube.com/playlist?list=", "");
            try
            {
                progressBar.Value = 0;
                startBtn.Text = "Stop";
                var result = await task.ConfigureAwait(false);
                CreateFiles(result, convertCheckbox.Checked, pathTextbox.Text, artistTextbox.Text, albumTextbox.Text, yearTextbox.Text, albumPicture.Image);
                AddIDTags(paths, pathTextbox.Text, artistTextbox.Text);
                MessageBox.Show("All Files, except for " + errors + ", were successfully downloaded", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetControlPropertyValue(progressBar, "value", 0);
            }
            catch (AggregateException agg)
            {
                SetControlPropertyValue(startBtn, "text", "Start");
                foreach (var f in agg.Flatten().InnerExceptions)
                    Console.WriteLine(f.Message);
                MessageBox.Show("Oops Something went wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception g)
            {
                SetControlPropertyValue(startBtn, "text", "Start");
                Console.WriteLine(g.Message);
                MessageBox.Show("Oops Something went wrong!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddIDTags(List<string> paths, string pathToFile, string artist)
        {
            done = 0;
            SetControlPropertyValue(progressBar, "value", 0);
            SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");

            foreach (string path in paths)
            {
                try
                {
                    Mp3File file = new Mp3File(path);
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
                    #region
                    string newPath = path;

                    if (newPath.Contains("OFFICIAL VIDEO"))
                    {
                        newPath = newPath.Replace("OFFICIAL VIDEO", "");
                    }

                    if (newPath.Contains("(Official Video)"))
                    {
                        newPath = newPath.Replace("(Official Video)", "");
                    }

                    if (newPath.Contains("[Official Video]"))
                    {
                        newPath = newPath.Replace("[Official Video]", "");
                    }

                    if (newPath.Contains("(Official Music Video)"))
                    {
                        newPath = newPath.Replace("(Official Music Video)", "");
                    }

                    if (newPath.Contains("Official Video"))
                    {
                        newPath = newPath.Replace("Official Video", "");
                    }

                    if (newPath.Contains("Official Music Video"))
                    {
                        newPath = newPath.Replace("Official Music Video", "");
                    }

                    if (newPath.Contains("Music Video"))
                    {
                        newPath = newPath.Replace("Music Video", "");
                    }



                    if (newPath.Contains("(Lyrics)"))
                    {
                        newPath = newPath.Replace("(Lyrics)", "");
                    }

                    if (newPath.Contains("(lyrics"))
                    {
                        newPath = newPath.Replace("(lyrics)", "");
                    }

                    if (newPath.Contains("Lyrics"))
                    {
                        newPath = newPath.Replace("Lyrics", "");
                    }

                    if (newPath.Contains("Lyric Video"))
                    {
                        newPath = newPath.Replace("Lyric Video", "");
                    }

                    if (newPath.Contains("(Lyric Video)"))
                    {
                        newPath = newPath.Replace("(Lyric Video)", "");
                    }



                    if (newPath.Contains("[Audio]"))
                    {
                        newPath = newPath.Replace("[Audio]", "");
                    }

                    if (newPath.Contains("(AUDIO)"))
                    {
                        newPath = newPath.Replace("(AUIDO)", "");
                    }

                    if (newPath.Contains("(official audio)"))
                    {
                        newPath = newPath.Replace("(official audio)", "");
                    }

                    if (newPath.Contains("(Official Audio)"))
                    {
                        newPath = newPath.Replace("(Official Audio)", "");
                    }

                    if (newPath.Contains("[Official Audio]"))
                    {
                        newPath = newPath.Replace("[Official Audio]", "");
                    }



                    if (newPath.Contains("[HQ]"))
                    {
                        newPath = newPath.Replace("[HQ]", "");
                    }

                    File.Move(path, newPath);
                    #endregion
                    pathToFile = pathToFile.Replace("\\", "/");
                    string fileToDelete = path.Replace(".mp3", ".bak");
                    if (File.Exists(fileToDelete))
                        File.Delete(fileToDelete);

                    done++;
                    SetControlPropertyValue(progressBar, "value", done);
                    SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    errors++;
                    done++;
                    SetControlPropertyValue(progressBar, "value", done);
                    SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                }
                
                
                
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
                SetControlPropertyValue(progressBar, "maximum", result.items.Count);
                SetControlPropertyValue(progressLabel, "text", progressBar.Value.ToString() + "/" + progressBar.Maximum + " Done");
                paths = new List<string>();
                foreach (var item in result.items)
                {
                    if (stop)
                    {
                        break;
                    }
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
                            paths.Add(pathToFile);
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

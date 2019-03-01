using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VideoLibrary;
using ReazerJSON;
using MediaToolkit;
using MediaToolkit.Model;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Youtube
{
    public partial class Youtube
    {
        static void Main(string[] args)
        {
            JSONParser parser = new JSONParser();
            string playListId =  parser.loadString("link", "settings.json");
            playListId = playListId.Replace("https://www.youtube.com/playlist?list=", "");
            try
            {
                var result = GetVideosInPlaylistAsync(playListId).Result;
                PrintResult(result);
            }
            catch(AggregateException agg)
            {
                foreach (var e in agg.Flatten().InnerExceptions)
                Console.WriteLine(e.Message);
                Console.Read();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }

            Console.ReadKey();
        }

        private static void PrintResult(dynamic result)
        {
            if (result.items.Count > 0)
            {
                JSONParser parser = new JSONParser();
                string source = parser.loadString("path", "settings.json");

                int errors = 0;
                foreach (var item in result.items)
                {
                    try
                    {


                        var youtube = YouTube.Default;
                        string id = item.snippet.resourceId.videoId;
                        Console.WriteLine(item.snippet.title);
                        Console.WriteLine("Downloading...");
                        var vid = youtube.GetVideo("https://www.youtube.com/watch?v=" + id);
                        string path = Path.Combine(source, vid.FullName);
                        File.WriteAllBytes(path, vid.GetBytes());

                        var inputFile = new MediaFile { Filename = path };
                        var outputFile = new MediaFile { Filename = $"{path.Replace(".mp4", "")}.mp3" };
                        JSONParser boolLoader = new JSONParser();
                        bool convert = Convert.ToBoolean(boolLoader.loadInt("convert", "settings.json"));
                        if (convert)
                        {
                            Console.WriteLine("Converting...");

                            using (var engine = new Engine())
                            {
                                engine.GetMetadata(inputFile);

                                engine.Convert(inputFile, outputFile);
                            }
                            File.Delete(path);
                        }
                        Console.WriteLine("Finishing...");
                        Console.WriteLine();
                    }
                        
                    
                    catch
                    {
                        errors++;
                        Console.WriteLine("There was a problem with this Video!");
                        Console.WriteLine();
                        continue;
                    }
                }
                Console.WriteLine("Done. {0} Videos didn't download!", errors);
                Console.WriteLine("Press any Key to close...");
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

            if(result != null)
            {
                return JsonConvert.DeserializeObject(result);
            }

            return default(dynamic);
        }

        private static string makeUrlWithQuery(string baseUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return parameters.Aggregate(baseUrl, (accumalated, kvp) => string.Format($"{accumalated}{kvp.Key}={kvp.Value}&"));
        }
    }
}
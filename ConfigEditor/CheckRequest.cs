using System;
using System.Collections.Generic;
using System.Net.Http;
using ConfigEditor.Logger;
using Newtonsoft.Json;
using SetupProcessor;
using SetupProcessor.Model;

namespace ConfigEditor {
    public static class CheckRequest {
        public static ConfigModel.IgnoreData onCheck(ConfigModel model,string ignoreVideoId) {
            HttpClient httpClient = new HttpClient();
            ParseObject parseObject;
            try {
                AlLite.WriteLine(WriteMode.INFO, $"Checking :: {ignoreVideoId}");
                string getObj = httpClient.GetStringAsync(
                    $"https://www.googleapis.com/youtube/v3/videos?" + 
                    $"id={ignoreVideoId}&key={model.API_KEY}&" + 
                    $"fields=items(id,snippet/title)&part=snippet").Result;
                parseObject = JsonConvert.DeserializeObject<ParseObject>(getObj);
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            return new ConfigModel.IgnoreData() { IgnoreDataName = parseObject.items[0].snippet.title, IgnoreVideoId = ignoreVideoId };
        }
        
        private class ParseObject {
            [JsonProperty("items")]
            public List<Item> items { get; set; } 
        }
        
        private class Item {
            [JsonProperty("id")]
            public string id { get; set; } 
            [JsonProperty("snippet")]
            public Snippet snippet { get; set; } 
        }
        
        private class Snippet {
            [JsonProperty("title")]
            public string title { get; set; } 
        }
    }
}
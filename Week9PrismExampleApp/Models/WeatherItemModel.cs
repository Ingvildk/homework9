using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
namespace Week9PrismExampleApp.Models
{
    public static class WeatherItemModel
    {
        public partial class WeatherItem
        {
			[JsonProperty("userId")]
			public int userId { get; set; }

			[JsonProperty("id")]
			public int id { get; set; }

			[JsonProperty("title")]
			public string title { get; set; }
			
            [JsonProperty("body")]
			public string body { get; set; }
        }
  
        public partial class WeatherItem
        {
            public static WeatherItem FromJson(string json) => JsonConvert.DeserializeObject<WeatherItem>(json, Converter.Settings);
        }

        public static string ToJson(this WeatherItem self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            };
        }
    }
}

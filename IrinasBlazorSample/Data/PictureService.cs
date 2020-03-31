using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using IrinasBlazorSample.Models;

namespace IrinasBlazorSample.Data
{
    public interface IPictureService
    {
        Task<GIF> GetRandomGif();
    }

    public class PictureService : IPictureService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _key;
        private readonly string _tag;
        private readonly string _type;
        private readonly string _rating;
        private readonly Random _random;

        public PictureService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetValue<string>("Gif:Url");
            _key = configuration.GetValue<string>("Gif:Key");
            _tag = configuration.GetValue<string>("Gif:Tag");
            _type = configuration.GetValue<string>("Gif:Type");
            _rating = configuration.GetValue<string>("Gif:Rating");
            _random = new Random();

        }

        public async Task<GIF> GetRandomGif()
        {
            var uri = $"{_url}?q={_type}&api_key={_key}&limit=11";
            var json = await _httpClient.GetStringAsync(uri);
            var picutres = JsonSerializer.Deserialize<GiphyReturn>(json);
            return picutres.Picutres.ElementAtOrDefault(_random.Next(0, (picutres.Picutres.Count - 1)));
        }

        class GiphyReturn
        {
            [JsonPropertyName("data")]
            public List<GIF> Picutres { get; set; }
        }

    }
}

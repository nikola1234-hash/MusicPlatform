﻿using Microsoft.Extensions.Options;
using MusicPlatform.DTO;
using MusicPlatform.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace MusicPlatform.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;
        private readonly IOptions<FmApi> _configuration;
        public ApiService(ILogger<ApiService> logger, IOptions<FmApi> configuration)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _configuration = configuration;

        }

        public async Task<ArtistResponseDto> GetArtistDetails(string artistName)
        {
            var url = $"{_configuration.Value.ApiBaseUrl}{_configuration.Value.ApiEndPoint.Replace("{0}", artistName).Replace("{1}", _configuration.Value.ApiKey)}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var artist = JsonConvert.DeserializeObject<ArtistResponseDto>(content);
                return artist;
            }
            return null;
        }



    }
}
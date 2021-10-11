using InvestCloud.App.Models;
using InvestCloud.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public class NumbersClient : INumbersClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NumbersClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ResultOfRowInt32> GetRow(string dataset, int idx)
        {
            var type = "row";
            var getPath = _configuration
                .GetValue<string>("Endpoints:Get")
                .InterpolateConvert(new { dataset, type, idx });

            return await _httpClient.GetFromJsonAsync<ResultOfRowInt32>(getPath);
        }

        public async Task<ResultOfInt32> Initialize(int size)
        {
            var initializePath = _configuration
                .GetValue<string>("Endpoints:Initialize")
                .InterpolateConvert(new { size });

            return await _httpClient.GetFromJsonAsync<ResultOfInt32>(initializePath);
        }

        public async Task<ResultOfString> Validate(string md5Hash)
        {
            var validatePath = _configuration
                .GetValue<string>("Endpoints:Validate");

            var stringPayLoad = JsonConvert.SerializeObject(md5Hash);
            var requestContent = new StringContent(stringPayLoad, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(validatePath, requestContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResultOfString>(responseContent);            
        }
    }
}

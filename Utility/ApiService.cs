using Newtonsoft.Json;
using mvc_surfboard.Models;

namespace mvc_surfboard.Utility
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://localhost:7051/api/surfboards";

        public ApiService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Surfboard>> UseApiService()
        {
            try
            {
                var response = await _httpClient.GetAsync(baseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<Surfboard>>(json);

                    return data;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Surfboard>();
            }
        }



    }
}

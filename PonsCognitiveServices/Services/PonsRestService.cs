using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace PonsCognitiveServices.Services
{
    public class PonsRestService : IPonsRestService
    {
        private string _secretKey;
        private readonly HttpClient _client;

        public PonsRestService()
        {
            _client = new HttpClient();
        }

        public void SetSecretKey(string xSecret)
        {
            _secretKey = xSecret;
            if (_client.DefaultRequestHeaders.ContainsKey("X-Secret"))
                _client.DefaultRequestHeaders["X-Secret"] = _secretKey;
            else
                _client.DefaultRequestHeaders.Add("X-Secret", _secretKey);
        }

        public async Task<string> GetRequest(Uri uri)
        {
            HttpResponseMessage responseGet = await _client.GetAsync(uri);
            return await responseGet.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}

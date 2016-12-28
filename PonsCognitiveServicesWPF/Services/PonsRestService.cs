using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Services
{
    public class PonsRestService : IPonsRestService, IDisposable
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
            if (_client.DefaultRequestHeaders.Contains("X-Secret"))
            {
                _client.DefaultRequestHeaders.Remove("X-secret");
                _client.DefaultRequestHeaders.Add("X-Secret", _secretKey);

            }
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
            }
        }
    }
}

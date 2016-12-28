using System;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Services
{
    public interface IPonsRestService
    {
        void SetSecretKey(string xSecret);
        Task<string> GetRequest(Uri uri);
        void Dispose();
    }
}

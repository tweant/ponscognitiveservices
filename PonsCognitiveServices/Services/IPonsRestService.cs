using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServices.Services
{
    public interface IPonsRestService
    {
        void SetSecretKey(string xSecret);
        Task<string> GetRequest(Uri uri);
        void Dispose();
    }
}

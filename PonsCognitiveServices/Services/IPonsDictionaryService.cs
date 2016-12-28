using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServices.Services
{
    public interface IPonsDictionaryService
    {
        Task<string> GeneratePage(Uri uri);
    }
}

using System;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Services
{
    public interface IPonsDictionaryService
    {
        Task<string> GeneratePage(Uri uri);
    }
}

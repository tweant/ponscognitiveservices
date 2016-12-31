using System;
using System.Threading.Tasks;
using PonsCognitiveServicesWPF.Model.Language;

namespace PonsCognitiveServicesWPF.Services
{
    public interface IPonsDictionaryService
    {
        Task<string> GeneratePage(Uri uri);
        Task<string> LookForQuery(Translation translation, string query);
    }
}

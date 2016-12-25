using System.Threading.Tasks;

namespace PonsCognitiveServices.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}
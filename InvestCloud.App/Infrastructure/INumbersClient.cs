using InvestCloud.App.Models;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public interface INumbersClient
    {
        Task<ResultOfInt32> Initialize(int size);
        Task<ResultOfRowInt32> GetRow(string dataset, int idx);
        Task<ResultOfString> Validate(string md5Hash);
    }
}

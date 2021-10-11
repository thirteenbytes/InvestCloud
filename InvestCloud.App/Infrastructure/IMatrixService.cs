using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public interface IMatrixService
    {
        Task Run(int matrixSize);
    }
}

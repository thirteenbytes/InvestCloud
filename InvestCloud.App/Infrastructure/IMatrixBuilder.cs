using InvestCloud.Core.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public interface IMatrixBuilder<T> where T : class
    {
        Task<T> GetMatrix(int size, string dataset);
    }
}

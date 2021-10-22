using InvestCloud.Core.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public class ParallelMatrixBuilder : IMatrixBuilder<ParallelForMatrixFunc>
    {
        private readonly INumbersClient _numbersClient;
        public ParallelMatrixBuilder(INumbersClient numbersClient)
            => _numbersClient = numbersClient;

        public Task<ParallelForMatrixFunc> GetMatrix(int size, string dataset)
        {                            
            var matrix = new ParallelForMatrixFunc(size);

            Parallel.For(0, size, i =>
            {
                var getResponse = _numbersClient.GetRow(dataset, i);
                if (getResponse.Result.Success)
                {
                    var values = getResponse.Result.Value.ToArray<int>();
                    matrix.AddRow(i, values);
                }
                else
                {
                    throw new NumbersClientException($"Get Row endpoint failed: {getResponse.Result.Cause}");
                }

            });            
            return Task.FromResult(matrix);
        }
    }
}

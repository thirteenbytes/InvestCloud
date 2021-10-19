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
        public async Task<ParallelForMatrixFunc> GetMatrix(int size, string dataset)
        {

            var initCallResponse = await _numbersClient.Initialize(size);
            if(initCallResponse.Success)
            {
                int initResponseSize = initCallResponse.Value;
                var matrix = new ParallelForMatrixFunc(size);

                for(var i = 0; i < initResponseSize; i++)
                {
                    var getResponse = await _numbersClient.GetRow(dataset, i);
                    if(getResponse.Success)
                    {
                        var values = getResponse.Value.ToArray<int>();
                        matrix.AddRow(i, values);
                    }
                    else
                    {
                        throw new NumbersClientException($"Get Row endpoint failed: {getResponse.Cause}");
                    }
                }

                return matrix;
            }            
            throw new NumbersClientException($"Init endpoint failed: {initCallResponse.Cause}");            
        }
    }
}

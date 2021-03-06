using InvestCloud.App.Models;
using InvestCloud.Core.Extensions;
using InvestCloud.Core.Matrix;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public class MathNetMatrixService : IMatrixService
    {
        private readonly ILogger<MathNetMatrixService> _logger;
        private readonly INumbersClient _numbersClient;

        public MathNetMatrixService(ILogger<MathNetMatrixService> logger, INumbersClient numbersClient)
        {
            _logger = logger;
            _numbersClient = numbersClient;
        }

        public async Task Run(int matrixSize)
        {
            try
            {

                var stopwatch = new Stopwatch();

                
                _logger.LogInformation($"(1/5) Initializing and building Squares Matrices ({matrixSize} X {matrixSize})...");

                stopwatch.Start();
                (MathNetMatrixFunc a, MathNetMatrixFunc b) = await GetMatrixAsDouble(matrixSize);
                stopwatch.Stop();

                _logger.LogInformation($"Build completed at {stopwatch.Elapsed};");

                _logger.LogInformation($"(2/5) Matrices are being multiplied...");

                stopwatch.Reset();
                stopwatch.Start();
                var c = a * b;
                stopwatch.Stop();

                _logger.LogInformation($"Calculation completed at {stopwatch.Elapsed};");

                var matrixAsString = c.ToString();
                _logger.LogInformation($"(3/5) Creating MD5 Hash from the from string;");            
                var md5 = matrixAsString.ToMD5();

                _logger.LogInformation($"(4/5) Validating MD5 Hash: {md5};");

                ResultOfString passphrase = await _numbersClient.Validate(md5);

                if (passphrase.Success)
                {
                    _logger.LogInformation($"(5/5) Passphrase: {passphrase.Value} .");
                }
                else
                {
                    throw new NumbersClientException($"Validate error occurred: {passphrase.Cause}!");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during Matrix Service Run: {ex.Message}!");
            }

        }

        private async Task<(MathNetMatrixFunc, MathNetMatrixFunc)> GetMatrixAsDouble(int size)
        {
            MathNetMatrixFunc matrixA = null; MathNetMatrixFunc matrixB = null;

            var initResponse = await _numbersClient.Initialize(size);
            if (initResponse.Success)
            {
                int initSize = initResponse.Value;
                matrixA = new MathNetMatrixFunc(initSize);
                matrixB = new MathNetMatrixFunc(initSize);

                for (int i = 0; i < initSize; i++)
                {
                    var getResponseA = await _numbersClient.GetRow("A", i);
                    if (getResponseA.Success)
                    {
                        var values = getResponseA.Value.ToArray<int>();

                        List<double> numbers = values.Select(x => (double)x).ToList();
                        matrixA.AddRow(i, numbers.ToArray());
                    }
                    else
                    {
                        throw new NumbersClientException($"Get Row endpoint failed: {getResponseA.Cause}");
                    }

                    var getResponseB = await _numbersClient.GetRow("B", i);
                    if (getResponseB.Success)
                    {
                        var values = getResponseB.Value.ToArray<int>();
                        List<double> numbers = values.Select(x => (double)x).ToList();
                        matrixB.AddRow(i, numbers.ToArray());
                    }
                    else
                    {
                        throw new NumbersClientException($"Get Row endpoint failed: {getResponseB.Cause}");
                    }
                }
            }
            else
            {
                throw new NumbersClientException($"Init endpoint failed: {initResponse.Cause}");
            }

            return (matrixA, matrixB);

        }
    }
}

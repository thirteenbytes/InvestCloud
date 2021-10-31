using InvestCloud.App.Models;
using InvestCloud.Core.Extensions;
using InvestCloud.Core.Matrix;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud.App.Infrastructure
{
    public class ParallelMatrixService : IMatrixService
    {
        private readonly ILogger<ParallelMatrixService> _logger;
        private readonly INumbersClient _numbersClient;

        public ParallelMatrixService(ILogger<ParallelMatrixService> logger, INumbersClient numbersClient)
        {
            _logger = logger;
            _numbersClient = numbersClient;
        }

        public async Task Run(int matrixSize)
        {
            try
            {
                var usecaseWatch = new Stopwatch();
                var taskWatch = new Stopwatch();

                usecaseWatch.Start();                
                _logger.LogInformation($"(1/5) Initializing and building Squares Matrices ({matrixSize} X {matrixSize})...");

                taskWatch.Start();
                (ParallelMatrixFunc a, ParallelMatrixFunc b) = await GetMatrices(matrixSize);
                taskWatch.Stop();

                _logger.LogInformation($"Build completed, task duration {taskWatch.Elapsed} total time: {usecaseWatch.Elapsed};");

                _logger.LogInformation($"(2/5) Matrices are being multiplied...");

                taskWatch.Reset();
                taskWatch.Start();
                var c = a * b;
                taskWatch.Stop();

                _logger.LogInformation($"Calculation completed at {taskWatch.Elapsed} total time {usecaseWatch.Elapsed};");

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
                usecaseWatch.Stop();
                _logger.LogInformation($"Final time {usecaseWatch.Elapsed}.");                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during Matrix Service Run: {ex.Message}!");
            }

        }

        private async Task<(ParallelMatrixFunc, ParallelMatrixFunc)> GetMatrices(int size)
        {
            var initCallResponse = await _numbersClient.Initialize(size);
            if (initCallResponse.Success)
            {
                var matrixBuilder = new ParallelMatrixBuilder(_numbersClient);
                var matrixA = await matrixBuilder.GetMatrix(size, "A");
                var matrixB = await matrixBuilder.GetMatrix(size, "B");

                return (matrixA, matrixB);
            }
            throw new NumbersClientException($"Init endpoint failed: {initCallResponse.Cause}");
        }
    }
}

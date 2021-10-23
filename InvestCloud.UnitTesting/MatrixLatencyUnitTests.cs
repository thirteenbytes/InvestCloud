using InvestCloud.Core.Matrix;
using System;
using Xunit;

namespace InvestCloud.UnitTesting
{
    public class MatrixLatencyUnitTests
    {
        private int matrixSize = 1000;
        
        [Fact]
        public void Sequence_Multiple_Matrices()
        {        
            
            var a = new SequenceMatrixFunc(matrixSize);
            var b = new SequenceMatrixFunc(matrixSize);
            for (int r=0; r < matrixSize; r++)
            {
                a.AddRow(r, GetRandomValuesAsInt32Row(matrixSize));
                b.AddRow(r, GetRandomValuesAsInt32Row(matrixSize));
            }

            var c = a * b;
        }
        

        [Fact]
        public void MathNet_Multiple_Matrices()
        {

            var a = new MathNetMatrixFunc(matrixSize);
            var b = new MathNetMatrixFunc(matrixSize);
            for (int r = 0; r < matrixSize; r++)
            {
                a.AddRow(r, GetRandomValuesAsDoubleRow(matrixSize));
                b.AddRow(r, GetRandomValuesAsDoubleRow(matrixSize));
            }

            var c = a * b;
        }

        [Fact]
        public void ParallelFor_Multiple_Matrices()
        {

            var a = new ParallelMatrixFunc(matrixSize);
            var b = new ParallelMatrixFunc(matrixSize);
            for (int r = 0; r < matrixSize; r++)
            {
                a.AddRow(r, GetRandomValuesAsInt32Row(matrixSize));
                b.AddRow(r, GetRandomValuesAsInt32Row(matrixSize));
            }

            var c = a * b;
        }

        private int[] GetRandomValuesAsInt32Row(int size)
        {
            var random = new Random();
            var row = new int[size];
            for(int col = 0; col < size; col++)
            {
                row[0] = random.Next(-10, 10);
            }

            return row;
        }

        private double[] GetRandomValuesAsDoubleRow(int size)
        {
            var random = new Random();
            var row = new double[size];
            for (int col = 0; col < size; col++)
            {
                row[0] = random.NextDouble() * (10 - (-10)) + -10;
            }

            return row;
        }


    }
}

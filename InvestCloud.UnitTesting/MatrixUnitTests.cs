using InvestCloud.Core.Matrix;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InvestCloud.UnitTesting
{
    public class MatrixUnitTests
    {

        [Fact]
        public void Sequence_Multiple_2x2_Matrices()
        {
            // Create 2 x 2 Matrix A
            var a = new SequenceMatrixFunc(2);
            var row0 = new List<int> { 2, 3 };
            var row1 = new List<int> { 1, 4 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());

            // Create 2 x 2 Matrix B
            var b = new SequenceMatrixFunc(2);
            row0 = new List<int> { 3, 2 };
            row1 = new List<int> { 1, -6 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "9-147-22";

            Assert.Equal(expectedResult, actualResult);
        }

        
        [Fact]
        public void Sequence_Multiple_3x3_Matrices()
        {
            // Create 2 x 2 Matrix A
            var a = new SequenceMatrixFunc(3);
            var row0 = new List<int> { 22, 8, -4 };
            var row1 = new List<int> { -1, 1, 34 };
            var row2 = new List<int> { 19, 0, 1 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());
            a.AddRow(2, row2.ToArray());

            // Create 2 x 2 Matrix B
            var b = new SequenceMatrixFunc(3);
            row0 = new List<int> { 14, -1, 0 };
            row1 = new List<int> { 33, -4, 2 };
            row2 = new List<int> { 9, -1, 1 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());
            b.AddRow(2, row2.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "536-5012325-3736275-201";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void MathNet_Multiple__2x2_Matrices()
        {
            // Create 2 x 2 Matrix A
            var a = new MathNetMatrixFunc(2);
            var row0 = new List<double> { 2.0, 3.0 };
            var row1 = new List<double> { 1.0, 4.0 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());

            // Create 2 x 2 Matrix B
            var b = new MathNetMatrixFunc(2);
            row0 = new List<double> { 3.0, 2.0 };
            row1 = new List<double> { 1.0, -6.0 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "9-147-22";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void MathNet_Multiple_3x3_Matrices()
        {

            // Create 2 x 2 Matrix A
            var a = new MathNetMatrixFunc(3);
            var row0 = new List<double> { 22.0, 8.0, -4.0 };
            var row1 = new List<double> { -1.0, 1.0, 34.0 };
            var row2 = new List<double> { 19.0, 0.0, 1.0 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());
            a.AddRow(2, row2.ToArray());

            // Create 2 x 2 Matrix B
            var b = new MathNetMatrixFunc(3);
            row0 = new List<double> { 14.0, -1.0, 0.0 };
            row1 = new List<double> { 33.0, -4.0, 2.0 };
            row2 = new List<double> { 9.0, -1.0, 1.0 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());
            b.AddRow(2, row2.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "536-5012325-3736275-201";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ParallelFor_Multiple_2x2_Matrices()
        {
            // Create 2 x 2 Matrix A
            var a = new ParallelMatrixFunc(2);
            var row0 = new List<int> { 2, 3 };
            var row1 = new List<int> { 1, 4 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());

            // Create 2 x 2 Matrix B
            var b = new ParallelMatrixFunc(2);
            row0 = new List<int> { 3, 2 };
            row1 = new List<int> { 1, -6 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "9-147-22";

            Assert.Equal(expectedResult, actualResult);
        }



        [Fact]
        public void ParallelFor_Multiple_3x3_Matrices()
        {
            // Create 2 x 2 Matrix A
            var a = new ParallelMatrixFunc(3);
            var row0 = new List<int> { 22, 8, -4 };
            var row1 = new List<int> { -1, 1, 34 };
            var row2 = new List<int> { 19, 0, 1 };

            a.AddRow(0, row0.ToArray());
            a.AddRow(1, row1.ToArray());
            a.AddRow(2, row2.ToArray());

            // Create 2 x 2 Matrix B
            var b = new ParallelMatrixFunc(3);
            row0 = new List<int> { 14, -1, 0 };
            row1 = new List<int> { 33, -4, 2 };
            row2 = new List<int> { 9, -1, 1 };

            b.AddRow(0, row0.ToArray());
            b.AddRow(1, row1.ToArray());
            b.AddRow(2, row2.ToArray());

            // Multiple A * B
            var c = a * b;

            var actualResult = c.ToString();
            string expectedResult = "536-5012325-3736275-201";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ToStringTest()
        {
            int[,] data = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
           
            var q = data.GetUpperBound(0) + 1 ;

            var a = Enumerable.Range(0, data.GetUpperBound(0) + 1);

            var result = string.Concat(Enumerable
                .Range(0, data.GetUpperBound(0) + 1)
                .Select(x => Enumerable.Range(0, data.GetUpperBound(1) + 1)
                .Select(y => data[x, y]))
                .Select(z => string.Concat(z))
                );

            Assert.Equal("123456789", result);

        }

    }
}

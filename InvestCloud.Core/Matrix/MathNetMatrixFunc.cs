using MathNet.Numerics.LinearAlgebra.Double;

namespace InvestCloud.Core.Matrix
{
    public class MathNetMatrixFunc : MatrixFuncBase<double>, IMatrix<double>
    {
        public MathNetMatrixFunc(int size) : base(size) { }

        protected override MatrixFuncBase<double> DoMultiplication(MatrixFuncBase<double> b)
        {
            
            
            var matrixA = DenseMatrix.OfArray(this.data);
            var matrixB = DenseMatrix.OfArray(b.data);

            var resultMatrix = matrixA * matrixB;

            return new MathNetMatrixFunc(this.rowTotal) { data = resultMatrix.ToArray() };
        }

    }
}

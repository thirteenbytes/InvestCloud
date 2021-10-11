namespace InvestCloud.Core.Matrix
{
    public class SequenceMatrixFunc : MatrixFuncBase<int>, IMatrix<int>
    {

        public SequenceMatrixFunc(int size) : base(size) { }
        public override MatrixFuncBase<int> DoMultiplication(MatrixFuncBase<int> b)
        {
            var resultMatrix = new SequenceMatrixFunc(this.rowTotal);

            for (int i = 0; i < resultMatrix.rowTotal; i++)
            {
                for (int j = 0; j < resultMatrix.columnTotal; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (int k = 0; k < this.columnTotal; k++)
                    {
                        resultMatrix[i, j] += this[i, k] * b[k, j];
                    }
                }
            }
            return resultMatrix;
        }
    }
}

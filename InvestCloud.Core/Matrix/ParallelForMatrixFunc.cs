using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud.Core.Matrix
{
    public class ParallelForMatrixFunc : MatrixFuncBase<int>, IMatrix<int>
    {        
        public ParallelForMatrixFunc(int size) : base(size) { }
        
        public override MatrixFuncBase<int> DoMultiplication(MatrixFuncBase<int> b)
        {
            var resultMatrix = new ParallelForMatrixFunc(base.rowTotal);
            Parallel.For(0, this.rowTotal, i =>
            {
                Parallel.For(0, b.columnTotal, j =>
                {
                    Compute(i, j, this, (ParallelForMatrixFunc)b, resultMatrix);
                });
            });

            return resultMatrix;
        }

        private void Compute(int tempRowIndex, int tempColIndex, ParallelForMatrixFunc a, ParallelForMatrixFunc b, ParallelForMatrixFunc result)
        {
            int rowIndex = tempRowIndex;
            int colIndex = tempColIndex;

            result[rowIndex, colIndex] = 0;

            int[] rowData = a.GetRow(rowIndex);
            int[] colData = b.GetColumn(colIndex);
            
            for(int i=0; i < rowData.Length; i++)
            {
                result[rowIndex, colIndex] += rowData[i] * colData[i];
            }            
        }        
    }
}

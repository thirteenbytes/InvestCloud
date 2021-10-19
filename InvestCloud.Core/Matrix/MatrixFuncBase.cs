using System.Linq;
using System.Text;

namespace InvestCloud.Core.Matrix
{
    public abstract class MatrixFuncBase<T> : IMatrix<T> where T : struct
    {
        protected readonly int rowTotal;
        public int columnTotal { get; protected set; }

        public T this[int row, int column]
        {
            get
            {
                return data[row, column];
            }
            set
            {
                data[row, column] = value;
            }
        }

        public T[,] data { get; protected set; }

        public MatrixFuncBase(int size)
        {
            rowTotal = size; columnTotal = size;
            data = new T[rowTotal, columnTotal];
        }

        public void AddRow(int rowNumber, T[] rowData)
        {
            for (int columnNumber = 0; columnNumber < columnTotal; columnNumber++)
            {
                data[rowNumber, columnNumber] = rowData[columnNumber];
            }
        }

        public T[] GetRow(int row)
        {
            T[] result = new T[columnTotal];
            for (int column = 0; column < columnTotal; column++)
            {
                result[column] = data[row, column];
            }
            return result;
        }

        public T[] GetColumn(int column)
        {
            T[] result = new T[columnTotal];
            for (int row = 0; row < rowTotal; row++)
            {
                result[row] = data[row, column];
            }
            return result;
        }
        protected abstract MatrixFuncBase<T> DoMultiplication(MatrixFuncBase<T> b);

        public static MatrixFuncBase<T> operator *(MatrixFuncBase<T> a, MatrixFuncBase<T> b)
        {
            return a.DoMultiplication(b);
        }

        public override string ToString()
        {
            var result = string.Concat(Enumerable
                .Range(0, data.GetUpperBound(0) + 1)
                .Select(x => Enumerable.Range(0, data.GetUpperBound(1) + 1)
                .Select(y => data[x, y]))
                .Select(z => string.Concat(z)));

            return result;
        }
    }
}

using System;
using System.Text;

namespace PatternRecognition.Templates
{
    public class Matrix
    {
        public double[,] matrix = null;
        public int CountColumn { get; private set; }
        public int CountRow { get; private set; }

        public Matrix(int x, int y)
        {
            matrix = new double[x, y];
            CountRow = x;
            CountColumn = y;
        }

        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        public Matrix Full(int height, int width, double x)
        {
            Matrix result = new Matrix(height, width);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = x;
                }
            return result;
        }

        //заполнение матрицы в соответствии с нормальным 
        //распределением на промежутке [lower; upper]
        public Matrix Full(int height, int width, double lower, double upper)
        {
            Matrix result = new Matrix(height, width);         
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    Random random = new Random();
                    result[i, j] = (lower + (upper - lower) * random.NextDouble());
                }
            return result;
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            for (int i = 0; i < matrix1.CountRow; i++)
                for (int j = 0; j < matrix1.CountColumn; j++)
                    matrix1[i, j] += matrix2[i, j];
            return matrix1;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            for (int i = 0; i < matrix1.CountRow; i++)
                for (int j = 0; j < matrix1.CountColumn; j++)
                    matrix1[i, j] -= matrix2[i, j];
            return matrix1;
        }

        public static Matrix operator /(Matrix matrix, int number)
        {
            for (int i = 0; i < matrix.CountRow; i++)
                for (int j = 0; j < matrix.CountColumn; j++)
                    matrix[i, j] /= number;
            return matrix;
        }

        public static Matrix operator *(double number, Matrix matrix)
        {
            for (int i = 0; i < matrix.CountRow; i++)
                for (int j = 0; j < matrix.CountColumn; j++)
                    matrix[i, j] *= number;
            return matrix;
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }

        public double Max()
        {
            double max = matrix[0, 0];
            for (int i = 0; i < CountRow; i++)
                for (int j = 0; j < CountColumn; j++)
                {
                    if (max < matrix[i, j]) max = matrix[i, j];
                }
            return max;
        }

        public Tuple<int, int> MaxPosition()
        {
            int maxi = 0;
            int maxj = 0;
            double max = matrix[0, 0];
            for (int i = 0; i < CountRow; i++)
                for (int j = 0; j < CountColumn; j++)
                {
                    if (max < matrix[i, j])
                    {
                        max = matrix[i, j];
                        maxi = i;
                        maxj = j;
                    }
                }
            return new Tuple<int, int>(maxi, maxj);
        }

        public double Average()
        {
            double avg = 0.0;
            for (int i = 0; i < CountRow; i++)
                for (int j = 0; j < CountColumn; j++)
                {
                    avg += matrix[i, j];
                }
            return avg / (CountRow * CountColumn);
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            if (matrix == null) return ret.ToString();

            for (int i = 0; i < CountRow; i++)
            {
                for (int t = 0; t < CountColumn; t++)
                {
                    ret.Append(matrix[i, t]);
                    ret.Append("\t");
                }
                ret.Append("\n");
            }
            return ret.ToString();
        }
    }
}

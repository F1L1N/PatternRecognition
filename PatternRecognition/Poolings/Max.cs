using System;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;

namespace ConvolutionalNetwork
{
    namespace Poolings
    {
        public class Max : IPooling
        {
            public double f(Matrix matrix)
            {
                return matrix.Max();
            }

            public Matrix df(Matrix matrix)
            {
                Tuple<int, int> maxPosition = matrix.MaxPosition();
                Matrix result = new Matrix(matrix.CountRow, matrix.CountColumn);
                for (int i = 0; i < result.CountRow; i++)
                    for (int j = 0; j < result.CountColumn; j++)
                    {
                        if (i == maxPosition.Item1 && j == maxPosition.Item2)
                        {
                            result[i, j] = 1;
                        }
                        else
                        {
                            result[i, j] = 0;
                        }
                    }
                return result;
            }

            public string Type()
            {
                return "Max";
            }
        }
    }
}

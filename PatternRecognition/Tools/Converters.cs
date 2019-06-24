using PatternRecognition.Templates;
using System;

namespace PatternRecognition.Tools
{
    class Converters
    {
        public Vector ToVector(Matrix[] array, int h, int w)
        {
            Vector vector = new Vector(array.Length * h * w);
            int index = 0;

            foreach (var matrix in array)
            {
                for (int x = 0; x < matrix.CountRow; x++)
                {
                    for (int y = 0; y < matrix.CountColumn; y++)
                    {
                        vector[index] = matrix[x, y];
                        index++;
                    }
                }
            }

            return vector;
        }

        public Vector ToVector(Matrix matrix)
        {
            Vector vector = new Vector(matrix.CountRow * matrix.CountColumn);
            int index = 0;

            for (int x = 0; x < matrix.CountRow; x++)
            {
                for (int y = 0; y < matrix.CountColumn; y++)
                {
                    vector[index] = matrix[x, y];
                    index++;
                }
            }

            return vector;
        }

        public Matrix[] ToMatrix(Vector vector, int d, int h, int w)
        {
            if (vector.Count != d * h * w)
            {
                throw new ArgumentException("Invalid vector's size");
            }

            Matrix[] array = new Matrix[d];
            Matrix matrix = new Matrix(h, w);
            int index = 0;

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    for (int k = 0; k < w; k++)
                    {
                        matrix[j, k] = vector[index];
                        index++;
                    }
                }
                array[i] = matrix;
            }

            return array;
        }

        public Matrix ToMatrix(Vector vector, int h, int w)
        {
            if (vector.Count != h * w)
            {
                throw new ArgumentException("Invalid vector's size");
            }

            Matrix matrix = new Matrix(h, w);
            int index = 0;

            for (int j = 0; j < h; j++)
            {
                for (int k = 0; k < w; k++)
                {
                    matrix[j, k] = vector[index];
                    index++;
                }
            }          

            return matrix;
        }
    }
}

using System;
using System.Text;

namespace PatternRecognition.Templates
{
    class Vector
    {
        public double[] vector = null;
        public int count { get; private set; }

        public Vector(int x)
        {
            vector = new double[x];
            count = x;
        }

        public double this[int x]
        {
            get { return vector[x]; }
            set { vector[x] = value; }
        }

        public static Vector operator /(Vector vector, int number)
        {
            for (int i = 0; i < vector.count; i++)
                vector[i] /= number;
            return vector;
        }

        public double Max()
        {
            double max = vector[0];
            for (int i = 0; i < count; i++)
            {
                if (max < vector[i]) max = vector[i];
            }
            return max;
        }

        public int MaxPosition()
        {
            int maxPosition = 0;
            double max = vector[0];
            for (int i = 0; i < count; i++)
            {
                if (max < vector[i])
                {
                    max = vector[i];
                    maxPosition = i;
                }
            }
            return maxPosition;
        }

        public double Average()
        {
            double avg = 0.0;
            for (int i = 0; i < count; i++)
            {
                avg += vector[i];
            }
            return avg / count;
        }

        public Vector Clone()
        {
            Vector result = new Vector(count);
            for (int i = 0; i < count; i++)
            {
                result[i] = vector[i];
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            if (vector == null) return ret.ToString();

            for (int i = 0; i < count; i++)
            {
                 ret.Append(vector[i]);
                 ret.Append("\t");
            }
            ret.Append("\n");
            return ret.ToString();
        }
    }

}

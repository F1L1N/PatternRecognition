using System.Text;

namespace PatternRecognition.Templates
{
    class Vector
    {
        public double[] vector = null;
        public int Count { get; private set; }

        public Vector(int x)
        {
            vector = new double[x];
            Count = x;
        }

        public double this[int x]
        {
            get { return vector[x]; }
            set { vector[x] = value; }
        }

        public static Vector operator /(Vector vector, int number)
        {
            for (int i = 0; i < vector.Count; i++)
                vector[i] /= number;
            return vector;
        }

        public double Max()
        {
            double max = vector[0];
            for (int i = 0; i < Count; i++)
            {
                if (max < vector[i]) max = vector[i];
            }
            return max;
        }

        public int MaxPosition()
        {
            int maxi = 0;
            double max = vector[0];
            for (int i = 0; i < Count; i++)
            {
                if (max < vector[i])
                {
                    max = vector[i];
                    maxi = i;
                }
            }
            return maxi;
        }

        public double Average()
        {
            double avg = 0.0;
            for (int i = 0; i < Count; i++)
            {
                avg += vector[i];
            }
            return avg / Count;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            if (vector == null) return ret.ToString();

            for (int i = 0; i < Count; i++)
            {
                 ret.Append(vector[i]);
                 ret.Append("\t");
            }
            ret.Append("\n");
            return ret.ToString();
        }
    }

}

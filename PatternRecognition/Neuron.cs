using ConvolutionalNetwork.Activations;
using PatternRecognition.Templates;
using static System.Math;

namespace PatternRecognition
{
    class Neuron
    {
        private NeuronType type;
        private double[] weights;
        private double[] inputs;
        private double output;
        private double derivative;

        public Neuron(double[] inputs, double[] weights, NeuronType type)
        {
            this.type = type;
            this.weights = weights;
            this.inputs = inputs;
        }

        public void Activator(double[] i, double[] w)
        {
            double sum = w[0];
            for (int l = 0; l < i.Length; ++l)
            {
                sum += i[l] * w[l + 1];
            }
            switch (type)
            {
                case NeuronType.Convolution:
                    LReLU relu = new LReLU();
                    output = relu.f(sum);
                    derivative = relu.df(sum);
                    break;
                case NeuronType.Pooling:
                    break;
                case NeuronType.FullyConnected:
                    output = Exp(sum);
                    break;
            }
        }
    }
}

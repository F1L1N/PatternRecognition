using System;
using ConvolutionalNetwork.Activations;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;

namespace PatternRecognition
{
    class Neuron
    {
        #region Variables

        private static readonly Random random = new Random();

        private IActivation activationFunction;

        public Vector inputs { get; set; }

        private int inputCount;

        private double output;

        public Vector weightFactors { get; set; }

        public double errorSignal { get; set; }

        public double deepRating { get; set; }

        #endregion

        #region Methods

        public Neuron(int count, IActivation activation, double rating = 0.3)
        {
            inputCount = count;
            weightFactors = new Vector(inputCount);
            activationFunction = activation;
            initWeightFactors();
            deepRating = rating;
        }

        private void initWeightFactors()
        {
            for (int i = 0; i < weightFactors.Count; i++)
            {
                weightFactors[i] = random.Next(-5, 5) / 10.0;
            }
        }

        public void initInputValues(Vector inputValues)
        {
            inputs = inputValues;
        }

        public void abjustWeights()
        {
            for (int i = 0; i < inputCount; i++)
            {
                weightFactors[i] += deepRating * errorSignal * inputs[i] * output * (1 - output);
            }
        }

        public virtual double Output
        {
            get
            {
                LReLU relu = new LReLU();
                output = relu.f(output);
                return output;
            }
            set
            {
                output = value;
            }
        }

        #endregion
    }
}

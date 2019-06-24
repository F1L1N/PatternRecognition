using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using System.Collections.Generic;

namespace PatternRecognition.Layers
{
    abstract class BaseLayer
    {
        private readonly int inputCount;
        private readonly int outputCount;
        private Vector inputs;
        public List<Neuron> neurons { get; set; }

        public BaseLayer(int iCount, int oCount, IActivation activationFunc)
        {
            inputCount = iCount;
            outputCount = oCount;

            GenerateNeurons(outputCount, activationFunc);
        }

        private void GenerateNeurons(int outputCount, IActivation activationFunc)
        {
            neurons = new List<Neuron>();
            for (var i = 0; i < outputCount; i++)
            {
                var n = new Neuron(inputCount, activationFunc);
                n.initInputValues(inputs);
                neurons.Add(n);
            }
        }

        public void initInputValues(Vector inputValues)
        {
            inputs = inputValues;
        }

        public Vector GetLayerOutput()
        {
            var arr = new Vector(outputCount);
            var i = 0;
            foreach (var neuron in neurons)
            {
                neuron.initInputValues(inputs);
                arr[i++] = neuron.Output;
            }
            return arr;
        }
    }
}

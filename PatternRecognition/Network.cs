using PatternRecognition.Interfaces;
using PatternRecognition.Layers;
using PatternRecognition.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognition
{
    class Network<LossFunctionType> : IDisposable where LossFunctionType : ILoss, new()
    {
        private List<BaseLayer> layers;
        
        //TODO what's wrong with this parameters
        public double eta;
        public double mu;
        public double lambda;

        public LossFunctionType LossFunction { get; } = new LossFunctionType();
        public Network()
        {
            layers = new List<BaseLayer>();
        }

        public Network(BaseLayer[] layers)
        {
            this.layers = new List<BaseLayer>(layers);
        }

        public void AddLayer(BaseLayer layer)
        {
            layers.Add(layer);
        }

        public void AddLayer(BaseLayer[] layers)
        {
            this.layers.AddRange(layers);
        }

        public void Dispose()
        {
            layers.Clear();
        }

        private Vector ForwardPropagation(Vector trainingSet)
        {
            layers[0].inputs = trainingSet;
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].ForwardPropagation();
                if (i == layers.Count - 1)
                {
                    break;
                }
                layers[i + 1].inputs = layers[i].outputs;
            }
            return layers.Last().outputs;
        }

        public double standardTraining(Vector trainingSet, Vector teachingSet)
        {
            var forwardResult = ForwardPropagation(trainingSet);

            double error = 0.0;

            var derriative = forwardResult.Clone();
            for (int i = 0; i < derriative.count; i++)
            {
                error += LossFunction.f(forwardResult[i], teachingSet[i]);
                derriative[i] = LossFunction.df(derriative[i], teachingSet[i]);
            }

            for (int i = layers.Count - 1; i >= 0; i--)
            {
                derriative = layers[i].BackPropagation(derriative.Clone());
                layers[i].WeightUpdate(eta, mu, lambda);
            }

            return error;
        }

        private double batchTraining(Vector[] trainingSet, Vector[] teachingSet)
        {
            double error = 0.0;

            for (int i = 0; i < trainingSet.Length; i++)
            {
                Vector forwardResult = ForwardPropagation(trainingSet[i]);

                Vector derriative = forwardResult.Clone();
                for (int j = 0; j < derriative.count; j++)
                {
                    error += LossFunction.f(forwardResult[j], teachingSet[i][j]);
                    derriative[j] = LossFunction.df(derriative[j], teachingSet[i][j]);
                }
                
                for (int j = layers.Count - 1; j >= 0; j--)
                {
                    derriative = layers[j].BackPropagation(derriative.Clone());
                }
            }

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].WeightUpdate(eta, mu, lambda);
            }

            return error;
        }
    }
}

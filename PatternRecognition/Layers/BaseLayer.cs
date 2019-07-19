using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using System.Collections.Generic;

namespace PatternRecognition.Layers
{
    abstract class BaseLayer
    {
        public abstract Vector inputs { set; }

        public abstract Vector outputs { get; }

        public abstract void ForwardPropagation();

        public abstract Vector BackPropagation(Vector vector);

        //TODO think about renaming
        //it is not updating wegihts, it is adding biases
        public virtual void WeightUpdate(double eta, double mu, double lambda) { }
    }
}

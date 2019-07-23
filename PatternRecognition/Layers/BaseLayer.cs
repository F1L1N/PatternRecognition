using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using System.Collections.Generic;

namespace PatternRecognition.Layers
{
    abstract class BaseLayer
    {
        /// <summary>
        /// 
        /// </summary>
        public string LayerType { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public string LayerName { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public abstract Vector inputs { set; }
        /// <summary>
        /// 
        /// </summary>
        public abstract Vector outputs { get; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Vector weights { get { return null; } protected set { } }
        /// <summary>
        /// 
        /// </summary>
        public virtual Vector biases { get { return null; } protected set { } }
        /// <summary>
        /// 
        /// </summary>
        protected int inputSize;
        /// <summary>
        /// 
        /// </summary>
        protected int outputSize;
        /// <summary>
        /// 
        /// </summary>
        protected int stride;
        public virtual string ToString(string mode) { return "None\n"; }
        /// <summary>
        /// 
        /// </summary>
        public abstract void ForwardPropagation();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public abstract Vector BackPropagation(Vector vector);

        //TODO think about renaming
        //it is not updating wegihts, it is adding biases
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eta"></param>
        /// <param name="mu"></param>
        /// <param name="lambda"></param>
        public virtual void WeightUpdate(double eta, double mu, double lambda) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        public virtual void GenerateWeights(double lower, double upper) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        public virtual void GenerateWeights(Vector weights) { }
    }
}

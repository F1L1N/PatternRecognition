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
    //IDisposable - для задания функции Dispose (реализует освобождение ресурсов)
    class Network<LossFunctionType> : IDisposable where LossFunctionType : ILoss, new()
    {
        private List<BaseLayer> layers;

        private readonly LossFunctionType loss = new LossFunctionType();

        public Network()
        {
            layers = new List<BaseLayer>();
        }

        public Network(BaseLayer[] paramLayers)
        {
            layers = new List<BaseLayer>(paramLayers);
        }

        public void AddLayer(BaseLayer layer)
        {
            layers.Add(layer);
        }

        public void AddLayer(BaseLayer[] paramLayers)
        {
            layers.AddRange(paramLayers);
        }

        public void Dispose()
        {
            layers.Clear();
        }
    }
}

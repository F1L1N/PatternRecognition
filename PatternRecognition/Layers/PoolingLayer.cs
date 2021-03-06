﻿using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognition.Layers
{
    class PoolingLayer<PoolingType> : BaseLayer where PoolingType : IPooling, new()
    {
        private static int numberChannels;
        Matrix[] input = new Matrix[numberChannels];
        Matrix[] output = new Matrix[numberChannels];
        private new int stride;
        private string mode;

        public override Vector inputs { set => throw new NotImplementedException(); }

        public override Vector outputs => throw new NotImplementedException();

        public override Vector BackPropagation(Vector vector)
        {
            throw new NotImplementedException();
        }

        public override void ForwardPropagation()
        {
            Pooling();
        }

        private void Pooling()
        {
            List<Matrix> result = new List<Matrix>();
            //пулинг
            for (int k = 0; k < input.Length; k++)
            {
                Matrix channelResult = new Matrix(input[k].CountRow / stride, input[k].CountColumn / stride);
                int currentRow = 0;
                int currentColumn = 0;
                for (int i = 0; i < input[k].CountRow; i += stride)
                {
                    for (int j = 0; j < input[k].CountColumn; j += stride)
                    {
                        Matrix subMatrix = input[k].getSubMatrix(i, j, stride);
                        //TODO remake it(so much switch, on each step)
                        switch (mode)
                        {
                            case "max":
                                channelResult[currentRow, currentColumn] = subMatrix.Max();
                                break;
                            case "avg":
                                channelResult[currentRow, currentColumn] = subMatrix.Average();
                                break;
                        }

                        currentColumn++;
                    }
                    currentRow++;
                }
                result.Add(channelResult);
            }
            output = result.ToArray();
        }
    }
}

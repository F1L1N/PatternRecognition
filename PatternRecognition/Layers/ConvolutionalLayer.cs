using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using PatternRecognition.Tools;

namespace PatternRecognition.Layers
{
    class ConvolutionalLayer<ActivationType> : BaseLayer where ActivationType : IActivation, new()
    {
        private new int stride = 1;
        private static int numberChannels;
        private int numberKernels;
        private string convolutionMode;
        private Matrix[] input = new Matrix[numberChannels];
        private Matrix[] output = new Matrix[numberChannels];
        private Matrix[] kernels;

        protected ActivationType activation = new ActivationType();

        private int inputHeight, inputWidth, inputDepth;

        protected int outputHeight, outputWidth, outputDepth;

        protected int weightsHeight, weightsWidth, weightsDepth, weightsSize;

        protected int padding;

        protected int paddingInputSize, paddingInputHeight, paddingInputWidth;

        //TODO what's wrong with this parameters
        protected Matrix[] _dw;
  
        protected Vector _db;

        protected Matrix[] _pre_dw;

        public ConvolutionalLayer()
        {
            //kernels = formKernel(5, 5);
            convolutionMode = "valid";
            numberChannels = 3;
        }

        public ConvolutionalLayer(Matrix[] input)
        {
            this.input = input;
        }

        public ConvolutionalLayer(
                int inputHeight, int inputWidth, int inputDepth, 
                int kernelSize = 3, int out_depth = 32,
                int stride = 1, int padding = 0, string layerName = "",
                Vector kernels = null, Vector biases = null)
        {
            this.inputHeight = inputHeight;
            this.inputWidth = inputWidth;
            this.inputDepth = inputDepth;
            this.inputSize = inputHeight * inputWidth * inputDepth;

            this.paddingInputHeight = inputHeight + padding * 2;
            this.paddingInputWidth = inputWidth + padding * 2;
            this.paddingInputSize = paddingInputHeight * paddingInputWidth * inputDepth;

            this.input = new Matrix[inputDepth];
            for (int i = 0; i < inputDepth; i++)
            {
                this.input[i].Full(paddingInputHeight, paddingInputWidth, 0);
            }

            this.weightsHeight = weightsWidth = kernelSize;
            this.weightsDepth = inputDepth * out_depth;
            this.weightsSize = kernelSize * kernelSize * weightsDepth;

            this.outputHeight = (paddingInputHeight - weightsHeight) / stride + 1;
            this.outputWidth = (paddingInputWidth - weightsWidth) / stride + 1;
            this.outputDepth = out_depth;
            this.outputSize = outputHeight * outputWidth * outputDepth;

            LayerType = "ConvolutionalLayer";
            //GenericsType = activation.Type();

            this.stride = stride;
            this.padding = padding;

            output = new Matrix[outputDepth];
            for (int i = 0; i < outputDepth; i++)
            {
                output[i].Full(outputHeight, outputWidth, 0);
            }

            //TODO think about loading kernels from xml
            this.kernels = new Matrix[weightsDepth];
            if (kernels != null && kernels.count == weightsSize)
            {
                GenerateWeights(kernels);
            }
            else
            {
                var accuracy = Math.Sqrt(inputDepth * weightsHeight * weightsWidth);
                GenerateWeights(-1.0 / accuracy, 1.0 / accuracy);
            }

            if (biases != null && biases.count == out_depth)
            {
                biases = biases.Clone();
            }
            else
            {
                biases.Full(out_depth, 0);
            }


            _dw = new Matrix[weightsDepth];
            _pre_dw = new Matrix[weightsDepth];
            for (int wd = 0; wd < weightsDepth; wd++)
            {
                _dw[wd].Full(weightsHeight, weightsWidth, 0);
                _pre_dw[wd].Full(weightsHeight, weightsWidth, 0);
            }

            _db.Full(out_depth, 0);
        }

        public override void ForwardPropagation()
        {
            Convolution();
        }

        public override Vector BackPropagation(Vector vector)
        {
            return null;
        }

        private void modifiedInput()
        {
            Matrix[] newInput = new Matrix[numberChannels];
            int shift;
            switch (convolutionMode)
            {
                case "same":
                    shift = kernels[0].CountRow % 2;
                    break;
                case "full":
                    shift = kernels[0].CountRow - 1;
                    break;
                default:
                    Console.WriteLine("Changes don't need.");
                    return;
            }
            for (int i = 0; i < numberChannels; i++)
            {
                newInput[i] = new Matrix(input[i].CountRow + 2 * shift, input[i].CountColumn + 2 * shift);
                for (int j = 0; j < newInput[i].CountRow; j++)
                {
                    for (int k = 0; k < newInput[i].CountColumn; k++)
                    {
                        if ((k < shift) && (j < shift))
                        {
                            newInput[i][j, k] = input[i][j, k];
                        }
                        else if ((k >= shift) && (k < shift + input[i].CountColumn) && (j < shift))
                        {
                            newInput[i][j, k] = input[i][j, k - shift];
                        }
                        else if ((k >= shift + input[i].CountColumn) && (j < shift))
                        {
                            newInput[i][j, k] = input[i][j, k - 2 * shift];
                        }
                        else if ((k < shift) && (j >= shift) && (j < shift + input[i].CountRow))
                        {
                            newInput[i][j, k] = input[i][j - shift, k];
                        }
                        else if ((k >= shift) && (k < shift + input[i].CountRow) && (j >= shift) && (j < shift + input[i].CountColumn))
                        {
                            newInput[i][j, k] = input[i][j - shift, k - shift];
                        }
                        else if ((k >= shift + input[i].CountRow) && (j >= shift) && (j < shift + input[i].CountRow))
                        {
                            newInput[i][j, k] = input[i][j, k];
                        }
                        else if ((k < shift) && (j >= shift + input[i].CountRow))
                        {
                            newInput[i][j, k] = input[i][j - 2 * shift, k];
                        }
                        else if ((k >= shift) && (k < shift + input[i].CountColumn) && (j >= shift + input[i].CountRow))
                        {
                            newInput[i][j, k] = input[i][j - 2 * shift, k - shift];
                        }
                        else if ((k >= shift + input[i].CountColumn) && (j >= shift + input[i].CountRow))
                        {
                            newInput[i][j, k] = input[i][j - 2 * shift, k - 2 * shift];
                        }
                    }
                }
            }
            input = newInput;
        }

        private static Matrix[] formKernel(int numberKernels, int x, int y)
        {
            //List<Matrix> kernel = new Matrix(x, y);
            Matrix[] kernels = new Matrix[numberKernels];
            Random random = new Random();
            for (int k = 0; k < numberChannels; k++)
            {
                Matrix kernel = new Matrix(x, y);
                for (int i = 0; i < kernel.CountRow; i++)
                {
                    for (int j = 0; j < kernel.CountColumn; j++)
                    {
                        kernel[i, j] = Convert.ToDouble(random.Next(-10, 10)) / 100;
                    }
                }
                kernels[k] = kernel;
            }
            return kernels;
        }

        //перегрузка для загрузки из памяти
        private static List<Matrix> formKernel()
        {
            List<Matrix> kernels = new List<Matrix>();
            return kernels;
        }

        private void Convolution()
        {
            //расширение размера
            modifiedInput();
            //свертка
            int shift = kernels[0].CountRow % 2;
            for (int o = 0; o < input.Length; o++)
            {
                //Matrix featureMap = new Matrix();
                for (int p = 0; p < numberKernels; p++)
                {
                    for (int i = 1; i < input[o].CountRow - 1; i += stride)
                    {
                        for (int j = 1; j < input[o].CountColumn - 1; j += stride)
                        {
                            double output = 0.0;
                            for (int k = 0; k < kernels[0].CountRow; k++)
                            {
                                for (int h = 0; h < kernels[0].CountColumn; h++)
                                {
                                    output += input[o][i - shift, j - shift] * kernels[p][k, h];
                                }
                            }
                            input[o][i, j] = output;
                        }
                    }
                }
            }
            output = input;
        }

        public override Vector inputs
        {
            set
            {
                if (inputSize != value.count)
                {
                    throw new ArgumentException("Size of inputs is different");
                }
                int counter = 0;
                for (int d = 0; d < inputDepth; d++)
                {
                    for (int h = 0; h < inputHeight; h++)
                    {
                        for (int w = 0; w < inputWidth; w++)
                        {
                            input[d][h + padding, w + padding] = value[counter];
                            counter++;
                        }
                    }
                }
            }
        }

        public override Vector outputs
        {
            get
            {
                Matrix[] matrices = new Matrix[output.Length];
                for (int k = 0; k < matrices.Length; k++)
                {
                    for (int i = 0; i < matrices[k].CountRow; i++)
                    {
                        for (int j = 0; j < matrices[k].CountColumn; j++)
                        {
                            matrices[k][i, j] = activation.f(output[k][i, j]);
                        }
                    }
                }
                return Converters.ToVector(matrices, outputHeight, outputWidth);
            }
        }

        public override Vector weights
        {
            get
            {
                return Converters.ToVector(kernels, weightsHeight, weightsWidth);
            }
            protected set
            {
                if (weightsSize != value.count)
                {
                    throw new ArgumentException("Size of kernels are different");
                }
                kernels = Converters.ToMatrices(value, weightsDepth, weightsHeight, weightsWidth);
            }
        }

        public override Vector biases
        {
            get
            {
                return biases.Clone();
            }
            protected set
            {
                if (outputDepth != value.count)
                {
                    throw new ArgumentException("Size of biases are different");
                }
                biases = value.Clone();
            }
        }
    }
}

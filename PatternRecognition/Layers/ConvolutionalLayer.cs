using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using PatternRecognition.Tools;

namespace PatternRecognition.Layers
{
    class ConvolutionalLayer
    {
        int stride = 1;
        private static int numberChannels;
        private int numberKernels = numberChannels;
        private string convolutionMode;
        Matrix[] input = new Matrix[numberChannels];
        Matrix[] output = new Matrix[numberChannels];
        List<Matrix> kernels;

        public ConvolutionalLayer()
        {
            kernels = formKernel(5, 5);
            convolutionMode = "valid";
            numberChannels = 3;
        }

        public ConvolutionalLayer(Matrix[] input)
        {
            this.input = input;
        }

        private Vector BackPropagation(Vector vector)
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

        private static List<Matrix> formKernel(int x, int y)
        {
            //List<Matrix> kernel = new Matrix(x, y);
            List<Matrix> kernels = new List<Matrix>();
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
                kernels.Add(kernel);
            }
            return kernels;
        }

        //перегрузка для загрузки из памяти
        private static List<Matrix> formKernel()
        {
            List<Matrix> kernels = new List<Matrix>();
            return kernels;
        }

        private void formOutput()
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
    }
}

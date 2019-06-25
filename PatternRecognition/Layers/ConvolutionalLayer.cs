using System;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;

namespace PatternRecognition.Layers
{
    class ConvolutionalLayer
    {
        int stride = 1;
        private static int numberChannels = 3;
        private string convolutionMode = "valid";
        Matrix[] input = new Matrix[numberChannels];
        Matrix[] output = new Matrix[numberChannels];
        Matrix kernel = formKernel(5, 5);

        public ConvolutionalLayer(int bias, IActivation activationFunc)
        {

        }

     
        private void modifiedInput()
        {
            Matrix[] newInput = new Matrix[numberChannels];
            int shift;
            switch (convolutionMode)
            {
                case "same":
                    shift = kernel.CountRow % 2;
                    break;
                case "full":
                    shift = kernel.CountRow - 1;
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

        private static Matrix formKernel(int x, int y)
        {
            Matrix kernel = new Matrix(x, y);
            Random rand = new Random();
            for (int i = 0; i < kernel.CountRow; i++)
                for (int j = 0; j < kernel.CountColumn; j++)
                {
                    kernel[i, j] = Convert.ToDouble(rand.Next(-10, 10)) / 100;
                }
            return kernel;
        }

        private void formOutput()
        {
            //загрузка изображений по очереди

            //расширение размера
            modifiedInput();
            //свертка
            int shift = kernel.CountRow % 2;
            for (int o = 0; o < input.Length; o++)
            {
                for (int i = 1; i < input[o].CountRow - 1; i += stride)
                {
                    for (int j = 1; j < input[o].CountColumn - 1; j += stride)
                    {
                        double output = 0.0;
                        for (int k = 0; k < kernel.CountRow; k++)
                        {
                            for (int h = 0; h < kernel.CountColumn; h++)
                            {
                                output += input[o][i - shift, j - shift] * kernel[k, h];
                            }
                        }
                        input[o][i, j] = output;
                    }
                }
            }
            output = input;
        }
    }
}

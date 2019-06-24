using System;
using System.IO;
using System.Drawing;
using PatternRecognition.Interfaces;
using PatternRecognition.Templates;
using System.Drawing.Imaging;

namespace PatternRecognition.Layers
{
    class ConvolutionalLayer : BaseLayer
    {
        int stride = 1;
        private static int numberChannels = 3;
        private string convolutionMode = "valid";
        Matrix[] input = new Matrix[numberChannels];
        Matrix[] output = new Matrix[numberChannels];
        Matrix kernel = formKernel(5, 5);

        public ConvolutionalLayer(int iCount, int oCount, IActivation activationFunc) : base(iCount, oCount, activationFunc)
        {

        }

        public Matrix[,] loadInput()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + @"..\\..\\trainData";
            DirectoryInfo dir = new DirectoryInfo(path);
            Matrix[,] result = new Matrix[dir.GetFiles().Length, numberChannels];
            int i = 0;
            foreach (var item in dir.GetFiles())
            {
                Matrix[] load = loadImage(item.FullName);
                for (int j = 0; j < numberChannels; j++)
                {
                    result[i, j] = load[j];
                }
                i++;
            }
            //writeRGB(result, path, ".png");
            return result;
        }

        public void writeRGB(Matrix[,] images, String path, String imageFormat)
        {
            String newPath = path;
            int number = images.Length / numberChannels;
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < numberChannels; j++)
                {
                    switch (j)
                    {
                        case 0:
                            newPath = path + "\\" + i + "r" + imageFormat;
                            break;
                        case 1:
                            newPath = path + "\\" + i + "g" + imageFormat;
                            break;
                        case 2:
                            newPath = path + "\\" + i + "b" + imageFormat;
                            break;
                    }
                    Bitmap myBitmap = new Bitmap(images[i, j].CountRow, images[i, j].CountColumn);
                    for (int k = 0; k < images[i, j].CountRow; k++)
                    {
                        for (int h = 0; h < images[i, j].CountColumn; h++)
                        {
                            Color color = new Color();
                            switch (j)
                            {
                                case 0:
                                    color = Color.FromArgb((byte)images[i, j][k, h], 0, 0);
                                    break;
                                case 1:
                                    color = Color.FromArgb(0, (byte)images[i, j][k, h], 0);
                                    break;
                                case 2:
                                    color = Color.FromArgb(0, 0, (byte)images[i, j][k, h]);
                                    break;
                            }
                            myBitmap.SetPixel(k, h, color);
                        }
                    }
                    myBitmap.Save(newPath, ImageFormat.Bmp);
                    newPath = path;
                }
            }
        }

        public Matrix[] loadImage(String path)
        {
            Matrix[] result = new Matrix[numberChannels];
            try
            {
                var image = new Bitmap(path, true);
                int width = image.Width;
                int height = image.Height;
                for (int i = 0; i < numberChannels; i++)
                {
                    result[i] = new Matrix(width, height);
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            Color pixelColor = image.GetPixel(x, y);
                            switch (i)
                            {
                                case 0:
                                    result[i][x, y] = pixelColor.R;
                                    break;
                                case 1:
                                    result[i][x, y] = pixelColor.G;
                                    break;
                                case 2:
                                    result[i][x, y] = pixelColor.B;
                                    break;
                            }
                        }
                    }
                }
                return result;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Load image error.");
                return null;
            }
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

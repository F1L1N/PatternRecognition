using PatternRecognition.Templates;
using System;
using System.Drawing;
using System.IO;

namespace PatternRecognition.Layers
{
    class InputLayer
    {
        private const int numberChannels = 3;

        private string folderPath;

        private Matrix[,] output;

        public InputLayer(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public void loadInput()
        {
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + @"..\\..\\trainData";
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            output = new Matrix[dir.GetFiles().Length, numberChannels];
            //Matrix[,] result = new Matrix[dir.GetFiles().Length, numberChannels];
            int i = 0;
            foreach (var item in dir.GetFiles())
            {
                Matrix[] load = loadImage(item.FullName);
                for (int j = 0; j < numberChannels; j++)
                {
                    output[i, j] = load[j];
                }
                i++;
            }
        }

        public Matrix[] loadImage(string path)
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
                Console.WriteLine("Load image error on path " + path);
                return null;
            }
        }
    }
}

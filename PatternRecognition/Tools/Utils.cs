using PatternRecognition.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognition
{
    public class Utils
    {
        public void writeRGB(Matrix[,] images, string path, string imageFormat, int numberChannels)
        {
            string newPath = path;
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
    }
}

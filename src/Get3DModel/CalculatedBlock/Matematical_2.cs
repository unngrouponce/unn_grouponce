using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    public class Matematical_2 : IMathematical
    {
        private Bitmap image;
        public double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore3x3(x, y);
            return laplaceOperator3x3(core);
        }

        public void setImage(Bitmap image)
        {
            this.image = image;
        }

        private double[,] getCore3x3(int x, int y)
        {
            double[,] core = new double[3, 3];
            int[] indicesX = new int[3];
            indicesX[0] = x == 0 ? -1 : x - 1;
            indicesX[1] = x;
            indicesX[2] = x == image.Width ? -1 : x + 1;

            int[] indicesY = new int[3];
            indicesY[0] = y == 0 ? -1 : y - 1;
            indicesY[1] = y;
            indicesY[2] = y == image.Height ? -1 : y + 1;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (indicesX[i] == -1 || indicesY[j] == -1)
                    {
                        core[i, j] = 0;
                    }
                    else
                    {
                        core[i, j] = image.GetPixel(indicesX[i], indicesY[j]).B;
                    }
                }
            return core;
        }

        private double laplaceOperator3x3(double[,] core)
        {
            double[,] operatorLaplace = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            double convolution = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    convolution += core[i, j] * operatorLaplace[j, i];
            return convolution;
        }
    }
}

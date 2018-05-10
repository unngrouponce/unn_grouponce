using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    public abstract class MathematicialSearchPoint:IMathematical
    {
        protected double[,] xMatrix;
        protected double[,] yMatrix;
        protected Bitmap image;
        protected double deltaThreshold;

        public MathematicialSearchPoint()
        {
            xMatrix = null;
            yMatrix = null;
            deltaThreshold = 0;
        }

        public double[,] XMatrix { get { return xMatrix; } }
        public double[,] YMatrix { get { return yMatrix; } }

        public void setImage(System.Drawing.Bitmap image)
        {
            this.image = image;
        }

        public void setDeltaThreshold(double threshold)
        {
            deltaThreshold = threshold;
        }

        abstract public double gradientAtPoint(int x, int y);

        protected double[,] getCore3x3(int x, int y)
        {
            double[,] core = new double[3, 3];
            int[] indicesX = new int[3];
            indicesX[0] = x == 0 ? -1 : x - 1;
            indicesX[1] = x;
            indicesX[2] = x == image.Width - 1 ? -1 : x + 1;

            int[] indicesY = new int[3];
            indicesY[0] = y == 0 ? -1 : y - 1;
            indicesY[1] = y;
            indicesY[2] = y == image.Height - 1 ? -1 : y + 1;

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

        protected double[,] getCore5x5(int x, int y)
        {
            double[,] core = new double[5, 5];
            int[] indicesX = new int[5];
            indicesX[0] = x <= 1 ? -1 : x - 2;
            indicesX[1] = x == 0 ? -1 : x - 1;
            indicesX[2] = x;
            indicesX[3] = x == image.Width - 1 ? -1 : x + 1;
            indicesX[4] = x >= image.Width - 2 ? -1 : x + 2;

            int[] indicesY = new int[5];
            indicesY[0] = y <= 1 ? -1 : y - 2;
            indicesY[1] = y == 0 ? -1 : y - 1;
            indicesY[2] = y;
            indicesY[3] = y == image.Height - 1 ? -1 : y + 1;
            indicesY[4] = y >= image.Height - 2 ? -1 : y + 2;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
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

        protected double[,] getCore7x7(int x, int y)
        {
            double[,] core = new double[7, 7];
            int[] indicesX = new int[7];
            indicesX[0] = x <= 2 ? -1 : x - 3;
            indicesX[1] = x <= 1 ? -1 : x - 2;
            indicesX[2] = x == 0 ? -1 : x - 1;
            indicesX[3] = x;
            indicesX[4] = x == image.Width - 1 ? -1 : x + 1;
            indicesX[5] = x >= image.Width - 2 ? -1 : x + 2;
            indicesX[6] = x >= image.Width - 3 ? -1 : x + 3;

            int[] indicesY = new int[7];
            indicesY[0] = y <= 2 ? -1 : y - 3;
            indicesY[1] = y <= 1 ? -1 : y - 2;
            indicesY[2] = y == 0 ? -1 : y - 1;
            indicesY[3] = y;
            indicesY[4] = y == image.Height - 1 ? -1 : y + 1;
            indicesY[5] = y >= image.Height - 2 ? -1 : y + 2;
            indicesY[6] = y >= image.Height - 3 ? -1 : y + 3;

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
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
        abstract public double calculation(double xConvolution, double yConvolution);

        protected double Gradient(double[,] core)
        {
            double xConvolution = 0;
            double yConvolution = 0;

            if (xMatrix!=null)
                for (int i = 0; i < core.GetLength(0); i++)
                    for (int j = 0; j < core.GetLength(0); j++)
                        xConvolution += core[i, j] * xMatrix[j, i];

            if (yMatrix != null)
                for (int i = 0; i < core.GetLength(0); i++)
                    for (int j = 0; j < core.GetLength(0); j++)
                        yConvolution += core[i, j] * yMatrix[j, i];
            
            xConvolution /= getMaxValueX();
            yConvolution /= getMaxValueY();
            return Math.Abs(calculation(xConvolution, yConvolution));
        }


        public double getDeltaThreshold()
        {
            return deltaThreshold;
        }

        public double getMaxValueX()
        {
            double sum = 0;
            for (int i = 0; i < xMatrix.GetLength(0); i++)
                for (int j = 0; j < xMatrix.GetLength(1); j++)
                {
                    if (xMatrix[i, j] > 0)
                    {
                        sum += xMatrix[i, j] * 255;
                    }
                }
            return sum;
        }

        public double getMaxValueY()
        {
            double sum = 0;
            for (int i = 0; i < yMatrix.GetLength(0); i++)
                for (int j = 0; j < yMatrix.GetLength(1); j++)
                {
                    if (yMatrix[i, j] > 0)
                    {
                        sum += yMatrix[i, j] * 255;
                    }
                }
            return sum;
        }
    }
}

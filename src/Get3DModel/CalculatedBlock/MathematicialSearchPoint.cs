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

        public MathematicialSearchPoint()
        {
            xMatrix = null;
            yMatrix = null;
        }

        public double[,] XMatrix { get { return xMatrix; } }
        public double[,] YMatrix { get { return yMatrix; } }

        public void setImage(System.Drawing.Bitmap image)
        {
            this.image = image;
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

        abstract public double calculation(double xConvolution, double yConvolution);

        protected double Gradient3x3(double[,] core)
        {
            double xConvolution = 0;
            double yConvolution = 0;

            if (xMatrix!=null)
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        xConvolution += core[i, j] * xMatrix[j, i];

            if (yMatrix != null)
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        yConvolution += core[i, j] * yMatrix[j, i];

            return calculation(xConvolution, yConvolution);
        }
    }
}

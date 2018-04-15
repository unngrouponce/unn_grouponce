using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint7 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint7()
        {
            double[,] currentX_Matrix = { { 1, 1, 1, 0, -1, -1, -1 }, { 1, 1, 2, 0, -2, -1, -1 }, { 1, 2, 3, 0, -3, -2, -1 },
                { 2, 3, 4, 0, -4, -3, -2 }, { 1, 2, 3, 0, -3, -2, -1 }, { 1, 1, 2, 0, -2, -1, -1 }, { 1, 1, 1, 0, -1, -1, -1 } };
            double[,] currentY_Matrix = { { -1, -1, -1, -2, -1, -1, -1 }, { -1, -1, -2, -3, -2, -1, -1 }, { -1, -2, -3, -4, -3, -2, -1 },
                { 0, 0, 0, 0, 0, 0, 0 }, { 1, 2, 3, 4, 3, 2, 1 }, { 1, 1, 2, 3, 2, 1, 1 }, { 1, 1, 1, 2, 1, 1, 1 } };
            xMatrix = currentX_Matrix;
            yMatrix = currentY_Matrix;
        }

        public override double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore7x7(x, y);
            return Gradient(core);
        }

        public override double calculation(double xConvolution, double yConvolution)
        {
            return Math.Sqrt(xConvolution * xConvolution + yConvolution * yConvolution);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint11 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint11()
        {
            double[,] currentX_Matrix = { { 0, 0, 0, 1, 0, 0, 0 }, { 0, 1, 1, 2, 1, 1, 0 }, { 0, 1, 2, 3, 2, 1, 0 },
                { 1, 2, 3, -44, 3, 2, 1 }, { 0, 0, 0, 1, 0, 0, 0 }, { 0, 1, 1, 2, 1, 1, 0 }, { 0, 1, 2, 3, 2, 1, 0 } };
            double[,] currentY_Matrix = { { 1, 0, 0, 0, 0, 0, 1 }, { 0, 2, 1, 1, 1, 2, 0 }, { 0, 1, 3, 2, 3, 1, 0 },
                { 0, 1, 2, -44, 2, 1, 0 }, { 1, 0, 0, 0, 0, 0, 1 }, { 0, 2, 1, 1, 1, 2, 0 }, { 0, 1, 3, 2, 3, 1, 0 } };
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
            return (xConvolution + yConvolution) / 2;
        }
    }
}

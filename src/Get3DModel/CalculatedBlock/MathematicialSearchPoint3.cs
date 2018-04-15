using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint3 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint3()
        {
            double[,] currentX_Matrix = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };
            xMatrix = currentX_Matrix;
        }

        public override double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore3x3(x, y);
            return Gradient(core);
        }

        public override double calculation(double xConvolution, double yConvolution)
        {
            return xConvolution;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint1 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint1()
        {
            double[,] currentX_Matrix = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            double[,] currentY_Matrix = { { 1, 0, 1 }, { 0, -4, 0 }, { 1, 0, 1 } };
            xMatrix = currentX_Matrix;
            yMatrix = currentY_Matrix;
            deltaThreshold = 0.23;
        }
        
        public override double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore3x3(x, y);
            return Gradient(core);
        }

        public override double calculation(double xConvolution, double yConvolution)
        {
            return (xConvolution + yConvolution) / 2;
        }
    }
}

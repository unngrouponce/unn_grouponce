using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint12 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint12()
        {
            double[,] currentX_Matrix = {
    {0,1,1,2,2,2,1,1,0},
    {1,2,4,5,5,5,4,2,1},
    {1,4,5,3,0,3,5,4,1},
    {2,5,3,-12,-24,-12,3,5,2},
    {2,5,0,-24,-40,-24,0,5,2},
    {2,5,3,-12,-24,-12,3,5,2},
    {1,4,5,3,0,3,5,4,1},
    {1,2,4,5,5,5,4,2,1},
    {0,1,1,2,2,2,1,1,0}
        };
            xMatrix = currentX_Matrix;
            deltaThreshold = 0.5;
        }
        public override double calculation(double xConvolution, double yConvolution)
        {
            return xConvolution;
        }

        public override double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore9x9(x, y);
            return Gradient(core);
        }
    }
}

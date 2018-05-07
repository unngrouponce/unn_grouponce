using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace CalculatedBlock
{
    class MathematicalDefault : IMathematical
    {
        private Bitmap image;
        public double gradientAtPoint(int x, int y)
        {
            return image.GetPixel(x, y).R;
        }

        public void setImage(Bitmap image)
        {
            this.image = image;
            //this.image = new Data.Image((Bitmap)image.Clone()).Convolution(new double[,]
            //                 {{1, 1, 1},
            //                  {1, -8, 1},
            //                  {1, 1, 1}});
        }



        public void setDeltaThreshold(double threshold)
        {
            throw new NotImplementedException();
        }


        public double getDeltaThreshold()
        {
            throw new NotImplementedException();
        }

        public double getMaxValueX()
        {
            throw new NotImplementedException();
        }

        public double getMaxValueY()
        {
            throw new NotImplementedException();
        }
    }
}

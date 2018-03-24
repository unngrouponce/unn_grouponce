using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    class Mathematical : IMathematical
    {
        private Bitmap image;
        public double gradientAtPoint(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void setImage(Bitmap image)
        {
            this.image = image;
        }
    }
}

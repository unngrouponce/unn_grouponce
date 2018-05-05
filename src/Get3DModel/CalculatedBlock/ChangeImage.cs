using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    class ChangeImage : IChangeImage
    {
        public Bitmap translateToMonochrome(Bitmap image)
        {
          /*  Bitmap res = new Bitmap(image.Width, image.Height, image.PixelFormat);
            for (int y = 0; y < image.Height; ++y)
            for (int x = 0; x < image.Width; ++x)
             {
                Color c = ((Bitmap)image.Clone()).GetPixel(x, y);
                byte rgb = (byte)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
                 res.SetPixel(x, y, Color.FromArgb(c.A, rgb, rgb, rgb));
                }
            return res;
           */
            return image;
        }
    }
}

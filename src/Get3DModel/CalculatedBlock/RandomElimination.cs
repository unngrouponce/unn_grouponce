using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
   public class RandomElimination : IElimination
    {
        int width=0;
        int hight=0;
        public void calculateGradientImage(Data.Image image)
        {
            if (width == 0) 
            {
                width = image.width();
                hight = image.height();
            }
        }

        public List<Data.Point> getSolution()
        {
            List<Data.Point> result = new List<Data.Point>();
            Random rnd = new Random();
            for (int x = 0; x < width; x++)
                for (int y = 0; y < hight; y++)
                    if (rnd.Next(25) == 5) result.Add(new Data.Point(x, y));
            return result;
        }
    }
}

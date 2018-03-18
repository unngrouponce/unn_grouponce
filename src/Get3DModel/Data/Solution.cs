using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using ParsingInputData;

namespace Data
{
    /// <summary>
    /// Класс для полученного решения системы
    /// </summary>
    public class Solution
    {
        Bitmap sharpImage;
        double[,] mapHeights;
        bool nullSolution;

        public Solution()
        {
            sharpImage = null;
            mapHeights = null;
            nullSolution = true;
        }

        public void createdBeginSolution(int width, int height)
        {
            mapHeights = new double[width, height];
            sharpImage = new Bitmap(width, height);
            for (int Xcount = 0; Xcount < width; Xcount++)
                for (int Ycount = 0; Ycount < height; Ycount++)
                {
                    mapHeights[Xcount, Ycount] = -1;
                    sharpImage.SetPixel(Xcount, Ycount, Color.White);
                }
            nullSolution = false;
        }

        public void setValue(List<Point> coordinate, Image image)
        {
            if (nullSolution)
                createdBeginSolution(image.width(), image.height());
            foreach (Point point in coordinate)
            {
                mapHeights[point.x, point.y] = image.tall;
                sharpImage.SetPixel(point.x, point.y,image.GetPixel(point.x, point.y));
            }
        }
    }
}

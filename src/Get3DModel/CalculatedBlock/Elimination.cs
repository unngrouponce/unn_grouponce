using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Drawing;

namespace CalculatedBlock
{
    public class Elimination: IElimination
    {
        double[,] swingSharpness;
        IChangeImage changeImage;
        IMathematical matematical;

        public Elimination()
        {
            changeImage = new ChangeImage();
            matematical = new MathematicialSearchPoint13();
            swingSharpness = null;
        }

        public Elimination(IMathematical strategia)
        {
            changeImage = new ChangeImage();
            matematical = strategia;
            swingSharpness = null;
        }
        Bitmap curentImage;
        public void calculateGradientImage(Data.Image image)
        {
            if (swingSharpness == null)
            {
                swingSharpness = new double[image.width(), image.height()];
                for (int x = 0; x < image.width(); x++)
                    for (int y = 0; y < image.height(); y++)
                        swingSharpness[x, y] = -255;
            }

            curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            matematical.setImage(curentImage);
          /*
           for (int x = 0; x < image.width(); x++)
                for (int y = 0; y < image.height(); y++)
                {
                    double gradient = matematical.gradientAtPoint(x, y);
                    if (gradient > swingSharpness[x, y])
                        swingSharpness[x, y] = gradient;
                }
           */
            Parallel.For(0, 4, calculateGradientImage);

        }
        void calculateGradientImage(int sector) 
        {
            int x_begin=0, x_end=0, y_begin=0, y_end=0;
            lock (curentImage)
            {
                switch (sector)
                {
                    case 0: x_begin = 0; x_end = curentImage.Width / 2; y_begin = 0; y_end = curentImage.Height / 2; break;
                    case 1: x_begin = curentImage.Width / 2; x_end = curentImage.Width; y_begin = 0; y_end = curentImage.Height / 2; break;
                    case 2: x_begin = 0; x_end = curentImage.Width / 2; y_begin = curentImage.Height / 2; y_end = curentImage.Height; break;
                    case 3: x_begin = curentImage.Width / 2; x_end = curentImage.Width; y_begin = curentImage.Height / 2; y_end = curentImage.Height; break;
                }
            }
            for (int x = x_begin; x < x_end; x++)
                for (int y = y_begin; y < y_end; y++)
                {
                    double gradient = matematical.gradientAtPoint(x, y);
                    if (gradient > swingSharpness[x, y])
                        swingSharpness[x, y] = gradient;
                }
        }

        public List<Data.Point> getSolution()
        {
            List<Data.Point> result = new List<Data.Point>();
           /* double maxGradient = -255;
            double minGradient = 255;

            for (int i = 0; i < swingSharpness.GetLength(0); i++)
                for (int j = 0; j < swingSharpness.GetLength(1); j++)
                {
                    if (maxGradient < swingSharpness[i, j]) maxGradient = swingSharpness[i, j];
                    if (minGradient > swingSharpness[i, j]) minGradient = swingSharpness[i, j];
                }
            */

           double maxGradient =  swingSharpness.Cast<double>().Max();
           double minGradient = swingSharpness.Cast<double>().Min();
            double delta = matematical.getDeltaThreshold();
            double threshold = minGradient + ((maxGradient - minGradient) * delta);

            for (int x = 0; x < swingSharpness.GetLength(0); x++)
                for (int y = 0; y < swingSharpness.GetLength(1); y++)
                    if (swingSharpness[x, y] >= threshold)
                        result.Add(new Data.Point(x, y));

            return result;
        }
    }
}

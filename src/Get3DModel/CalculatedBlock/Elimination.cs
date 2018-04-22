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
            matematical = new MathematicialSearchPoint1();
            swingSharpness = null;
        }

        public Elimination(IMathematical strategia)
        {
            changeImage = new ChangeImage();
            matematical = strategia;
            swingSharpness = null;
        }

        public void calculateGradientImage(Data.Image image)
        {
            if (swingSharpness == null)
            {
                swingSharpness = new double[image.width(), image.height()];
                for (int x = 0; x < image.width(); x++)
                    for (int y = 0; y < image.height(); y++)
                        swingSharpness[x, y] = -255;
            }

            Bitmap curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            matematical.setImage(curentImage);
            for (int x = 0; x < image.width(); x++)
                for (int y = 0; y < image.height(); y++)
                {
                    double gradient = matematical.gradientAtPoint(x, y);
                    if (gradient > swingSharpness[x, y])
                        swingSharpness[x, y] = gradient;
                }
        }

        public List<Data.Point> getSolution()
        {
            List<Data.Point> result = new List<Data.Point>();
            double maxGradient = -255;
            double minGradient = 255;

            for (int i = 0; i < swingSharpness.GetLength(0); i++)
                for (int j = 0; j < swingSharpness.GetLength(1); j++)
                {
                    if (maxGradient < swingSharpness[i, j]) maxGradient = swingSharpness[i, j];
                    if (minGradient > swingSharpness[i, j]) minGradient = swingSharpness[i, j];
                }

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

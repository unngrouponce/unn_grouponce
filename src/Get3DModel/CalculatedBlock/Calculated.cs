using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Drawing;

namespace CalculatedBlock
{
    public class Calculated : ICalculated
    {
        Solution solution;
        IChangeImage changeImage;
        IMathematical matematical;
        double[,] swingSharpness;
        double width;
        double height;
        double thresholdDelta;

        public Calculated()
        {
            changeImage = new ChangeImage();
            matematical = new MathematicalDefault();
        }

        public Calculated(IMathematical stategia)
        {
            changeImage = new ChangeImage();
            matematical = stategia;
        }

        public void createdBeginSolution()
        {
            solution = new Solution();
            swingSharpness = null;
        }

        public void clarifySolution(Data.Image image)
        {
            if (swingSharpness == null)
            {
                swingSharpness = new double[image.width(), image.height()];
                for (int x = 0; x < image.width(); x++)
                    for (int y = 0; y < image.height(); y++)
                        swingSharpness[x,y] = 0;
                width = image.width();
                height = image.height();
            }

            List<Data.Point> listPoint = new List<Data.Point>();
            Bitmap curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            matematical.setImage(curentImage);
            for (int x = 0; x < image.width(); x++)
                for (int y = 0; y < image.height(); y++)
                {
                    double gradient = matematical.gradientAtPoint(x, y);
                    if (gradient > swingSharpness[x, y])
                    {
                        listPoint.Add(new Data.Point(x,y));
                        swingSharpness[x, y] = gradient;
                    }
                }
            solution.setValue(listPoint, image);
        }

        public Data.Solution getSolution()
        {
            return solution;
        }


        public void eliminationPoints(double delta)
        {
            List<Data.Point> listPoint = new List<Data.Point>();
            double maxGradient = -255;
            double minGradient = 255;

            for (int i = 0; i < swingSharpness.GetUpperBound(0)+1; i++)
                for (int j = 0; j < swingSharpness.GetUpperBound(1)+1; j++)
                {
                    if (maxGradient < swingSharpness[i, j]) maxGradient = swingSharpness[i, j];
                    if (minGradient > swingSharpness[i, j]) minGradient = swingSharpness[i, j];
                }

            double threshold = minGradient + ((maxGradient - minGradient) * delta);

            for (int x = 0; x < swingSharpness.GetUpperBound(0)+1; x++)
                for (int y = 0; y < swingSharpness.GetUpperBound(1)+1; y++)
                    if (swingSharpness[x,y]<threshold)
                        listPoint.Add(new Data.Point(x, y));

            solution.setValue(listPoint, Color.White);
        }
    }
}

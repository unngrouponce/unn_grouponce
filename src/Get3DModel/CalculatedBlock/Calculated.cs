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

        public Calculated()
        {
            changeImage = new ChangeImage();
            matematical = new Mathematical_1();
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
                    for (int y = 0; y < image.width(); y++)
                        swingSharpness[x,y] = -255;
            }

            List<Data.Point> listPoint = new List<Data.Point>();
            Bitmap curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            matematical.setImage(curentImage);
            for (int x = 0; x < image.width(); x++)
                for (int y = 0; y < image.width(); y++)
                {
                    double gradient = matematical.gradientAtPoint(x, y);
                    if (gradient > swingSharpness[x, y])
                    {
                        listPoint.Add(new Data.Point(x,y));
                        swingSharpness[x, y] = gradient;
                    }
                }
            solution.binarization();
            solution.setValue(listPoint, image);
        }

        public Data.Solution getSolution()
        {
            return solution;
        }
    }
}

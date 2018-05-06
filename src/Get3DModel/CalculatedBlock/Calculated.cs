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
        //double thresholdDelta;

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

        public void clarifySolution(Data.Image image, List<IMathematical> coreGoodPoint, List<Data.Point> goodPoint)
        {
            if (swingSharpness == null)
            {
                swingSharpness = new double[image.width(), image.height()];
              /*  for (int x = 0; x < image.width(); x++)
                    for (int y = 0; y < image.height(); y++)
                        swingSharpness[x, y] = 0;*/
                width = image.width();
                height = image.height();
            }

            List<Data.Point> listPoint = new List<Data.Point>();
            Bitmap curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            HashSet<IMathematical> set = new HashSet<IMathematical>(coreGoodPoint);
            foreach (var i in set) i.setImage(curentImage);
            for (int i = 0; i < goodPoint.Count; i++ )
            {
                int x = goodPoint[i].x;
                int y = goodPoint[i].y;
              matematical=  set.First(item => item.GetType() == coreGoodPoint[i].GetType());
               
                double gradient = matematical.gradientAtPoint(x, y);
                if (gradient > swingSharpness[x, y])
                {
                    listPoint.Add(new Data.Point(x, y));
                    swingSharpness[x, y] = gradient;
                }
            }
            solution.setValue(listPoint, image);
        }


        public void clarifySolution(Data.Image image, List<Data.Point> goodPoint)
        {
            if (swingSharpness == null)
            {
                swingSharpness = new double[image.width(), image.height()];
                for (int x = 0; x < image.width(); x++)
                    for (int y = 0; y < image.height(); y++)
                        swingSharpness[x, y] = 0;
                width = image.width();
                height = image.height();
            }

            List<Data.Point> listPoint = new List<Data.Point>();
            Bitmap curentImage = image.image;
            curentImage = changeImage.translateToMonochrome(curentImage);
            for (int i = 0; i < goodPoint.Count; i++)
            {
                int x = goodPoint[i].x;
                int y = goodPoint[i].y;
                matematical.setImage(curentImage);
                double gradient = matematical.gradientAtPoint(x, y);
                if (gradient > swingSharpness[x, y])
                {
                    listPoint.Add(new Data.Point(x, y));
                    swingSharpness[x, y] = gradient;
                }
            }
            solution.setValue(listPoint, image);
        }
    }
}

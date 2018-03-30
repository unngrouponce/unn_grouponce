using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
    public class CalculatedNull: ICalculated
    {
        Solution solution;

        public CalculatedNull() { }

        public void createdBeginSolution()
        {
            solution = new Solution();
        }

        public void clarifySolution(Data.Image image)
        {
            Random random = new Random();
            List<Point> listPoint = new List<Point>();
            //Задаем случайное количество точек попавших в фокус
            int countFocusPoint = random.Next(image.width(), image.width() * image.height());

            for (int i = 0; i < countFocusPoint; i++)
            {
                int x = random.Next(1, image.width());
                int y = random.Next(1, image.height());
                Point point = new Point(x, y);
                listPoint.Add(point);
            }

            solution.setValue(listPoint, image);
        }

        public Data.Solution getSolution()
        {
            return solution;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Класс для задания координаты пикселя
    /// </summary>
    public class Point
    {
        int _x;
        int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int x { get { return _x; } }
        public int y { get { return _y; } }
    }
}

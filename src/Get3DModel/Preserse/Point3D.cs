using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preserse
{
    class Point3D
    {
        private double _x;
        private double _y;
        private double _z;
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } set { _z = value; } }
        public Point3D(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

    }
}

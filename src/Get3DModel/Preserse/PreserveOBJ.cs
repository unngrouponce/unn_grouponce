using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preserse
{
    public class PreserveOBJ: IPreserveOBJ
    {
        public void saveOBJ(Data.Solution solution, Data.Setting setting, string path)
        {
            ObjWriter objWriter = new ObjWriter(path + "//result.obj");
            List<Point3D> listPoint3D = new List<Point3D>();
            for(int x=0; x<solution.sharpImage.Width; x++)
                for (int y=0; y<solution.sharpImage.Height; y++)
                    if (solution.getValue(x, y) != -1) listPoint3D.Add(new Point3D(x,y,solution.getValue(x,y)));
            objWriter.addPoints(listPoint3D);
        }
    }
}

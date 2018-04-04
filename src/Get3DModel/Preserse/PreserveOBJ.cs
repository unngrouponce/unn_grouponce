using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
                    if (solution.getValue(x, y) != -1)
                        listPoint3D.Add(
                            new Point3D(x/(double)solution.Width*setting.FWIDTH,
                                        y/(double)solution.Height*setting.FWIDTH,
                                        solution.getValue(x,y)*setting.HCOEFF));
            objWriter.addPoints(listPoint3D);
        }
        public static void saveDat(double[,] map, string path)
        {
            FileStream fs = new FileStream(path+"//test2.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, map);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preserse
{
    /// <summary>
    /// Класс для записи в obj файл
    /// </summary>
    class ObjWriter
    {
        private string fileName;
        /// <param name="filerName">имя файла для записи </param>
        public ObjWriter(string fileName)
        {
            this.fileName = fileName;
            StreamWriter _writer = new StreamWriter(fileName);
            _writer.Close();

        }
        /// <summary>
        /// добавляет точки в файл
        /// </summary>
        public void addPoints(List<Point3D> points)
        {
            StreamWriter _writer = new StreamWriter(fileName);
            foreach (var i in points)
            {
                _writer.WriteLine(string.Format("v {0} {1} {2}", i.x, i.y, i.z).Replace(',', '.'));
            }
            _writer.Close();
            Console.WriteLine("successful completed");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;


namespace ParsingInputData
{
    public interface IParser
    {
        /// <summary>
        /// Читает config файл и возвращает (переменная,значение)
        /// </summary>
        /// <param name="path">Путь к папке</param>
        Dictionary<string, double> readConfig(string path);
        /// <summary>
        /// Читает png файл и возвращает карту цветов
        /// </summary>
        /// <param name="path">Путь к папке</param>
       Bitmap readPNG(string path);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsingInputData;

namespace Data
{
    /// <summary>
    /// Сожержание конфигурационного файла в указанной директории, сохраняет параметры и изображения.
    /// </summary>
    public class Setting
    {
            private double _FDISTANCE;
            private double _FWIDTH;
            private double _HCOEFF;  //TODO

            /// <summary>
            /// Фокусное расстояние
            /// </summary>
            public double FDISTANCE { get { return _FDISTANCE; } }

            /// <summary>
            ///Наблюдаемая ширина в фокусе
            /// </summary>
            public double FWIDTH { get { return _FWIDTH; } }

            /// <summary>
            ///Коэффициент для вычисления абсолютной высоты фокуса
            /// </summary>
            public double HCOEFF { get { return _HCOEFF; } }

            public Setting(string pathConfig)
            {
            IParser parser = new Parser();
            Dictionary<string, double> mapSetting = parser.readConfig(pathConfig);
            

            if (!mapSetting.TryGetValue("FDISTANCE", out _FDISTANCE))
                    Console.WriteLine("Error: Not found setting FDISTANCE");

                if (!mapSetting.TryGetValue("FWIDTH", out _FWIDTH))
                    Console.WriteLine("Error: Not found setting FWIDTH");

                if (!mapSetting.TryGetValue("HCOEFF", out _HCOEFF))
                    Console.WriteLine("Error: Not found setting HCOEFF");
            }
        
    }
}

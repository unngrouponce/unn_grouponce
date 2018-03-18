using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Preserse
{
    public interface IPreserseOBJ
    {
        /// <summary>
        /// Расчет и сохранение OBJ файла
        /// </summary>
        /// <param name="solution">Расчитаное решение</param>
        /// <param name="setting">Настройки сиситемы</param>
        void saveOBJ(Solution solution, Setting setting);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Preserse
{
    public interface IPreservePNG
    {
        /// <summary>
        /// Сохранение OBJ файла
        /// </summary>
        /// <param name="solution">Расчитаное решение</param>
        void savePNG(Solution solution);
    }
}

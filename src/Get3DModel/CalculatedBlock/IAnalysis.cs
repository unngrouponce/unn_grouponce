using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
     /// <summary>
    /// Класс для подбора ядра для каждой точки
    /// </summary>
    public interface IAnalysis
    {
        /// <summary>
        /// Добавить изображение для анализа
        /// </summary>
        /// <param name="image">изображение анализа</param>
        void addImageAnalysis (Data.Image image);

        /// <summary>
        /// Получить ядра для каждой точки
        /// </summary>
        List<IMathematical> getCore();
    }
}

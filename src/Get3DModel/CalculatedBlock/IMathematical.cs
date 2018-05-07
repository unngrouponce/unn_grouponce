using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    /// <summary>
    /// Класс для математических решений
    /// </summary>
    public interface IMathematical
    {
        /// <summary>
        /// Установка изображения для расчетов
        /// </summary>
        void setImage(Bitmap image);

        /// <summary>
        /// Подсчет градиента в точке
        /// </summary>
        double gradientAtPoint(int x, int y);

        /// <summary>
        /// Установить порогое значение
        /// </summary>
        void setDeltaThreshold(double threshold);

        /// <summary>
        /// Получить порогое значение
        /// </summary>
        double getDeltaThreshold();

        /// <summary>
        /// Получить максимальное значение для ядра по X
        /// </summary>
        double getMaxValueX();

        /// <summary>
        /// Получить максимальное значение для ядра по Y
        /// </summary>
        double getMaxValueY();

    }
}

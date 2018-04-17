using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
    /// <summary>
    /// Класс для отсева точек не несущих достоверную информацию
    /// </summary>
    public interface IElimination
    {
        /// <summary>
        /// Подсчитать градиенты на картинке
        /// </summary>
        /// <param name="image">изображение для подсчета</param>
        void calculateGradientImage(Image image);

        /// <summary>
        /// Получить "хорошие" точки
        /// </summary>
        List<Data.Point> getSolution();
    }
}

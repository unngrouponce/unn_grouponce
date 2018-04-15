using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
    public interface ICalculated
    {
        /// <summary>
        /// Создать начальное решение
        /// </summary>
        void createdBeginSolution();

        /// <summary>
        /// Уточнить решение
        /// </summary>
        /// <param name="image">изображение для уточнения решения</param>
        void clarifySolution(Image image);

        /// <summary>
        /// Выполнить отсев
        /// </summary>
        /// <param name="delta">Процент</param>
        void eliminationPoints(double delta);

        /// <summary>
        /// Получить решение
        /// </summary>
        Solution getSolution();
    }
}

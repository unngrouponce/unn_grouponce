using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalculatedBlock
{
    /// <summary>
    /// Класс для обработки изображения
    /// </summary>
    public interface IChangeImage
    {
        /// <summary>
        /// Перевод изображения в монохром
        /// </summary>
        Bitmap translateToMonochrome(Bitmap image);
    }
}

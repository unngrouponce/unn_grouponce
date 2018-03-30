using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preserse
{
    public class PreservePNG: IPreservePNG
    {
        public void savePNG(Data.Solution solution, string path)
        {
            solution.sharpImage.Save(path + "//sharpImage.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}

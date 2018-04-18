using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
    public class Analysis: IAnalysis
    {
        List<Data.Point> pointAnalysis;

        public Analysis(List<Data.Point> point)
        {
            pointAnalysis = point;
        }

        public void addImageAnalysis(Image image)
        {
            throw new NotImplementedException();
        }

        public List<IMathematical> getCore()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace CalculatedBlock
{
    public class Calculated : ICalculated
    {
        Solution solution;

        public Calculated() { }

        public void createdBeginSolution()
        {
            solution = new Solution();
        }

        public void clarifySolution(Data.Image image)
        {
            //solution.setValue()
        }

        public Data.Solution getSolution()
        {
            throw new NotImplementedException();
        }
    }
}

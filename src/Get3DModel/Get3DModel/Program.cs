using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using ParsingInputData;
using Data;
using CalculatedBlock;
using Preserse;

namespace Get3DModel
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Parser();
            ICalculated calculated = new Calculated();
            IPreserseOBJ preserveOBJ = new PreserveOBJ();
            IPreservePNG preservePNG = new PreservePNG();
            Setting setting = null;

            List<string> filesInagesname;
            string pathFolder;
            string pathConfig;
            
            pathFolder = "";
            filesInagesname = Directory.GetFiles(pathFolder, "*.png").ToList<string>();

            pathConfig = pathFolder + "/config.ini";
            FileInfo fileInf = new FileInfo(pathConfig);
            if (fileInf.Exists)
                setting = new Setting(pathConfig);
            else
            {
                Console.WriteLine("the configuration file is not found");
                Environment.Exit(-1);
            }

            calculated.createdBeginSolution();

            for (int i = 0; i < filesInagesname.Count; i++)
            {
                Data.Image itemImage = new Data.Image(pathFolder + "/" + filesInagesname[i]);
                calculated.clarifySolution(itemImage);
            }

            Solution solution = calculated.getSolution();

            preserveOBJ.saveOBJ(solution, setting);
            preservePNG.savePNG(solution);
        }
    }
}

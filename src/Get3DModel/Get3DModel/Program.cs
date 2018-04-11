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
using System.Diagnostics;

namespace Get3DModel
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Parser();
            HashSet <string> CommandLineParams= new HashSet<string>(args);
            ICalculated calculated = new Calculated(); //используется класс MathematicalDefault 
            //ICalculated calculated = new Calculated(new MathematicalOption1()); 
            IPreserveOBJ preserveOBJ = new PreserveOBJ();
            IPreservePNG preservePNG = new PreservePNG();
            Setting setting = null;

            List<string> filesImagesname;
            string pathFolder;
            string pathConfig;
            if (args.Length == 0)
            { Console.WriteLine("usage: Get3DModel.exe <path to folder>"); Environment.Exit(-1); }
            pathFolder = args[0];
            filesImagesname = Directory.GetFiles(pathFolder, "*.png").ToList<string>();

            pathConfig = Directory.GetFiles(pathFolder).ToList().First(
                x => x.EndsWith(".camera") || x.EndsWith(".ini") || x.EndsWith("ConfigurationFile.txt"));
            FileInfo fileInf = new FileInfo(pathConfig);
            if (fileInf.Exists)
            {setting = new Setting(pathConfig);
             Console.WriteLine("the verification of the optics configuration file completed successfully");
            }
            else
            {
                Console.WriteLine("the configuration file is not found");
                Environment.Exit(-1);
            }

            calculated.createdBeginSolution();
            Stopwatch timeForParsing = new Stopwatch();
            for (int i = 0; i < filesImagesname.Count; i++)
            {
                if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                timeForParsing.Restart();
                Data.Image itemImage = new Data.Image(filesImagesname[i]);
                calculated.clarifySolution(itemImage);
                timeForParsing.Stop();
                Console.WriteLine(
                    string.Format("processing of the {0} has finished\n\telapsed time: {1} milliseconds",
                    filesImagesname[i], timeForParsing.Elapsed.Milliseconds));
            }

            Solution solution = calculated.getSolution();
            Console.WriteLine("saving data was started");
            preserveOBJ.saveOBJ(solution, setting, pathFolder);
           if(CommandLineParams.Contains("-d")) PreserveOBJ.saveDat(solution.Map, pathFolder);
            preservePNG.savePNG(solution, pathFolder);
           
        }
    }
}


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
            try
            {
                IParser parser = new Parser();
                ICalculated calculated = new Calculated(new MathematicialSearchPoint1());
                IPreserveOBJ preserveOBJ = new PreserveOBJ();
                IPreservePNG preservePNG = new PreservePNG();
                IElimination elimination = new Elimination();
                IAnalysis analysis;
                Setting setting = null;
                double delta = 0.0;

                List<string> filesImagesname;
                string pathFolder;
                string pathConfig = "";

                if (args.Length == 0)
                { Console.WriteLine("usage: Get3DModel.exe <path to folder>"); Environment.Exit(-1); }
                pathFolder = args[0];

                filesImagesname = Directory.GetFiles(pathFolder, "*.png").ToList<string>();
                if (filesImagesname.Count(x => !x.EndsWith("sharpImage.png")) < 3) { Console.WriteLine("image files not found"); Environment.Exit(-1); }
                if (args.Length > 1)
                    delta = Convert.ToDouble(args[1]);

                try
                {
                    pathConfig = Directory.GetFiles(pathFolder).ToList().First(
                    x => x.EndsWith(".camera") || x.EndsWith(".ini") || x.EndsWith("ConfigurationFile.txt"));
                }
                catch
                {
                    Console.WriteLine("the configuration file is not found");
                    Environment.Exit(-1);
                }
                setting = new Setting(pathConfig);
                Console.WriteLine("the verification of the optics configuration file completed successfully");

                Stopwatch timeForParsing = new Stopwatch();
                for (int i = 0; i < filesImagesname.Count; i++)
                {
                    if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                    Data.Image itemImage = new Data.Image(filesImagesname[i]);
                    elimination.calculateGradientImage(itemImage);
                }
                List<Data.Point> goodPoint = elimination.getSolution();

                analysis = new Analysis(goodPoint);
                for (int i = 0; i < filesImagesname.Count; i++)
                {
                    if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                    Data.Image itemImage = new Data.Image(filesImagesname[i]);
                    analysis.addImageAnalysis(itemImage);
                }
                List<IMathematical> coreGoodPoint = analysis.getCore();

                calculated.createdBeginSolution();
                for (int i = 0; i < filesImagesname.Count; i++)
                {
                    if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                    timeForParsing.Restart();
                    Data.Image itemImage = new Data.Image(filesImagesname[i]);
                    calculated.clarifySolution(itemImage, coreGoodPoint, goodPoint);
                    timeForParsing.Stop();
                    Console.WriteLine(
                        string.Format("processing of the {0} has finished\n\telapsed time: {1} milliseconds",
                        filesImagesname[i], timeForParsing.ElapsedMilliseconds));
                }
                Solution solution = calculated.getSolution();
                Console.WriteLine("saving data was started");
                preserveOBJ.saveOBJ(solution, setting, pathFolder);
                preservePNG.savePNG(solution, pathFolder);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                Console.WriteLine("incorrect operation of the program");
            }
        }
    }
}


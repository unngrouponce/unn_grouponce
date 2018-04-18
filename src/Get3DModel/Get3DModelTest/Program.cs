using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using ParsingInputData;
using Data;
using CalculatedBlock;
using Preserse;
using System.Diagnostics;

namespace Get3DModelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Parser();
            ICalculated calculated; 
            IPreserveOBJ preserveOBJ = new PreserveOBJ();
            IPreservePNG preservePNG = new PreservePNG();
            Setting setting = null;
            double delta = 0.0;

            List<string> filesImagesname;
            string pathFolder;
            string pathConfig;
            if (args.Length == 0)
            { Console.WriteLine("usage: Get3DModel.exe <path to folder>"); Environment.Exit(-1); }
            pathFolder = args[0];

            string[] separators = { "\\"};
            string[] words = pathFolder.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string saveFolder = words[0];
            for (int i = 1; i < words.Length - 1; i++)
                saveFolder = saveFolder + "\\" + words[i];
            saveFolder = saveFolder + "\\Resulst_" + words[words.Length-1];

            Directory.CreateDirectory(saveFolder);

            StreamWriter timeTxt = new StreamWriter(saveFolder+"\\time.txt");

            if (args.Length == 1)
            {
                Console.WriteLine("strategy is chosen MathematicialSearchPoint1");
                timeTxt.WriteLine("MathematicialSearchPoint1");
                calculated = new Calculated(new MathematicialSearchPoint1());
            }
            else
            {
                string strategy = args[1];
                switch (strategy)
                {
                    case "MathematicialSearchPoint1":
                        timeTxt.WriteLine("MathematicialSearchPoint1");
                        calculated = new Calculated(new MathematicialSearchPoint1());
                        break;
                    case "MathematicialSearchPoint2":
                        timeTxt.WriteLine("MathematicialSearchPoint2");
                        calculated = new Calculated(new MathematicialSearchPoint2());
                        break;
                    case "MathematicialSearchPoint3":
                        timeTxt.WriteLine("MathematicialSearchPoint3");
                        calculated = new Calculated(new MathematicialSearchPoint3());
                        break;
                    case "MathematicialSearchPoint4":
                        timeTxt.WriteLine("MathematicialSearchPoint4");
                        calculated = new Calculated(new MathematicialSearchPoint4());
                        break;
                    case "MathematicialSearchPoint5":
                        timeTxt.WriteLine("MathematicialSearchPoint5");
                        calculated = new Calculated(new MathematicialSearchPoint5());
                        break;
                    default:
                        timeTxt.WriteLine("MathematicialSearchPoint1");
                        calculated = new Calculated(new MathematicialSearchPoint1());
                        break;
                }
                if (args.Length > 2)
                {
                    delta = Convert.ToDouble(args[2]);
                }
            }

            string[] listFileFolder = Directory.GetDirectories(pathFolder);
            foreach (string folder in listFileFolder)
            {
                FileInfo fileInfFolder = new FileInfo(folder);
                string nameFolder = fileInfFolder.Name;
                string saveFolderCurrent = saveFolder + "\\" + nameFolder;

                Directory.CreateDirectory(saveFolderCurrent);

                filesImagesname = Directory.GetFiles(folder, "*.png").ToList<string>();

                pathConfig = Directory.GetFiles(folder).ToList().First(
                x => x.EndsWith(".camera") || x.EndsWith(".ini") || x.EndsWith("ConfigurationFile.txt"));

                FileInfo fileInf = new FileInfo(pathConfig);
                if (fileInf.Exists)
                    setting = new Setting(pathConfig);
                else
                {
                    Console.WriteLine("the configuration file is not found");
                    Environment.Exit(-1);
                }

                calculated.createdBeginSolution();
                Stopwatch timeForParsing = new Stopwatch();
                timeForParsing.Restart();
                for (int i = 0; i < filesImagesname.Count; i++)
                {
                    if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                    Data.Image itemImage = new Data.Image(filesImagesname[i]);
                    calculated.clarifySolution(itemImage);
                }
                calculated.eliminationPoints(delta);
                Solution solution = calculated.getSolution();
                timeForParsing.Stop();
                timeTxt.WriteLine(nameFolder + " - " + timeForParsing.Elapsed.Milliseconds);
                preserveOBJ.saveOBJ(solution, setting, saveFolderCurrent);
                preservePNG.savePNG(solution, saveFolderCurrent);
                saveDat(solution.Map, saveFolderCurrent);
            }
            timeTxt.Close();
        }

        public static void saveDat(double[,] map, string path)
        {
            FileStream fs = new FileStream(path + "//dataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, map);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}

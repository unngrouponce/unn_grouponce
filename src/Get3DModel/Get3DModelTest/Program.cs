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

namespace Get3DModelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Parser();
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

            string[] separators = { "\\"};
            string[] words = pathFolder.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string saveFolder = words[0];
            for (int i = 1; i < words.Length - 1; i++)
                saveFolder = saveFolder + "\\" + words[i];
            saveFolder = saveFolder + "\\Resulst_" + words[words.Length-1];

            Directory.CreateDirectory(saveFolder);

            StreamWriter timeTxt = new StreamWriter(saveFolder+"\\time.txt");
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
                Solution solution = calculated.getSolution();
                timeForParsing.Stop();
                timeTxt.WriteLine(nameFolder + " - " + timeForParsing.Elapsed.Milliseconds);
                preserveOBJ.saveOBJ(solution, setting, saveFolderCurrent);
                preservePNG.savePNG(solution, saveFolderCurrent);
            }
            timeTxt.Close();
        }
    }
}

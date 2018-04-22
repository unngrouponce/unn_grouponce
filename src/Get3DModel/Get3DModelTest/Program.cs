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
            ICalculated calculated; 
            IPreserveOBJ preserveOBJ = new PreserveOBJ();
            IPreservePNG preservePNG = new PreservePNG();
            IElimination elimination;
            IAnalysis analysis;
            Setting setting = null;
            INIManager manager;

            List<string> filesImagesname;
            string pathFolder;
            string pathConfig;
            string pathSetting;
            string saveFolder;

            if (args.Length == 0)
            { Console.WriteLine("usage: Get3DModel.exe <path to folder>"); Environment.Exit(-1); }
            pathFolder = args[0];

            pathSetting  = pathFolder + "\\setting.ini";

            manager = new INIManager(pathSetting);

            string[] separators = { "\\"};
            string[] words = pathFolder.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            saveFolder = words[0];
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
                Stopwatch timeForParsing = new Stopwatch();
                timeForParsing.Restart();

                string nameCoreElimination = manager.GetPrivateString("ELIMINATION", "core");
                IMathematical coreElimination = getMathematical(nameCoreElimination);
                if (coreElimination != null)
                {
                    string thresholdElimination = manager.GetPrivateString("ELIMINATION", "threshold");
                    if (thresholdElimination != "default")
                    {
                        double deltaTheshold = Convert.ToDouble(thresholdElimination);
                        coreElimination.setDeltaThreshold(deltaTheshold);
                    }
                    elimination = new Elimination(coreElimination);
                }
                else
                    elimination = new Elimination();

                for (int i = 0; i < filesImagesname.Count; i++)
                {
                    if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                    Data.Image itemImage = new Data.Image(filesImagesname[i]);
                    elimination.calculateGradientImage(itemImage);
                }
                List<Data.Point> goodPoint = elimination.getSolution();

                string boolSelectionCore = manager.GetPrivateString("SELECTION_CORE", "selection_core");
                if (boolSelectionCore == "true")
                {
                    string nameCore3x3 = manager.GetPrivateString("CORE", "3_core");
                    IMathematical core3x3 = getMathematical(nameCore3x3);
                    if (core3x3 == null)
                        core3x3 = new MathematicialSearchPoint1();

                    string nameCore5x5 = manager.GetPrivateString("CORE", "5_core");
                    IMathematical core5x5 = getMathematical(nameCore5x5);
                    if (core5x5 == null)
                        core5x5 = new MathematicialSearchPoint8();

                    string nameCore7x7 = manager.GetPrivateString("CORE", "7_core");
                    IMathematical core7x7 = getMathematical(nameCore5x5);
                    if (core7x7 == null)
                        core7x7 = new MathematicialSearchPoint9();

                    analysis = new Analysis(goodPoint, core3x3, core5x5, core7x7);
                    for (int i = 0; i < filesImagesname.Count; i++)
                    {
                        if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                        Data.Image itemImage = new Data.Image(filesImagesname[i]);
                        analysis.addImageAnalysis(itemImage);
                    }
                    List<IMathematical> coreGoodPoint = analysis.getCore();
                    calculated = new Calculated();
                    calculated.createdBeginSolution();
                    for (int i = 0; i < filesImagesname.Count; i++)
                    {
                        if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                        Data.Image itemImage = new Data.Image(filesImagesname[i]);
                        calculated.clarifySolution(itemImage, coreGoodPoint, goodPoint);
                    }
                }
                else
                {
                    string nameCore = manager.GetPrivateString("SELECTION_CORE", "default_core");
                    IMathematical core = getMathematical(nameCore);
                    if (core == null)
                        core = new MathematicialSearchPoint1();

                    calculated = new Calculated(core);
                    calculated.createdBeginSolution();
                    for (int i = 0; i < filesImagesname.Count; i++)
                    {
                        if (filesImagesname[i].EndsWith("sharpImage.png")) continue;
                        Data.Image itemImage = new Data.Image(filesImagesname[i]);
                        calculated.clarifySolution(itemImage, goodPoint);
                    }
                }

                Solution solution = calculated.getSolution();
                timeForParsing.Stop();
                timeTxt.WriteLine(nameFolder + " - " + timeForParsing.ElapsedMilliseconds + " timeForParsing");
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

        public static IMathematical getMathematical(string nameCore)
        {
            switch (nameCore)
            {
                case "MathematicialSearchPoint1":
                    return new MathematicialSearchPoint1();
                case "MathematicialSearchPoint2":
                    return new MathematicialSearchPoint2();
                case "MathematicialSearchPoint3":
                    return new MathematicialSearchPoint3();
                case "MathematicialSearchPoint4":
                    return new MathematicialSearchPoint4();
                case "MathematicialSearchPoint5":
                    return new MathematicialSearchPoint5();
                case "MathematicialSearchPoint6":
                    return new MathematicialSearchPoint6();
                case "MathematicialSearchPoint7":
                    return new MathematicialSearchPoint7();
                case "MathematicialSearchPoint8":
                    return new MathematicialSearchPoint8();
                case "MathematicialSearchPoint9":
                    return new MathematicialSearchPoint9();
                case "MathematicialSearchPoint10":
                    return new MathematicialSearchPoint10();
                case "MathematicialSearchPoint11":
                    return new MathematicialSearchPoint11();
            }
            return null;
        }
    }

    public class INIManager
    {
        //Конструктор, принимающий путь к INI-файлу
        public INIManager(string aPath)
        {
            path = aPath;
        }

        //Конструктор без аргументов (путь к INI-файлу нужно будет задать отдельно)
        public INIManager() : this("") { }

        //Возвращает значение из INI-файла (по указанным секции и ключу) 
        public string GetPrivateString(string aSection, string aKey)
        {
            //Для получения значения
            StringBuilder buffer = new StringBuilder(SIZE);

            //Получить значение в buffer
            GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

            //Вернуть полученное значение
            return buffer.ToString();
        }

        //Пишет значение в INI-файл (по указанным секции и ключу) 
        public void WritePrivateString(string aSection, string aKey, string aValue)
        {
            //Записать значение в INI-файл
            WritePrivateString(aSection, aKey, aValue, path);
        }

        //Возвращает или устанавливает путь к INI файлу
        public string Path { get { return path; } set { path = value; } }

        //Поля класса
        private const int SIZE = 1024; //Максимальный размер (для чтения значения из файла)
        private string path = null; //Для хранения пути к INI-файлу

        //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

        //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);
    }
}

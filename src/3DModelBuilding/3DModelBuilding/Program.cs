using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelBuilding
{
    class Program
    {
        static void Main(string[] args)
        {
            //string inputPath = args[0];
            //string outputPath = args[0];
            string path = args[0];
            int count = 0;
            foreach(var element in args)
            Console.WriteLine(element);
            if (verificationConfigurationData(path, ref count) && checkImages(path, count) && trySaveData(path)) { }
            Console.ReadKey();
        }

        private static bool verificationConfigurationData(string pathToDirectory, ref int count)
        {
            pathToDirectory += @"\ConfigurationFile.txt";
            //Console.WriteLine("the verification of the optics configuration file started...");
            try
            {
                using (FileStream fs = File.Open(pathToDirectory, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    count = Int32.Parse(System.Text.Encoding.Default.GetString(bytes));
                }
                Console.WriteLine("the verification of the optics configuration file completed successfully");
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("the configuration file is not found");
                return false;
            }
            catch (System.FormatException)
            {
                Console.WriteLine("the configuration data is incorrect");
                return false;
            }

            Console.WriteLine();
        }

        private static bool checkImages(string pathToDirectory, int count)
        {
            //Console.WriteLine("image verification started...");
            string[] files = Directory.GetFiles(pathToDirectory, "*.png");
            if (files.Length < count)
            {
                Console.WriteLine("image files not found");
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (!File.Exists(pathToDirectory + @"\" + i.ToString() + "-image.png"))
                {
                    Console.WriteLine("image file have incorrect named");
                    return false;
                }
            }
            Console.WriteLine("image verification completed successfully");
            Console.WriteLine();
            parseImage(files);
            return true;
        }

        private static bool trySaveData(string pathToDirectory)
        {
            Console.WriteLine("saving data was started...");
            try
            {
                using (FileStream fs = File.Create(pathToDirectory + @"\outputFile.obj"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("New .obj file was created.");
                    fs.Write(info, 0, info.Length);
                }
                
                Console.WriteLine("successful completed");
                return true;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("directory to save the result of the program ({0}.obj) missing", pathToDirectory);
                return false;
            }
        }

        private static void parseImage(string[] filesNames)
        {
            Stopwatch timeForParsing = new Stopwatch();
            for (int i = 0; i < filesNames.Length; i++)
            {
                timeForParsing.Restart();
                Console.WriteLine("processing of the {0}-th image is started...", i + 1);
                timeForParsing.Stop();
                Console.WriteLine("processing of the {0}-th image has finished      elapsed time: {1} milliseconds", 
                    i + 1, timeForParsing.Elapsed.Milliseconds);
                Console.WriteLine();
            }
        }
    }
}

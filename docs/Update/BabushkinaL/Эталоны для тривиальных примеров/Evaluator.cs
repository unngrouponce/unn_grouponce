using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Interpol3D;

namespace Evaluator3D
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[]
                {
                    "test1.dat",
                    "test2.dat",
                };
            if (args == null || args.Length != 2)
            {
                Console.Write("Command line format:\nEvaluator3D MODEL1.DAT MODEL2.DAT\nEvaluator3D MODEL.DAT FILE.OBJ\n");
                return;
            }

            if (args[0].ToUpper().EndsWith("DAT") &&
                args[1].ToUpper().EndsWith("DAT"))
            {
                //generator(args[0], args[1]);
                evaluate_dat_dat(args[0], args[1]);
                return;
            }
            Console.Write("Evaluation is impossible. Undefined extension.\n");
        }

        static void evaluate_dat_dat(string fname1, string fname2)
        {
            double[,] map = loadMap(fname1);
            double[,] any = loadMap(fname2);

            double err_max, err_sum;
            int any_pnt;
            if (!evaluateMaps(map, any, out err_max, out err_sum, out any_pnt))
            {
                Console.Write("Evaluation is impossible. These models have different sizes.\n");
                return;
            }
            Console.Write("Maximum еrror: {0:f3}\n", err_max);
            Console.Write("Total error: {0:f3}\n", err_sum);
            Console.Write("Map points: {0}\n", map.GetLength(0) * map.GetLength(1));
            Console.Write("Any points: {0}\n", any_pnt);

            int[] count = new int[] { 0, 0, 0, 0, 0, 0 };
            calculateUniform(any, 0, 0, 0, any.GetLength(0), any.GetLength(1), count);
            Console.Write("Uniform: {0:f3}%\n\n", 100 * evaluateUniform(count));

            detailsMaps(map, any, err_max);
        }

        static double[,] loadMap(string fname)
        {
            double[,] map = null;
            FileStream fs = new FileStream(fname, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                map = (double[,])formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return map;
        }

        static void saveMap(double[,] map, string fname)
        {
            FileStream fs = new FileStream(fname, FileMode.Create);
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

        static bool evaluateMaps(double[,] map, double[,] any, out double err_max, out double err_sum, out int any_pnt)
        {
            err_max = 0;
            err_sum = 0;
            any_pnt = 0;
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            if (width != any.GetLength(0)) return false;
            if (height != any.GetLength(1)) return false;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (any[i, j] < 0) continue;
                    any_pnt++;
                    double err = Math.Abs(map[i, j] - any[i, j]);
                    err_max = Math.Max(err_max, err);
                    err_sum += err;
                }
            return true;
        }

        static void detailsMaps(double[,] map, double[,] any, double err_max)
        {
            int charts = 10;
            int[] chart = new int[charts];
            for (int i = 0; i < charts; i++) chart[i] = 0;

            err_max += 0.0000001;
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (any[i, j] < 0) continue;
                    double err = Math.Abs(map[i, j] - any[i, j]);
                    int index = (int)(charts * err / err_max);
                    chart[index]++;
                }

            for (int i = 0; i < charts; i++)
            {
                Console.Write("errors {0:f}...{1:f}\t{2:f}%\n",
                    (i + 0) * err_max / charts,
                    (i + 1) * err_max / charts, 
                    100.0 * chart[i] / (width * height));
            }
        }

        static bool calculateUniform(double[,] any, int level, int x, int y, int w, int h, int[] count)
        {
            if (level < count.Length)
            {
                w /= 2;
                h /= 2;
                bool present = false;
                if (calculateUniform(any, level + 1, x + 0, y + 0, w, h, count)) { count[level]++; present = true; }
                if (calculateUniform(any, level + 1, x + w, y + 0, w, h, count)) { count[level]++; present = true; }
                if (calculateUniform(any, level + 1, x + w, y + h, w, h, count)) { count[level]++; present = true; }
                if (calculateUniform(any, level + 1, x + 0, y + h, w, h, count)) { count[level]++; present = true; }
                return present;
            }
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if (any[x + i, y + j] >= 0) return true;
            return false;
        }

        static double evaluateUniform(int[] count)
        {
            double result = 0;
            int total = 1;
            for (int i = 0; i < count.Length; i++)
            {
                total *= 4;
                result += ((double)count[i] / total) / count.Length;
            }
            return result;
        }
    }
}

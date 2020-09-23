using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static List<string> PathList = new List<string>();
        public static List<string> RotatedList = new List<string>();
        double RasstoalieMezdyM = 0.0;
        public static double ugol = 5.0*Math.PI/3.0;
        static async Task Main(string[] args)
        {
            string path = Environment.CurrentDirectory + @"\map.svg";
            bool isEnd = true;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string c = sr.ReadLine();
                        bool contains = c.Contains("<path style=\"stroke:none;\" d=\"M");
                        if (contains && isEnd)
                        {
                           // ReversLocations(c);
                            string str = RevertUgol(c);
                            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\path.svg", true, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(str);
                            }

                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\path.svg", true, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(c);
                            }
                        }
                        if (c.Contains(@"image"))
                        {
                            isEnd = false;
                        }
                    }
                    Console.WriteLine("End");
                }
            }
            catch (Exception)
            {

            }
        }
        static string RevertUgol(string strg)
        {
            string result = string.Empty;
            double DFirst = 0.0;
            double DSecond = 0.0;
            string forFirst = string.Empty;
            string forSecond = string.Empty;
            int numOfCases = 0;
            bool flagSecond = false;
            for (int i = 0; i < strg.Length; i++)
            {
                char value = strg[i];
                if (numOfCases != 3)
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                    result += strg[i];
                }
                else
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                    if (strg[i] != '-' && strg[i] != '.' && strg[i] != '1' && strg[i] != '2' && strg[i] != '3' && strg[i] != '4' && strg[i] != '5' && strg[i] != '6' && strg[i] != '7' && strg[i] != '8' && strg[i] != '9' && strg[i] != '0')
                    {
                        result += strg[i];
                    }
                    else
                    {
                        if (!flagSecond)
                        {
                            forFirst += strg[i];
                            if (strg[i + 1] == ' ')
                            {
                                flagSecond = true;
                                continue;
                            }
                        }
                        if (flagSecond)
                        {
                            forSecond += strg[i];
                            if (strg[i + 1] == ' ')
                            {
                                DFirst = Double.Parse(forFirst, System.Globalization.CultureInfo.InvariantCulture);
                                DSecond = Double.Parse(forSecond, System.Globalization.CultureInfo.InvariantCulture);
                                double firstVal = DFirst * Math.Cos(ugol) + DSecond * Math.Sin(ugol);
                                double secondVal = -DFirst * Math.Sin(ugol) + DSecond * Math.Cos(ugol);
                                flagSecond = false;
                                result += String.Format("{0:F20}", firstVal).Replace(',', '.');
                                result += " ";
                                result += String.Format("{0:F20}", secondVal).Replace(',', '.');
                                forFirst = string.Empty;
                                forSecond = string.Empty;
                                continue;
                            }
                        }
                    }
                }
            }
            return result;
        }
        
        public static void ReversLocations(string currentString)
        {
           double DopustRasstoanieMezdyTochkami = 5.0;
           int CurrentPosition = 0;
           if(true)
            {
                if(PathList.Count==0)
                {
                    PathList.Add(currentString);
                }
                else
                {
                    double CurrentRasstoanie = GetRasstoanie(GetMPoint(currentString), GetMPoint(PathList.Last()));
                    if (CurrentRasstoanie == 2.3898325197821686)
                    {
                        RotateWords();
                        PathList = new List<string>();
                        PathList.Add(currentString);
                    }
                    else
                    {
                        PathList.Add(currentString);
                    }
                }
            }
        }
        public static double[] GetMPoint(string currentString)
        {
            double[] point = new double[] { 0.0, 0.0 };
            bool MisCurrent = false;
            bool flagSecond = false;
            string result = String.Empty;
            string forFirst = string.Empty;
            string forSecond = string.Empty;
            int numOfCases = 0;
            for (int i = 0; i < currentString.Length; i++)
            {
                char value = currentString[i];
                if (numOfCases != 3)
                {
                    if (currentString[i] == '"')
                    {
                        numOfCases++;
                    }
                }
                else
                {
                    if ( currentString[i] == '-' || currentString[i] == '.' || currentString[i] == '1' || currentString[i] == '2' || currentString[i] == '3' || currentString[i] == '4' || currentString[i] == '5' || currentString[i] == '6' || currentString[i] == '7' && currentString[i] == '8' || currentString[i] == '9' || currentString[i] == '0')
                    {
                        if (!flagSecond)
                        {
                            char charing = currentString[i];
                            forFirst += currentString[i];
                            if (currentString[i + 1] == ' ')
                            {
                                flagSecond = true;
                                continue;
                            }
                        }
                        if (flagSecond)
                        {
                            forSecond += currentString[i];
                            if (currentString[i + 1] == ' ')
                            {
                                point[0] = Double.Parse(forFirst, System.Globalization.CultureInfo.InvariantCulture);
                                point[1] = Double.Parse(forSecond, System.Globalization.CultureInfo.InvariantCulture);
                                flagSecond = false;
                                break;
                            }
                        }
                    }
                }
            }
            return point;
        }
        public static double GetRasstoanie(double[] p1, double[] p2)
        {
            double x1 = p1[0];
            double y1 = p1[1];
            double x2 = p2[0];
            double y2 = p2[1];
            Console.WriteLine("Иксы: " + x1 + " - " + x2 +" игреки " +  y1 + " - " + y2);
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
        public static void RotateWords()
        {
            string center = PathList.ElementAt(PathList.Count / 2 + 1);
            double[] CenterCoordinats = GetCoordinatesCenter(center);

            double[] normalcooficent=new double[2];
            normalcooficent = GetNormaleCooficent(CenterCoordinats);
            foreach(string str in PathList)
            {
                if(str!=center)
                {
                    RotatedList.Add(RotateItem(str, normalcooficent));
                }
            }

        }

        private static string RotateItem(string str, double[] normalcooficent)
        {
            throw new NotImplementedException();

        }

        private static double[] GetNormaleCooficent(double[] centerCoordinats)
        {
            if(ugol==Math.PI/2.0 || ugol==3.0*Math.PI/2.0)
            {
                //get to ctg
            }
            if(ugol<Math.PI/2.0 || ugol>3.0 * Math.PI / 2.0)
            {
                //ony get to line
            }
            if (ugol > Math.PI / 2.0 && ugol < 3.0 * Math.PI / 2.0)
            {
                //get to line
                //rotate
            }
            throw new NotImplementedException();
        }
        private static double[] GetCoordinatesCenter(string strg)
        {
            double[] center = new double[2];
            string result = string.Empty;
            string forFirst = string.Empty;
            string forSecond = string.Empty;
            int numOfCases = 0;
            bool flagSecond = false;
            for (int i = 0; i < strg.Length; i++)
            {
                char value = strg[i];
                if (numOfCases != 3)
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                    result += strg[i];
                }
                else
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                    if (strg[i] != '-' || strg[i] != '.' || strg[i] != '1' || strg[i] != '2' || strg[i] != '3' || strg[i] != '4' || strg[i] != '5' || strg[i] != '6' || strg[i] != '7' || strg[i] != '8' || strg[i] != '9' || strg[i] != '0')
                    {
                        if (!flagSecond)
                        {
                            forFirst += strg[i];
                            if (strg[i + 1] == ' ')
                            {
                                flagSecond = true;
                                continue;
                            }
                        }
                        if (flagSecond)
                        {
                            forSecond += strg[i];
                            if (strg[i + 1] == ' ')
                            {
                                center[0] = Double.Parse(forFirst, System.Globalization.CultureInfo.InvariantCulture);
                                center[1] = Double.Parse(forSecond, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        }
                    }
                }
            }
            return center;
        }
        public static string GetM(string strg)
        {
            string result = string.Empty;
            string forFirst = string.Empty;
            string forSecond = string.Empty;
            int numOfCases = 0;
            for (int i = 0; i < strg.Length; i++)
            {
                char value = strg[i];
                if (numOfCases != 3)
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                }
                else
                {
                    if (strg[i] == '"')
                    {
                        numOfCases++;
                    }
                    if (strg[i] == 'M' || strg[i] == 'Z' || strg[i] == 'C' || strg[i] == 'A' || strg[i] == 'Q')
                    {
                        result += strg[i];
                    }

                }
            }
            return result;
        }
    }
}

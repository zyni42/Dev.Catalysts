using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CCC_41_Rathaus
{
	internal class Program
	{

        const string levelRootPathZyni = @"C:\Users\User42\Downloads\CCC-Levels\";
        const string levelRootPathLeuchty = @"C:\Users\valen\source\repos\zyni42\Dev.Catalysts\CCC\CCC-41-Rathaus (14.11.2025)\Levels";
        const int LEVEL = 1;
        static string[] SUBLEVELS = { "small", "large" };
        const bool EXAMPLE = true;
        static void Main(string[] args)
        {
            if (EXAMPLE)
            {
                string[] rawInputData = File.ReadAllLines($"{levelRootPathLeuchty}\\level{LEVEL}\\level{LEVEL}_0_example.in");
                StreamWriter outputFile = new StreamWriter($"{levelRootPathLeuchty}\\level{LEVEL}\\level{LEVEL}_0_my_example.out");
                Level.Solve1(rawInputData, outputFile);
                outputFile.Flush();
                outputFile.Close();
                return;
            }
            else
            {
                foreach (string subl in SUBLEVELS)
                {
                    string[] rawInputData = File.ReadAllLines($"{levelRootPathLeuchty}\\level{LEVEL}\\level{LEVEL}_{Array.IndexOf(SUBLEVELS, subl)}_{subl}.in");
                    StreamWriter outputFile = new StreamWriter($"{levelRootPathLeuchty}\\level{LEVEL}\\level{LEVEL}_{Array.IndexOf(SUBLEVELS, subl)}_{subl}.out");
                    Console.WriteLine(subl);
                    outputFile.AutoFlush = true;
                    Level.Solve1(rawInputData, outputFile);
                    outputFile.Flush();
                    outputFile.Close();
                }

            }
        }
    }
}

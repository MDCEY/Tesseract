using System;
using Workshop;

namespace Tesseract
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            string engineer = args[0];
            Console.WriteLine(Workshop.Team.RepairsToday());
            Console.WriteLine(Workshop.Engineer.RepairsToday(engineer));
            Console.WriteLine(Workshop.Engineer.RepairedWorkingTime(engineer));
        }
    }
}
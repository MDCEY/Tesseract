using System;
using Workshop;
using CommandLine;

namespace Tesseract
{
    internal class Program
    {
        public class Options
        {
            
        }

        [Verb("repairs", HelpText = "Fetch your stats")]
        public class RepairOptions
        {
            [Option('u', "User", Default = "all")]
            public string User { get; set; }
        }    
        
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<RepairOptions>(args)
                .MapResult(
                    (RepairOptions opts) => GetRepairs(opts),
                    errs => 1);
        }

        private static int GetRepairs(RepairOptions args)
        {
            Console.WriteLine($"Getting stats for {args.User}");
            if (args.User == "all")
            {
                Console.WriteLine($"{Team.RepairsToday()} completed repairs");
            }
            else
            {
                Console.WriteLine($"{Workshop.Engineer.RepairsToday(args.User)} completed repairs");
                Console.WriteLine($"{Workshop.Engineer.RepairedWorkingTime(args.User)} spent on repaired items");
            }
            return 0;
        }
    }
}
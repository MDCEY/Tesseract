using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using TemplateEngine.Docx;
using Workshop;

namespace Tesseract
{
    internal class Program
    {
        public class Options
        {
            
        }
        [Verb("ber", HelpText = "Fetch Ber info for call number")]
        public class BerOptions
        {
            [Option('c', "CallNum")] public string Call { get; set; }
        }
        
        [Verb("average", HelpText = "Read averages of various things")]
        public class AverageOptions
        {
            [Option('t', "TimeToRepair")]
            public string Product { get; set; }
        }

        [Verb("stock", HelpText = "read your current stock")]
        public class StockOptions
        {
            [Option('u', "User")]
            public string User { get; set; }

        }

        [Verb("repairs", HelpText = "Fetch your stats")]
        public class RepairOptions
        {
            [Option('u', "User", Default = "all")]
            public string User { get; set; }
        }    
        
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<RepairOptions, StockOptions, AverageOptions, BerOptions>(args)
                .MapResult(
                    (RepairOptions opts) => GetRepairs(opts),
                    (StockOptions opts) => GetStock(opts),
                    (AverageOptions opts) => GetRepairAverage(opts),
                    (BerOptions opts) => WriteBerDetails(opts),
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
                Console.WriteLine($"{Engineer.RepairsToday(args.User)} completed repairs");
                Console.WriteLine($"{Engineer.RepairedWorkingTime(args.User)} spent on repaired items");
            }
            return 0;
        }

        private static int GetStock(StockOptions args)
        {
            foreach (KeyValuePair<string,string> entry in Engineer.CurrentStock(args.User))
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }

            return 0;
        }

        private static int GetRepairAverage(AverageOptions args)
        {
            Console.WriteLine(Parts.AverageRepairTime(args.Product));
            return 0;
        }

        private static int WriteBerDetails(BerOptions args)
        {    
            Dictionary<string, string> details = Other.BerDetails(args.Call);
            Console.WriteLine(details["RO Number"]);
            
            File.Copy(@"BerTemplate.docx", $@"{details["RO Number"].Trim()}.docx");
            var docValues = new Content(
                new FieldContent("Engineer Name", details["Engineer Name"] ),
                new FieldContent("BER Date", details["BER Date"] ),
                new FieldContent("MaterialNumber", details["MaterialNumber"] ),
                new FieldContent("Serial Number",details["Serial Number"] ),
                new FieldContent("RO Number", details["RO Number"] ),
                new FieldContent("Call Number",details["Call Number"] ),
                new FieldContent("Service Report",details["Service Report"] )
            );
            

            using (var outputDoc = new TemplateProcessor($@"{details["RO Number"].Trim()}.docx")
                .SetRemoveContentControls(true))
            {
                outputDoc.FillContent(docValues);
                outputDoc.SaveChanges();
            }

            return 0;
        }
    }
}
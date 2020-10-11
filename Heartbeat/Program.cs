using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Heartbeat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<string> errorLine = new List<string>();
            string resultTxt = string.Empty;
            string ErrorTxt = string.Empty;
            List<string> resultLine = new List<string>();
            PhoneticGenerator generator = new PhoneticGenerator();
            NameMapper nameMapper = new NameMapper();
            string completeInfo = string.Empty;

            /*doc format , just do the mapping
            * abandonment 英 [əˈbændənmənt]
            */
            if (args.Length > 0 && args[0] == "partial")
            {
                resultTxt = "result_partial.txt";
                ErrorTxt = "error_partial.txt";

                List<string> allLines = File.ReadLines("sourceText.list", Encoding.UTF8).ToList();
                resultLine = nameMapper.NameMappingPartial(allLines, ref errorLine);
                completeInfo = "partial";
            }
            else
            {
                resultTxt = "result.txt";
                ErrorTxt = "error.txt";
                List<string> deriveWords = generator.DeriveWords(ref errorLine);
                resultLine = nameMapper.NameMapping(deriveWords, ref errorLine);
                completeInfo = "full";
            }


            Utils.DeleteFile(resultTxt);
            Utils.DeleteFile(ErrorTxt);

            File.WriteAllLines(ErrorTxt, errorLine, Encoding.UTF8);

            File.WriteAllLines(resultTxt, resultLine, Encoding.UTF8);

            Console.WriteLine($"complete ! {completeInfo}");
        }
    }
}
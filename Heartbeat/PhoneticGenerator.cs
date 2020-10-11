using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Heartbeat
{
    public class PhoneticGenerator
    {
        public List<string> DeriveWords(ref List<string> invalidLines)
        {
            IEnumerable<string> allLines =
                File.ReadLines("sourceText.list",Encoding.UTF8);
            List<string> newline = new List<string>();
            foreach (string line in allLines)
            {
                string[] point = {"英", "美"};
                string[] lineContext = line.Split(point, StringSplitOptions.RemoveEmptyEntries);
                if (ValidateLine(lineContext))
                {
                    string word = lineContext[0].Trim();
                    string ukPhonetic = lineContext[1].Trim();
                    string usPhonetic = lineContext[2].Trim();
                    newline.AddRange(GenerateNewLines(word, ukPhonetic, point[0])); //uk
                    newline.AddRange(GenerateNewLines(word, usPhonetic, point[1])); //us
                }
                else
                {
                    newline.Add(line);
                    invalidLines.Add(line);
                }
            }

            return newline;
        }

        private List<string> GenerateNewLines(string word, string phonetic, string country)
        {
            List<string> newLinesForOnePhonetic = new List<string>();
            string[] phoneticStrs = phonetic.Split(',');
            foreach (var phoneticStr in phoneticStrs)
            {
                string str = phoneticStr.Replace(" ","").Trim();
                List<string> phoneticsByR = GenerateNewLinesByBracket(str);
                List<string> newlines = phoneticsByR.Select(x =>
                    $"{word} {country} {addBrackets(x)}").ToList();
                newLinesForOnePhonetic.AddRange(newlines);
            }

            return newLinesForOnePhonetic;
        }


        private List<string> GenerateNewLinesByBracket(string phoneticUnit) /*snɪə(r)*/
        {
            if (phoneticUnit.Contains("(")&& phoneticUnit.Contains(")"))
            {
                int startIndex = phoneticUnit.IndexOf('(');
                int endIndex = phoneticUnit.IndexOf(')');
                int len = endIndex - startIndex;
                string subStr = phoneticUnit.Substring(startIndex, len+1);
                return new List<string> {phoneticUnit.Replace(subStr, ""),
                    phoneticUnit.Replace("(", "").Replace(")", "")};
            }

            return new List<string> {phoneticUnit};
        }
        
        private string addBrackets(string str)
        {
            if (!str.Contains('['))
            {
                str = "[" + str;
            }

            if (!str.Contains(']'))
            {
                str = str + "]";
            }

            return str;
        }

        private bool ValidateLine(string[] oneLineContext)
        {
            return oneLineContext.Length == 3 && oneLineContext[1].Trim().StartsWith('[')
                                              && oneLineContext[2].Trim().StartsWith('[');
        }
    }
}
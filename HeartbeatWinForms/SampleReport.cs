using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeartbeatWinForms
{
    public class SampleReport
    {
        public List<string> GenerateEdReport(RichTextBox richTextBox1, List<string> allLines)
        {
            var dictionaryLines = File.ReadAllLines("Oxford English Dictionary.txt").ToList();
            var allVerbLIst = dictionaryLines.FindAll(x => x.Contains(" —v. ") || x.Contains(" v. "));
            for (int i = 0; i < allLines.Count; i++)
            {
                string verbEd = allLines[i];
                string verbOriginal = string.Empty;
                if (isValidVerb(allVerbLIst, verbEd.ToLower(), out verbOriginal))
                {
                    richTextBox1.AppendText($"{verbEd} is fined and the original is {verbOriginal} \r\n");
                    allLines[i] = $"{verbEd} *{verbOriginal}";
                }
                richTextBox1.AppendText($"{allLines[i]} didn't match.\r\n");
            }
            return allLines;
        }

        // check the key word is in our verb list or not
        private bool isValidVerb(List<string> allVerbList, string edKeyWords, out string originalVerb)
        {
            bool res = false;
            string str = "";
            foreach (var verbLine in allVerbList)
            {
                if (string.IsNullOrWhiteSpace(verbLine))
                {
                    continue;
                }

                string[] t = verbLine.Split(" ");
                string orVerb = t[0].ToLower();
                if (string.IsNullOrWhiteSpace(orVerb))
                {
                    continue;
                }

                if (edKeyWords == GenerateEdVerbByOriginalVerb(orVerb))
                {
                    str = orVerb;
                    res = true;
                    break;
                }
            }
            
            originalVerb = str;
            return res;
        }

        private string GenerateEdVerbByOriginalVerb(string originalVerb)
        {
            if (originalVerb.EndsWith("e"))
            {
                return $"{originalVerb}d";
            }

            return $"{originalVerb}ed";
        }
    }
}
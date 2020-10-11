using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var verbDict = GetValidVerbFromDictionary();
            List<string> result = new List<string>();
            var myLines = File.ReadAllLines(@"-ing-200206.word.all.list");
            foreach (var myLine in myLines)
            {
                if (verbDict.ContainsKey(myLine))
                {
                    result.Add($"{myLine},Yes,{verbDict[myLine]}");
                }
                else
                {
                    result.Add(myLine);
                }
            }

            if (File.Exists(@"result.txt"))
            {
                File.Delete(@"result.txt");
            }

            File.WriteAllLines(@"result.txt", result);
            Console.WriteLine("complete!");
        }

        //<verb-ing,verb>
        static Dictionary<string, string> GetValidVerbFromDictionary()
        {
            var dictLines = File.ReadAllLines(@"Oxford English Dictionary.txt");

            Dictionary<string, string> verbDict = new Dictionary<string, string>();
            Regex ingRegex = new Regex("^\\(-\\Ding\\)$");

            foreach (var line in dictLines)
            {
                if ((line.Contains(" v. ")||line.Contains(" —v. ")) && line.Contains("ing) "))
                {
                    string[] strs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine($"***{line}*****");
                    string verb = strs[0].ToLower();
                    string ingFormat = GetIngFormat(strs, ingRegex); //"ving"
                    if (!string.IsNullOrEmpty(ingFormat))
                    {
                        Console.WriteLine($"ingformat:{ingFormat}");
                        string verbing = $"{verb.Substring(0, verb.LastIndexOf(ingFormat[0]))}{ingFormat}";
                        verbDict.TryAdd(verbing, verb);
                    }
                    
                    
                    
                    
                }
            }

            return verbDict;
        }

        static string GetIngFormat(string[] strs, Regex ingRegex)
        {
            string ingFormat = String.Empty;
            foreach (var str in strs)
            {
                if (ingRegex.IsMatch(str))
                {
                    ingFormat = str.Replace("(", "").Replace(")", "").Replace("-", "");
                    break;
                }
            }

            return ingFormat;
        }
    }
}
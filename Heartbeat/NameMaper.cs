using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Heartbeat
{
    public class NameMapper
    {
        private List<string> _ukMapping = File.ReadLines("Mapping_Table_UK.txt").ToList();

        private List<string> _threeCharts_uk = null;
        private List<string> _twoCharts_uk = null;
        private List<string> _oneCharts_uk = null;

        private List<string> _threeCharts_us = null;
        private List<string> _twoCharts_us = null;
        private List<string> _oneCharts_us = null;

        private List<string> ThreeCharts_uk
        {
            get
            {
                if (_threeCharts_uk == null)
                {
                    return this._ukMapping.Where(x => x.Split(',')[0].Length >= 3).ToList();
                }

                return _threeCharts_uk;
            }
        }

        private List<string> TwoCharts_uk
        {
            get
            {
                if (_twoCharts_uk == null)
                {
                    return this._ukMapping.Where(x => x.Split(',')[0].Length == 2).ToList();
                }

                return _twoCharts_uk;
            }
        }

        private List<string> OneCharts_uk
        {
            get
            {
                if (_oneCharts_uk == null)
                {
                    return this._ukMapping.Where(x => x.Split(',')[0].Length == 1).ToList();
                }

                return _oneCharts_uk;
            }
        }

        private List<string> ThreeCharts_us
        {
            get
            {
                if (_threeCharts_us == null)
                {
                    return this._usMapping.Where(x => x.Split(',')[0].Length >= 3).ToList();
                }

                return _threeCharts_us;
            }
        }

        private List<string> TwoCharts_us
        {
            get
            {
                if (_twoCharts_us == null)
                {
                    return this._usMapping.Where(x => x.Split(',')[0].Length == 2).ToList();
                }

                return _twoCharts_us;
            }
        }

        private List<string> OneCharts_us
        {
            get
            {
                if (_oneCharts_us == null)
                {
                    return this._usMapping.Where(x => x.Split(',')[0].Length == 1).ToList();
                }

                return _oneCharts_us;
            }
        }

        private Random random = new Random();


        private readonly IEnumerable<string> _usMapping = File.ReadLines("Mapping_Table_US.txt");

        public List<string> NameMappingPartial(List<string> originalLines, ref List<string> errorList)
        {
            List<string> newLines = new List<string>();
            originalLines.ForEach(x =>
            {
                if (!newLines.Contains(x))
                {
                    newLines.Add(x);
                }
            });

            return NameMapping(newLines, ref errorList);
        }


        public List<string> NameMapping(List<string> originalLines, ref List<string> errorList)
        {
            List<string> resultLines = new List<string>();
            int count = 0;
            foreach (string line in originalLines)
            {
                count++;
                if (!ValidateStr(line))
                {
                    errorList.Add(line);
                    resultLines.Add(line);
                    continue;
                }

                string[] words = line.Split(' ');
                if (line.Contains("英"))
                {
                    string newline = MappingUkByLine(words, line);
                    resultLines.Add(newline);
                    Console.WriteLine(count + ")" + newline);
                }
                else
                {
                    var newMultipleLine = MappingUsByLine(words, line, ref errorList);
                    resultLines.AddRange(collection: newMultipleLine);
                    foreach (string str in newMultipleLine)
                    {
                        Console.WriteLine(count + ")" + str);
                    }
                }
            }

            return resultLines;
        }

        private string MappingUkByLine(string[] words, string line)
        {
            //slaughter 英 [ˈslɔ:tər] / s l ao t ax r /
            string originalIphonetic = removeBracket(words);
            string[] unit = originalIphonetic.Split(new[] {"ˌ", "ˈ", "'"}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> addressMapping = new Dictionary<string, string>();

            string phoneticAddress = string.Empty;
            foreach (string cell in unit)
            {
                //cellmapping: uniqueId, iphoneticmapping
                string cellAddress = ChartsReplace(cell, ThreeCharts_uk, ref addressMapping);
                cellAddress = ChartsReplace(cellAddress, TwoCharts_uk, ref addressMapping);
                cellAddress = ChartsReplace(cellAddress, OneCharts_uk, ref addressMapping);
                phoneticAddress = phoneticAddress + cellAddress;
            }

            var name = GerenateNameByAddress(phoneticAddress, addressMapping).Replace("'", "");

            return $"{line} / {name} /".Replace("/  ", "/ ");
        }

        private IEnumerable<string> MappingUsByLine(string[] words, string line, ref List<string> errorList)
        {
            //snobbish 美 [ˈsnɑbɪʃ] / s n aa b ih sh /
            string originalIphonetic = removeBracket(words);
            string[] unit = originalIphonetic.Split(new[] {'ˌ', 'ˈ'}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> addressMapping = new Dictionary<string, string>();

            string phoneticAddress = String.Empty;
            foreach (string cell in unit)
            {
                string cellAddress = ChartsReplace(cell, ThreeCharts_us, ref addressMapping);
                cellAddress = ChartsReplace(cellAddress, TwoCharts_us, ref addressMapping);
                cellAddress = ChartsReplace(cellAddress, OneCharts_us, ref addressMapping);
                phoneticAddress = phoneticAddress + cellAddress;
            }


            /* u,u:,uw|y uw    ɔ,oh|ao  */
            if (phoneticAddress.Contains("u") && phoneticAddress.Contains("ɔ"))
            {
                errorList.Add(line);
                return new List<string> {$"{line}  u and ɔ happened at same time :{phoneticAddress}"};
            }

            if (phoneticAddress.Contains("u"))
            {
                string phoneticAddress1 = ChartsReplace(phoneticAddress, new List<string> {"u,uw"}, ref addressMapping);
                string phoneticAddress2 =
                    ChartsReplace(phoneticAddress, new List<string> {"u,y uw"}, ref addressMapping);
                string name1 = GerenateNameByAddress(phoneticAddress1, addressMapping).Replace(":", "");
                string name2 = GerenateNameByAddress(phoneticAddress2, addressMapping).Replace(":", "");
                return new List<string>
                {
                    $"{line} / {name1} /".Replace("/  ", "/ "),
                    $"{line} / {name2} /".Replace("/  ", "/ ")
                };
            }

            if (phoneticAddress.Contains("ɔ"))
            {
                string phoneticAddress1 = ChartsReplace(phoneticAddress, new List<string> {"ɔ,ao"}, ref addressMapping);
                string phoneticAddress2 = ChartsReplace(phoneticAddress, new List<string> {"ɔ,oh"}, ref addressMapping);
                string name1 = GerenateNameByAddress(phoneticAddress1, addressMapping);
                string name2 = GerenateNameByAddress(phoneticAddress2, addressMapping);
                return new List<string>
                {
                    $"{line} / {name1} /".Replace("/  ", "/ "),
                    $"{line} / {name2} /".Replace("/  ", "/ ")
                };
            }

            if (IsAllNumber(phoneticAddress))
            {
                errorList.Add(line);
                return new List<string> {$"{line}  still have phonetic for phoneticAddress:{phoneticAddress}"};
            }

            string name = GerenateNameByAddress(phoneticAddress, addressMapping).Replace("'", " ");
            return new List<string> {$"{line} / {name} /".Replace("/  ", "/ ")};
        }

        private string GerenateNameByAddress(string phoneticAddress, Dictionary<string, string> cellMapping)
        {
            string name = phoneticAddress;
            foreach (string address in cellMapping.Keys)
            {
                string mapValue = cellMapping[address].Split(',')[1];
                name = name.Replace(address, $"[{mapValue}]");
            }

            name = name.Replace("][", " ").Replace("[", "").Replace("]", "");
            return name;
        }

        private static string removeBracket(string[] words)
        {
            return words[2].Replace("[", "").Replace("]", "").Trim();
        }

        private string ChartsReplace(string cell, List<string> mapList, ref Dictionary<string, string> cellMapping)
        {
            string str = cell;
            foreach (string mappingName in mapList)
            {
                string uniqueId = $" {DateTime.Now.Ticks.ToString()}{random.Next(10000000, 99999999).ToString()}";
                string iphonetic = mappingName.Split(',')[0];
                if (str.Contains(iphonetic))
                {
                    cellMapping.Add(uniqueId, mappingName);
                    str = str.Replace(iphonetic, $"[{uniqueId}]");
                }
            }

            return str;
        }


        private bool ValidateStr(string line)
        {
            string[] word = line.Split(' ');
            return word.Length == 3 &&
                   (word[1].Contains("美") || word[1].Contains("英")) &&
                   !word[2].Contains('-');
        }

        private bool IsAllNumber(string str)
        {
            var charArray = str.ToCharArray();
            foreach (var c in charArray)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day4
    {
        public static void Solve(string inputPath, bool isP2 = false)
        {
            if (Validate())
            {
                List<string> input = HelperFunctions.ReadFile(inputPath);
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 2.");
            }
        }


        public static void ParseStringsIntoSortedRecords(List<string> input)
        {
            List<Record> recordList = GetPartialRecordsFromString(input);
            // Sort list into datetime order
            recordList = recordList.OrderBy(p => p.date).ToList();
            recordList = GetFullRecords(recordList);
            List<Guard> guards = GetGuards(recordList);

            Guard highestGuard = guards.First();
        }

        public static int GetTotalSleepMinutes(Dictionary<int,int> sleepFrequency)
        {
            return sleepFrequency.Sum(p => p.Value);
        }

        /// <summary>
        /// Get most frequent sleep minute
        /// </summary>
        /// <param name="sleepFrequency"></param>
        public static int GetMostFrequentMinute(Dictionary<int, int> sleepFrequency)
        {
            return sleepFrequency.FirstOrDefault(p => p.Value == sleepFrequency.Values.Max()).Value;
        }

        /// <summary>
        /// Get the list of guards and their sleep frequencies
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        public static List<Guard> GetGuards(List<Record> recordList)
        {
            List<Guard> guards = new List<Guard>();
            Dictionary<int, int> sleepFrequency = new Dictionary<int,int>();

            foreach (Record record in recordList)
            {
                int sleepMinuteStart = 0;
                int sleepMinuteEnd = 0;
                if (record.isAsleep == false)
                {
                    if (record.date.Hour > 0)
                    {
                        sleepMinuteStart = 0;
                    }
                    else
                    {
                        sleepMinuteStart = record.date.Minute;
                    }
                }
                else
                {
                    if (record.date.Hour > 0)
                    {
                        sleepMinuteEnd = 59;
                    }
                    else
                    {
                        sleepMinuteEnd = record.date.Minute;
                    }
                }
                sleepFrequency = GetSleepFrequency(sleepMinuteStart, sleepMinuteEnd, sleepFrequency);

                Guard guard;
                if (!guards.Select(p => p.GuardId).Contains(record.guardId))
                {
                    guard = new Guard();
                    guard.GuardId = record.guardId;
                    guard.sleepFrequency = sleepFrequency;
                    guards.Add(guard);
                }
                else
                {
                    guards.First(p => p.GuardId == record.guardId).sleepFrequency = sleepFrequency;
                }
            }

            foreach(Guard guard in guards)
            {
                guard.totalSleep = GetTotalSleepMinutes(guard.sleepFrequency);
                guard.maxSleepMinute = GetMostFrequentMinute(guard.sleepFrequency);
            }

            return guards.OrderBy(p => p.totalSleep).ToList();

        }

        /// <summary>
        /// Return the frequency table for the sleep duration
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, int> GetSleepFrequency(int sleepMinuteStart, int sleepMinuteEnd, Dictionary<int,int> sleepFrequency = null)
        {
            if(sleepFrequency == null)
            {
                sleepFrequency = new Dictionary<int, int>();
            }
            for (int i = 0; i <= 59; i++)
            {
                if (i >= sleepMinuteStart && i < sleepMinuteEnd)
                {
                    if (!sleepFrequency.ContainsKey(i))
                    {
                        sleepFrequency.Add(i, 1);
                    }
                    else
                    {
                        sleepFrequency[i]++;
                    }
                }
            }
            return sleepFrequency;
        }

        public static List<Record> GetFullRecords(List<Record> input)
        {
            int currentGuardId = 0;

            foreach (Record entry in input)
            {
                // Parse text portion
                int guardId = 0;
                Regex guardRegex = new Regex("Guard #([0-9]+) begins shift");
                Match m = guardRegex.Match(entry.text);
                if (m.Success)
                {
                    guardId = Convert.ToInt32(m.Groups[1].ToString());
                    currentGuardId = guardId;
                }

                bool isAsleepRecord = false;
                if (String.Equals(entry.text, "falls asleep", StringComparison.OrdinalIgnoreCase))
                {
                    isAsleepRecord = true;
                }
                if (String.Equals(entry.text, "wakes up", StringComparison.OrdinalIgnoreCase))
                {
                    isAsleepRecord = false;
                }

                entry.guardId = currentGuardId;
                entry.isAsleep = isAsleepRecord;
            }

            return input;
        }

        /// <summary>
        /// For each string, return the records text plus parsed date
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Record> GetPartialRecordsFromString(List<string> input)
        {
            List<Record> recordList = new List<Record>();
            foreach (string inputRow in input)
            {
                // Parse date
                string datepart = inputRow.Substring(0, 18).Replace("[", "").Replace("]", "");
                DateTime dateRecord;
                bool dateConvert = DateTime.TryParseExact(datepart, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRecord);
                if (!dateConvert)
                {
                    Console.WriteLine($"Date failed {datepart}");
                }
                string textpart = inputRow.Substring(19);
                Record record = new Record()
                {
                    date = dateRecord,
                    text = textpart
                };

                recordList.Add(record);
            }
            return recordList;
        }

        public static bool Validate()
        {
            List<string> input = HelperFunctions.ReadFile("Data/D4P1Test.txt");
            ParseStringsIntoSortedRecords(input);
            return false;
        }

    }

    class Guard
    {
        public int GuardId;
        public Dictionary<int, int> sleepFrequency;
        public int totalSleep;
        public int maxSleepMinute;
    }

    class Record
    {
        public DateTime date;
        public int guardId;
        public bool isAsleep;
        public string text;
    }
}

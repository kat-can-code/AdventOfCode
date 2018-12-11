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
                int result = 0;
                if (isP2)
                {
                    result = GetStrategyTwo(input);
                    Console.WriteLine($"Solution is {result}");
                }
                else
                {
                    result = GetStrategyOne(input);
                    Console.WriteLine($"Solution is {result}");
                }
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 2.");
            }
        }

        /// <summary>
        /// Solution for part 2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetStrategyTwo(List<string> input)
        {

            List<Record> recordList = GetPartialRecordsFromString(input);
            // Sort list into datetime order
            recordList = recordList.OrderBy(p => p.date).ToList();
            recordList = GetFullRecords(recordList);
            List<Guard> guards = GetGuards(recordList);
            int highestValue = 0;
            int currentValue = 0;
            Guard maxGuard = new Guard();
            foreach (Guard guard in guards)
            {
                if (maxGuard.GuardId == 0)
                {
                    maxGuard = guard;
                }

                highestValue = maxGuard.sleepFrequency[maxGuard.maxSleepMinute];
                if (guard.sleepFrequency.Count == 0)
                {
                    continue;
                }
                currentValue = guard.sleepFrequency[guard.maxSleepMinute];
                if (currentValue > highestValue)
                {
                    maxGuard = guard;
                }
            }

            int checksum = maxGuard.GuardId * maxGuard.maxSleepMinute;

            return checksum;
        }

        /// <summary>
        /// Solution for part 1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetStrategyOne(List<string> input)
        {
            List<Record> recordList = GetPartialRecordsFromString(input);
            // Sort list into datetime order
            recordList = recordList.OrderBy(p => p.date).ToList();
            recordList = GetFullRecords(recordList);
            List<Guard> guards = GetGuards(recordList);

            Guard highestGuard = guards.OrderByDescending(p => p.totalSleep).ToList().First();

            int checksum = highestGuard.GuardId * highestGuard.maxSleepMinute;

            return checksum;
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
              return sleepFrequency.FirstOrDefault(p => p.Value == sleepFrequency.Values.Max()).Key;
        }

        /// <summary>
        /// Get the list of guards and their sleep frequencies
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        public static List<Guard> GetGuards(List<Record> recordList)
        {
            Dictionary<int,Guard> guards = new Dictionary<int, Guard>();

            int startTime = 59;
            int endTime = 0;
            int currentGuardId = 0;

            foreach (Record record in recordList)
            {
                if (!guards.ContainsKey(record.guardId))
                {
                    Guard guard = new Guard();
                    guard.sleepFrequency = new Dictionary<int, int>();
                    guard.GuardId = record.guardId;
                    guards.Add(guard.GuardId, guard);
                }
                
                if (currentGuardId == 0)
                {
                    currentGuardId = record.guardId;
                }

                // New guard started his shift
                if (currentGuardId != record.guardId)
                {
                    // if guard falls asleep til end of the shift
                    if(endTime < startTime)
                    {
                        endTime = 59;
                        guards[currentGuardId].sleepFrequency = GetSleepFrequency(startTime, endTime, guards[currentGuardId].sleepFrequency);
                    }

                    currentGuardId = record.guardId;
                }


                if (record.date.Hour == 0 && record.isAsleep == false)
                {
                    endTime = record.date.Minute;
                }
                if (record.date.Hour == 0 && record.isAsleep == true)
                {
                    startTime = record.date.Minute;
                }
                
                if(startTime <= endTime)
                {
                    guards[currentGuardId].sleepFrequency = GetSleepFrequency(startTime, endTime, guards[currentGuardId].sleepFrequency);
                    startTime = 59;
                    endTime = 0;
                }

            }
                
            foreach(int guardId in guards.Keys)
            {
                guards[guardId].totalSleep = GetTotalSleepMinutes(guards[guardId].sleepFrequency);
                guards[guardId].maxSleepMinute = GetMostFrequentMinute(guards[guardId].sleepFrequency);
            }

            return guards.Values.ToList();

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

        /// <summary>
        /// Phase two parsing of records
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
            bool result = false;
            List<string> input = HelperFunctions.ReadFile("Data/D4P1Test.txt");
            int testResult = GetStrategyOne(input);
            if(testResult == 240)
            {
                result = true;
            }

            testResult = GetStrategyTwo(input);
            if(testResult == 4455)
            {
                result = true;
            }

            return result;
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

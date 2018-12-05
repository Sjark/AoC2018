using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input\\input.txt");
            var timestamps = new List<TimeStamp>();

            foreach (var line in input)
            {
                var inputSplitted = line.Split(']');
                var date = DateTime.Parse(inputSplitted[0].Replace("[", ""));
                var text = inputSplitted[1].Trim();

                timestamps.Add(new TimeStamp
                {
                    Text = text,
                    Time = date
                });
            }

            var minutesAsleep = new Dictionary<int, GuardAsleep>();
            var currentGuardOnDuty = -1;
            var startedSleeping = DateTime.MinValue;

            foreach (var timestamp in timestamps.OrderBy(a => a.Time))
            {
                if (timestamp.Text.StartsWith("Guard"))
                {
                    currentGuardOnDuty = int.Parse(timestamp.Text.Replace("Guard #", "").Replace(" begins shift", ""));
                }

                if (timestamp.Text == "falls asleep")
                {
                    startedSleeping = timestamp.Time;
                }

                if (timestamp.Text == "wakes up")
                {
                    var sleptFor = (int)(timestamp.Time - startedSleeping).TotalMinutes;

                    if (minutesAsleep.ContainsKey(currentGuardOnDuty))
                    {
                        var guardAsleep = minutesAsleep[currentGuardOnDuty];
                        guardAsleep.MinutesAsleep += sleptFor;

                        for (int i = startedSleeping.Minute; i < timestamp.Time.Minute; i++)
                        {
                            if (guardAsleep.SleepSchedule.ContainsKey(i))
                            {
                                guardAsleep.SleepSchedule[i] += 1;
                            }
                            else
                            {
                                guardAsleep.SleepSchedule.Add(i, 1);
                            }
                        }

                        minutesAsleep[currentGuardOnDuty] = guardAsleep;
                    }
                    else
                    {
                        var guardAsleep = new GuardAsleep();
                        guardAsleep.MinutesAsleep = sleptFor;
                        guardAsleep.SleepSchedule = new Dictionary<int, int>();
                        for (int i = startedSleeping.Minute; i < timestamp.Time.Minute; i++)
                        {
                            if (guardAsleep.SleepSchedule.ContainsKey(i))
                            {
                                guardAsleep.SleepSchedule[i] += 1;
                            }
                            else
                            {
                                guardAsleep.SleepSchedule.Add(i, 1);
                            }
                        }
                        minutesAsleep.Add(currentGuardOnDuty, guardAsleep);
                    }
                }
            }

            var guardThatSleptTheMost = minutesAsleep.OrderByDescending(a => a.Value.MinutesAsleep).First();
            var minuteHeSleptMostOften = guardThatSleptTheMost.Value.SleepSchedule.OrderByDescending(a => a.Value).First().Key;

            Console.WriteLine($"Day4a: {guardThatSleptTheMost.Key * minuteHeSleptMostOften}");

            Console.Read();
        }
    }

    public class TimeStamp
    {
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }

    public class GuardAsleep
    {
        public int MinutesAsleep { get; set; }
        public Dictionary<int, int> SleepSchedule { get; set; }
    }
}

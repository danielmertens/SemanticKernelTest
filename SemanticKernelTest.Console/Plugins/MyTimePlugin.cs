using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelTest.Console.Plugins
{
    public class MyTimePlugin
    {
        [KernelFunction, Description("Get the current time")]
        public static DateTimeOffset Time() {
            System.Console.WriteLine("PLUGIN: Called function Time");
            return DateTimeOffset.Now;
        }

        [KernelFunction, Description("Get the date of tomorrow")]
        public static DateTimeOffset Tomorrow()
        {
            return DateTimeOffset.Now.AddDays(1);
        }

        [KernelFunction, Description("Get the date of yesterday")]
        public static DateTimeOffset Yesterday()
        {
            return DateTimeOffset.Now.AddDays(-1);
        }

        [KernelFunction, Description("Gets the date of the next given day of the week")]
        public static DateTimeOffset Next(
            [Description("The day of the week as a three letter represenation in english. (mon, tue, wed, thu, fri, sat, sun)")]string dayOfWeek)
        {
            DayOfWeek dow = dayOfWeek switch
            {
                "mon" => DayOfWeek.Monday,
                "tue" => DayOfWeek.Tuesday,
                "wed" => DayOfWeek.Wednesday,
                "thu" => DayOfWeek.Thursday,
                "fri" => DayOfWeek.Friday,
                "sat" => DayOfWeek.Saturday,
                "sun" => DayOfWeek.Sunday,
                _ => throw new ArgumentException($"Argument value {dayOfWeek} is invalid.", nameof(dayOfWeek)),
            };

            var date = DateTimeOffset.Now.AddDays(1);
            while(date.DayOfWeek != dow)
                date = date.AddDays(1);

            return date;
        }
    }
}

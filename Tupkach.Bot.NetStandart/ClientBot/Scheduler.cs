using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;
using Timer = System.Threading.Timer;

namespace Tupkach.Bot.NetStandart.ClientBot
{
    public class Scheduler : IScheduler
    {
        private readonly ConcurrentBag<TimerInfo> _timers;

        public Scheduler()
        {
            _timers = new ConcurrentBag<TimerInfo>();
        }

        public void Schedule(DateTime dateTime, string name, TimerCallback action)
        {
            dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, dateTime.Hour, dateTime.Minute, 0);
            var span = dateTime - DateTime.Now;

            var timer = new Timer(action, null, span, TimeSpan.FromDays(1));

            _timers.Add(new TimerInfo(timer, dateTime));
        }


        public string PrintTimers()
        {
            var builder = new StringBuilder();

            var i = 0;
            foreach (var timer in _timers.OrderBy(x => x.Time))
            {
                builder.Append(++i);
                builder.Append(": ");
                builder.Append($"{timer.Time.Hour}:{timer.Time.Minute}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }

    public class TimerInfo
    {
        public TimerInfo(Timer timer, DateTime time)
        {
            Timer = timer;
            Time = time;
        }

        public Timer Timer { get; }
        public DateTime Time { get; }
    }
}
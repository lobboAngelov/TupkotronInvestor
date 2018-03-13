using System;
using System.Threading;

namespace Tupkach.Bot.NetStandart.ClientBot.Interfaces
{
    public interface IScheduler
    {
        void Schedule(DateTime dateTime, string name, TimerCallback action);

        string PrintTimers();
    }
}
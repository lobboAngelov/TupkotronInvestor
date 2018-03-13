using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tupkach.Bot.NetStandart.ClientBot.Interfaces
{
    public interface IClientBot : ILogger
    {
        Task StartAsync();
    }
}

using System.Threading.Tasks;

namespace Tupkach.Bot.NetStandart.ClientBot.Interfaces
{
    public interface IClientBot : ILogger
    {
        Task StartAsync();
    }
}

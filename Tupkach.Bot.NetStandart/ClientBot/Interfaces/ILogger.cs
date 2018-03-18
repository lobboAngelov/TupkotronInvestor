using System.Threading.Tasks;

namespace Tupkach.Bot.NetStandart.ClientBot.Interfaces
{
    public interface ILogger
    {
        Task LogAsync(string message);
    }
}

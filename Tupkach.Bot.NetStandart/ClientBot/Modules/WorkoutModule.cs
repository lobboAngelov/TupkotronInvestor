using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Tupkach.Bot.Infrastructure;
using Tupkach.Bot.NetStandart.ClientBot.Context;
using Ninject;
using Tupkach.Bot.Infrastructure.Entities.Workouts;
using Tupkach.Bot.Infrastructure.Entities;
using System.Text;

namespace Tupkach.Bot.NetStandart.ClientBot.Modules
{
    public class WorkoutModule : ModuleBase<TupkachCommandContext>
    {
        [Command("trenirai")]
        public async Task WorkoutCommand(string workoutName, int amount)
        {
            var repository = Context.LifetimeScope.Get<IRepository>();
            var workoutType = repository.GetAll<WorkoutType>().FirstOrDefault(x => x.WorkoutName.ToLower() == workoutName.ToLower());

            if (workoutType == null)
            {
                await Context.Channel.SendMessageAsync("nema takova");
                await Context.Channel.SendMessageAsync("pishi kur trenirovki");
            }
            else
            {
                var tupkach = repository.GetAll<Tupkachov>().FirstOrDefault(x => x.DiscordName == Context.User.Username);

                if (tupkach == null)
                {
                    await Context.Channel.SendMessageAsync("Neregistriran tupkach");
                }
                else
                {
                    var workout = new Workout
                    {
                        WorkoutName = workoutType.WorkoutName,
                        Tupkachov = tupkach,
                        Amount = amount,
                    };

                    repository.Insert(workout);
                    repository.Save();
                }
            }
        }

        [Command("trenirovki")]
        public async Task WorkoutTypes()
        {
            var repository = Context.LifetimeScope.Get<IRepository>();
            var workoutTypes = repository.GetAll<WorkoutType>();

            var builder = new StringBuilder();
            builder.AppendLine("```");
            foreach (var workoutType in workoutTypes)
            {
                builder.AppendLine(workoutType.WorkoutName);
            }
            builder.AppendLine("```");
            await Context.Channel.SendMessageAsync(builder.ToString());
        }

        [Command("register")]
        public async Task Register()
        {
            var repository = Context.LifetimeScope.Get<IRepository>();

            if (repository.GetAll<Tupkachov>().Any(x => x.DiscordName == Context.User.Username))
            {
                await Context.Channel.SendMessageAsync($"Tupkach {Context.User.Username} already registered");
            }
            else
            {
                var tupkach = new Tupkachov
                {
                    DiscordName = Context.User.Username,
                };
                repository.Insert(tupkach);
                repository.Save();

                await Context.Channel.SendMessageAsync($"Tupkach {tupkach.DiscordName} registered with id {tupkach.TupkachovId}");
            }
        }

        [Command("karma")]
        public async Task Karma()
        {
            var repository = Context.LifetimeScope.Get<IRepository>();

            var tupkach = repository.GetAll<Tupkachov>().FirstOrDefault(x => x.DiscordName == Context.User.Username);

            if (tupkach == null)
            {
                await Context.Channel.SendMessageAsync("Unregistered tupkach");
            }
            else
            {
                var workoutTypes = repository.GetAll<WorkoutType>();
                var karma = 0.0f;
                foreach (var workout in tupkach.Wokouts)
                {
                    var workoutType = workoutTypes.First(x => x.WorkoutName == workout.WorkoutName);
                    karma += workoutType.KarmaValue * workout.Amount;
                }

                await Context.Channel.SendMessageAsync($"{Context.User} has {karma} karma");
            }
        }
    }
}

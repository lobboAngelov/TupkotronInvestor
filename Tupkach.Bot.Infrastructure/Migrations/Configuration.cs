namespace Tupkach.Bot.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tupkach.Bot.Infrastructure.Entities.Workouts;

    internal sealed class Configuration : DbMigrationsConfiguration<Tupkach.Bot.Infrastructure.TupkachDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tupkach.Bot.Infrastructure.TupkachDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            AddWorkoutTypes(context);
        }

        private static void AddWorkoutTypes(TupkachDbContext context)
        {
            var workoutTypes = new WorkoutType[]
                {
                new WorkoutType{ KarmaValue = 1.0f, WorkoutName = "Nabiraniq"},
                new WorkoutType{ KarmaValue = 1.1f, WorkoutName = "Nabiraniq Shiroki"},
                new WorkoutType{ KarmaValue = .5f, WorkoutName = "Klekaniq"},
                new WorkoutType{ KarmaValue = 0.8f, WorkoutName = "Licevi"},
                new WorkoutType{ KarmaValue = .5f, WorkoutName = "Koremni lesni"},
                new WorkoutType{ KarmaValue = .6f, WorkoutName = "Koremni trudni"},
                new WorkoutType{ KarmaValue = 1.0f, WorkoutName = "Tichane"},
                };

            foreach (var workoutType in workoutTypes)
            {
                if (!context.WorkoutTypes.Any(x => x.WorkoutName == workoutType.WorkoutName))
                {
                    context.WorkoutTypes.Add(workoutType);
                }
            }
            
            context.SaveChanges();
        }
    }
}

using System.Data.Entity;
using Tupkach.Bot.Infrastructure.Entities;
using Tupkach.Bot.Infrastructure.Entities.Workouts;

namespace Tupkach.Bot.Infrastructure
{
    public class TupkachDbContext : DbContext
    {
        public TupkachDbContext() : base("TupkachiDb")
        {}

        public DbSet<Tupkachov> Tupkachovs { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }
    }
}

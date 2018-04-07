using System.ComponentModel.DataAnnotations;

namespace Tupkach.Bot.Infrastructure.Entities.Workouts
{
    public class WorkoutType
    {
        [Key]
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public float KarmaValue { get; set; }
    }
}

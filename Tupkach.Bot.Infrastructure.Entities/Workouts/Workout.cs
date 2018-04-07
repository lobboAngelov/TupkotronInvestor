using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tupkach.Bot.Infrastructure.Entities.Interfaces;

namespace Tupkach.Bot.Infrastructure.Entities.Workouts
{
    public class Workout : ITrackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkoutId { get; set; }
        public int TupkachovId { get; set; }
        public int Amount { get; set; }
        public virtual Tupkachov Tupkachov { get; set; }
        public DateTime Time { get; set; }
        public string WorkoutName { get; set; }
    }
}

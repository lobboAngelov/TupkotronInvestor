using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tupkach.Bot.Infrastructure.Entities.Workouts;

namespace Tupkach.Bot.Infrastructure.Entities
{
    public class Tupkachov
    {
        private ICollection<Workout> workouts;

        public Tupkachov()
        {
            workouts = new HashSet<Workout>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TupkachovId { get; set; }
        public string DiscordName { get; set; }

        public virtual ICollection<Workout> Wokouts { get => workouts; set => workouts = value; }

    }
}

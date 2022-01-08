using System.ComponentModel.DataAnnotations.Schema;

namespace GameTracker.Shared.Models
{
    [Table("games")]
    public class Game
    {
        [Column("id")]
        public long? Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("platform")]
        public string? Platform { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("hours_to_complete")]
        public double? HoursToComplete { get; set; }

        [Column("rating")]
        public double? Rating { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace GameTracker.Shared.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public long? Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}

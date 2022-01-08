using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker.Shared.Models
{
    public class GameViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Platform { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        public string? Status { get; set; }
        public string? HoursToComplete { get; set; }
        public string? Rating { get; set; }
        public string? Price { get; set; }
        public string? UserId { get; set; }

        public GameViewModel() { }
    }
}

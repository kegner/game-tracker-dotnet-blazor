using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker.Shared.Models
{
    public record GameDto
    {
        public long? Id { get; init; }
        public string? Title { get; init; }
        public string? Platform { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Status { get; init; }
        public double? HoursToComplete { get; init; }
        public double? Rating { get; init; }
        public double? Price { get; init; }
        public long? UserId { get; init; }
    }
}

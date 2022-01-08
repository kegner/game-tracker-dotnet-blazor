using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GameTracker.Shared.Models
{
    public record GamesDto
    {
        [JsonPropertyName("games")]
        public List<GameDto>? GamesList { get; init; }
        public int Count { get; init; } = 0;

        public GamesDto() { }

        public GamesDto(List<GameDto> games)
        {
            GamesList = games;
            Count = games.Count();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GameTracker.Shared.Models
{
    public class Games
    {
        [JsonPropertyName("games")]
        public List<Game>? games { get; init; }
        public int Count { get; init; } = 0;

        public Games() { }

        public Games(List<Game> games)
        {
            this.games = games;
            Count = games.Count();
        }
    }
}

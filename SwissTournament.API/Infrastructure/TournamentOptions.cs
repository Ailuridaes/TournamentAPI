using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Infrastructure
{
    public class TournamentOptions
    {
        [Required]
        public IEnumerable<string> PlayerNames { get; set; }
        public int Rounds { get; set; }
    }
}
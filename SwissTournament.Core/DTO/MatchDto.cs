﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.DTO
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public Boolean IsCompleted { get; set; }
        public IEnumerable<MatchupDto> Matchups { get; set; }
    }
}
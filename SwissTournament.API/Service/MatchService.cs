﻿using AutoMapper;
using SwissTournament.API.Domain;
using SwissTournament.API.DTO;
using SwissTournament.API.Exceptions;
using SwissTournament.API.Infrastructure;
using SwissTournament.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Service
{
    public class MatchService
    {
        private readonly TournamentRepository _tournamentRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly MatchRepository _matchRepository;
        private readonly MatchupRepository _matchupRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MatchService(TournamentRepository tr, MatchRepository mr, MatchupRepository nr, UnitOfWork unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tr;
            _matchRepository = mr;
            _matchupRepository = nr;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MatchDto> GetMatches(int tournamentId)
        {
            Tournament tournament = GetTournament(tournamentId);
            IEnumerable<Match> matches = tournament.Matches;
            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }

        public IEnumerable<MatchDto> GetCurrentMatches(int tournamentId)
        {
            Tournament tournament = GetTournament(tournamentId);
            IEnumerable<Match> matches = tournament.Matches.Where(m => m.Round == tournament.Round);
            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }

        public void Update(MatchDto match)
        {
            var matchups = match.Matchups.OrderByDescending(n => n.Wins).ToList();
            var dbMatch = GetMatch(match.Id);

            // Sync win/loss records
            matchups[0].Losses = matchups[1].Wins;
            matchups[1].Losses = matchups[0].Wins;
            matchups[1].Ties = matchups[0].Ties;

            if (matchups[0].Wins > matchups[1].Wins)
            {
                matchups[0].DidWin = true;
            }
            else
            {
                matchups[0].DidTie = true;
                matchups[1].DidTie = true;
            }

            foreach(MatchupDto matchup in matchups)
            {
                var dbMatchup = GetMatchup(matchup.Id);

                // Verify Matchup object matches child of Match objec
                if (!dbMatch.Matchups.Any(n => n.Id == dbMatchup.Id) || dbMatchup.MatchId != matchup.MatchId || dbMatchup.PlayerId != matchup.PlayerId)
                {
                    throw new EntityHierarchyException("Matchup", "Match");
                }

                dbMatchup.SetResults(matchup);
                _matchupRepository.Update(dbMatchup);
            }
            
            dbMatch.IsCompleted = true;

            _matchRepository.Update(dbMatch);

            _unitOfWork.Commit();
        }

        // Helper classes

        private Tournament GetTournament(int tournamentId)
        {
            Tournament tournament = _tournamentRepository.GetById(tournamentId);

            if (tournament == null) throw new ReadEntityException("Tournament");

            return tournament;
        }

        private Match GetMatch(int matchId)
        {
            Match match = _matchRepository.GetById(matchId);

            if (match == null) throw new ReadEntityException("Match");

            return match;
        }

        private Matchup GetMatchup(int matchupId)
        {
            Matchup matchup = _matchupRepository.GetById(matchupId);

            if (matchup == null) throw new ReadEntityException("Matchup");

            return matchup;
        }
    }
}
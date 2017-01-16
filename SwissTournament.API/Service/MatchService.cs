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

        public MatchService(TournamentRepository tr, PlayerRepository pr, MatchRepository mr, MatchupRepository nr, UnitOfWork unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tr;
            _playerRepository = pr;
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

        public Tournament GetTournament(int tournamentId)
        {
            Tournament tournament = _tournamentRepository.GetById(tournamentId);

            if (tournament == null) throw new ReadEntityException("Tournament");

            return tournament;
        }
    }
}
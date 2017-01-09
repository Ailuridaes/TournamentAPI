﻿using SwissTournament.API.Infrastructure;
using SwissTournament.API.Domain;
using SwissTournament.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwissTournament.API.Exceptions;

namespace SwissTournament.API.Service
{
    public class TournamentService
    {
        private readonly TournamentRepository _tournamentRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly MatchRepository _matchRepository;
        private readonly MatchupRepository _matchupRepository;
        private readonly UnitOfWork _unitOfWork;

        public TournamentService()
        {
            // TODO: Use dependency injection
            _tournamentRepository = new TournamentRepository();
            _playerRepository = new PlayerRepository();
            _matchRepository = new MatchRepository();
            _matchupRepository = new MatchupRepository();
            _unitOfWork = new UnitOfWork();
        }
        public Tournament CreateTournament(IEnumerable<string> playerNames)
        {
            Tournament tournament = _tournamentRepository.Add(new Tournament());

            // TODO: Check # of players?
            foreach (string name in playerNames)
            {
                _playerRepository.Add(new Player(name, tournament.TournamentId));
            }

            if (playerNames.Count() % 2 != 0)
            {
                _playerRepository.Add(new Player("BYE", tournament.TournamentId));
            }

            _unitOfWork.Commit();

            StartTournament(tournament);

            return tournament;
        }

        public void StartTournament(Tournament tournament)
        {
            var rnd = new Random();
            List<int> playerIds = tournament.Players.OrderBy(p => rnd.Next()).Select(p => p.PlayerId).ToList();

            for(int i = 0; i < playerIds.Count()/2; i++) {
                Match match = _matchRepository.Add(new Match(tournament.TournamentId, 1));
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[i] });
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[playerIds.Count() / 2 + i] });
            }

            _unitOfWork.Commit();
        }

        public Tournament GetTournament(int tournamentId)
        {
            Tournament tournament = _tournamentRepository.GetById(tournamentId);

            if (tournament == null) throw new ReadEntityException("Tournament");

            return tournament;
        }
    }
}
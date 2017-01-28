using SwissTournament.API.Infrastructure;
using SwissTournament.API.Domain;
using SwissTournament.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwissTournament.API.Exceptions;
using AutoMapper;
using SwissTournament.API.DTO;

namespace SwissTournament.API.Service
{
    public class TournamentService
    {
        private readonly TournamentRepository _tournamentRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly MatchRepository _matchRepository;
        private readonly MatchupRepository _matchupRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentService(TournamentRepository tr, PlayerRepository pr, MatchRepository mr, MatchupRepository nr, UnitOfWork unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tr;
            _playerRepository = pr;
            _matchRepository = mr;
            _matchupRepository = nr;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public TournamentDto CreateTournament(IEnumerable<string> playerNames)
        {
            Tournament tournament = _tournamentRepository.Add(new Tournament());

            // TODO: Check # of players?
            foreach (string name in playerNames)
            {
                _playerRepository.Add(new Player(name, tournament.Id));
            }

            if (playerNames.Count() % 2 != 0)
            {
                _playerRepository.Add(new Player("BYE", tournament.Id));
            }

            _unitOfWork.Commit();

            StartTournament(tournament);

            return _mapper.Map<TournamentDto>(tournament);  
        }

        private void StartTournament(Tournament tournament)
        {
            var rnd = new Random();
            List<int> playerIds = tournament.Players.OrderBy(p => rnd.Next()).Select(p => p.Id).ToList();

            for(int i = 0; i < playerIds.Count()/2; i++) {
                Match match = _matchRepository.Add(new Match(tournament.Id, 1));
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[i] });
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[playerIds.Count() / 2 + i] });
            }

            _unitOfWork.Commit();
        }

        // Helper classes

        private Tournament GetTournament(int tournamentId)
        {
            Tournament tournament = _tournamentRepository.GetById(tournamentId);

            if (tournament == null) throw new ReadEntityException("Tournament");

            return tournament;
        }
    }
}
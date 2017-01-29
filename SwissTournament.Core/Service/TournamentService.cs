using SwissTournament.Core.Infrastructure;
using SwissTournament.Core.Domain;
using SwissTournament.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwissTournament.Core.Exceptions;
using AutoMapper;
using SwissTournament.Core.DTO;

namespace SwissTournament.Core.Service
{
    public interface ITournamentService
    {
        TournamentDto CreateTournament(IEnumerable<string> playerNames);
        void SubmitRound(int tournamentId);
    }

    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchupRepository _matchupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentService(ITournamentRepository tr, IPlayerRepository pr, IMatchRepository mr, IMatchupRepository nr, IUnitOfWork unitOfWork, IMapper mapper)
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

        public void SubmitRound(int tournamentId)
        {
            // TODO: Implement pairing for next round
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
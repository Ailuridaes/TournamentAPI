using SwissTournament.API.Infrastructure;
using SwissTournament.API.Domain;
using SwissTournament.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Service
{
    public class TournamentService
    {
        private readonly TournamentRepository _tournamentRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly UnitOfWork _unitOfWork;

        public TournamentService()
        {
            // TODO: Use dependency injection
            _tournamentRepository = new TournamentRepository();
            _playerRepository = new PlayerRepository();
            _unitOfWork = new UnitOfWork();
        }
        public Tournament createTournament(IEnumerable<string> playerNames)
        {
            Tournament tournament = _tournamentRepository.Add(new Tournament());

            // TODO: Check # of players?
            foreach (string name in playerNames)
            {
                _playerRepository.Add(new Player(name, tournament.TournamentId));
            }

            _unitOfWork.Commit();

            return tournament;
        }
    }
}
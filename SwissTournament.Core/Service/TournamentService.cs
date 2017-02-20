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
        private readonly Random _rnd;

        public TournamentService(ITournamentRepository tr, IPlayerRepository pr, IMatchRepository mr, IMatchupRepository nr, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tr;
            _playerRepository = pr;
            _matchRepository = mr;
            _matchupRepository = nr;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rnd = new Random();
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
            List<int> playerIds = tournament.Players.OrderBy(p => _rnd.Next()).Select(p => p.Id).ToList();

            for(int i = 0; i < playerIds.Count()/2; i++) {
                Match match = _matchRepository.Add(new Match(tournament.Id, 1));
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[i] });
                _matchupRepository.Add(new Matchup { Match = match, PlayerId = playerIds[playerIds.Count() / 2 + i] });
            }

            _unitOfWork.Commit();
        }

        public void SubmitRound(int tournamentId)
        {
            Tournament tournament = getTournament(tournamentId);

            if(tournament.Matches.Any(m => m.IsCompleted == false))
            {
                throw new Exception("Results have not been submitted for all matches");
            }

            tournament.Round++;

            updateRankings(tournament);

            // TODO: Implement pairing for next round

            _unitOfWork.Commit();
        }

        // Helper methods

        private Tournament getTournament(int tournamentId)
        {
            Tournament tournament = _tournamentRepository.GetById(tournamentId);

            if (tournament == null) throw new ReadEntityException("Tournament");

            return tournament;
        }

        private void updateRankings(Tournament tournament)
        {
            IEnumerable<IGrouping<double, Player>> rankedPlayers;

            // GroupBy match points
            rankedPlayers = tournament.Players.GroupBy(p => (double)((p.GetMatchWins() * 3) + p.GetMatchTies())).OrderByDescending(g => g.Key);

            // GroupBy opponents' match-win percentage
            rankedPlayers = rankedPlayers.SelectMany(g => g.GroupBy(p => p.Matchups.Average(n => n.Match.Matchups
                .Where(oN => oN.PlayerId != p.Id).Single().Player.GetMatchWinPercentage(false)))
                .OrderByDescending(h => h.Key));

            if (rankedPlayers.Any(g => g.Count() > 1))
            {
                // GroupBy game-win percentage
                rankedPlayers = rankedPlayers.SelectMany(g => g.GroupBy(p => p.GetGameWinPercentage())
                    .OrderByDescending(h => h.Key));
            }

            if (rankedPlayers.Any(g => g.Count() > 1))
            {
                // GroupBy opponents' game-win percentage
                rankedPlayers = rankedPlayers.SelectMany(g => g.GroupBy(p => p.Matchups.Average(n => n.Match.Matchups
                .Where(oN => oN.PlayerId != p.Id).Single().Player.GetGameWinPercentage(false)))
                .OrderByDescending(h => h.Key));
            }

            int rank = 1;

            foreach(IGrouping<double, Player> group in rankedPlayers)
            {
                foreach(Player player in group.ToList())
                {
                    player.Ranking = rank;
                    _playerRepository.Update(player);
                }
                rank += group.Count();
            }
        }

        private void createMatches(Tournament tournament)
        {
            IEnumerable<IGrouping<int, Player>> groups = tournament.Players.OrderBy(p => _rnd.Next()).GroupBy(p => p.GetMatchWins() * 3 + p.GetMatchTies()).OrderByDescending(g => g.Key);

            createRoundPairs(groups);
        }

        private ICollection<Tuple<Player, Player>> createRoundPairs(IEnumerable<IGrouping<int, Player>> groups)
        {
            throw new NotImplementedException();
        }

        private ICollection<Tuple<Player, Player>> createGroupPairs(List<Player> group)
        {
            // Verify group has even number of players
            if (group.Count() % 2 != 0)
            {
                throw new Exception("Group passed to createGroupPairs() with uneven number of players");
            }

            ICollection<Tuple<Player, Player>> pairs = new List<Tuple<Player, Player>>();
            int i = 1;

            while (i < group.Count())
            {
                if (!canPair(group[0], group[i]))
                {
                    i++;
                }
                else
                {
                    pairs.Add(new Tuple<Player, Player>(group[0], group[i]));

                    if (group.Count() > 2)
                    {
                        List<Player> tempList = new List<Player>(group);
                        tempList.Remove(group[0]);
                        tempList.Remove(group[i]);
                        try
                        {
                            var newPairs = createGroupPairs(tempList);
                            pairs.Concat(newPairs);
                        }
                        catch (CannotPairException ex)
                        {
                            i++;
                        }
                    }

                    return pairs;
                }

            }

            // iterated through entire group without successfully pairing
            throw new CannotPairException();
        }

        private bool canPair(Player p1, Player p2)
        {
            return p1.Matchups.SelectMany(n => n.Match.Matchups).Any(n => n.PlayerId == p2.Id);
        }

        private class CannotPairException : Exception
        {
            public CannotPairException() : base("Could not pair players within group") { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Infrastructure
{
    public sealed class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly TournamentDataContext _dataContext;

        public DatabaseFactory()
        {
            _dataContext = new TournamentDataContext();
        }

        public TournamentDataContext GetDataContext()
        {
            return _dataContext ?? new TournamentDataContext();
        }

        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
    public interface IDatabaseFactory : IDisposable
    {
        TournamentDataContext GetDataContext();
    }
}
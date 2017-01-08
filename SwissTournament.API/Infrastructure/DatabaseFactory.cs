using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Infrastructure
{
    public sealed class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private static readonly DatabaseFactory _instance = new DatabaseFactory();
        private readonly TournamentDataContext _dataContext;

        // TODO: Use dependency injection instead of implementing DatabaseFactory as a singleton
        public static DatabaseFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        private DatabaseFactory()
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
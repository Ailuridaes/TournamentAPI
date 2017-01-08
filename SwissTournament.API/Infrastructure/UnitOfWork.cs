using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Infrastructure
{
    public class UnitOfWork
    {
        private readonly DatabaseFactory _databaseFactory;
        private TournamentDataContext _dataContext;

        protected TournamentDataContext DataContext => _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public UnitOfWork()
        {
            _databaseFactory = DatabaseFactory.Instance;
        }
    }
}
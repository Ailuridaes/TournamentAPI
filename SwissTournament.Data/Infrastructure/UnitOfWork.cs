using SwissTournament.Core.Infrastructure;

namespace SwissTournament.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private TournamentDataContext _dataContext;

        protected TournamentDataContext DataContext => _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
    }
}
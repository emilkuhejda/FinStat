using System.Threading.Tasks;
using FinStat.DataAccess.Providers;
using FinStat.Domain.Interfaces.Repositories;
using FinStat.Domain.Models;

namespace FinStat.DataAccess.Repositories
{
    public class RecentlyVisitedCompanyRepository : IRecentlyVisitedCompanyRepository
    {
        private readonly IAppDbContextProvider _contextProvider;

        public RecentlyVisitedCompanyRepository(IAppDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Task InsertOrUpdateAsync(RecentlyVisitedCompany recentlyVisitedCompany)
        {
            throw new System.NotImplementedException();
        }

        public Task<RecentlyVisitedCompany[]> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}

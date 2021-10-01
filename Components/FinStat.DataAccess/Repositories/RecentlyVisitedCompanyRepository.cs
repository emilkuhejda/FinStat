using System.Linq;
using System.Threading.Tasks;
using FinStat.DataAccess.DataAdapters;
using FinStat.DataAccess.Entities;
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
            return _contextProvider.Context.InsertOrReplaceAsync(recentlyVisitedCompany.ToEntity());
        }

        public async Task<RecentlyVisitedCompany[]> GetAllAsync()
        {
            var entities = await _contextProvider.Context.RecentlyVisitedCompanies.ToListAsync();
            return entities.Select(x => x.ToDomainObject()).ToArray();
        }

        public async Task<RecentlyVisitedCompany[]> GetLastRecordsAsync(int limit)
        {
            var entities = await _contextProvider.Context.RecentlyVisitedCompanies
                .OrderByDescending(x => x.LastVisited)
                .Take(limit)
                .ToArrayAsync();

            return entities.Select(x => x.ToDomainObject()).ToArray();
        }

        public Task DeleteAsync(string symbol)
        {
            return _contextProvider.Context.DeleteAsync<RecentlyVisitedCompanyEntity>(symbol);
        }
    }
}

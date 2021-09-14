using System.Threading.Tasks;
using FinStat.Domain.Models;

namespace FinStat.Domain.Interfaces.Repositories
{
    public interface IRecentlyVisitedCompanyRepository
    {
        Task InsertOrUpdateAsync(RecentlyVisitedCompany recentlyVisitedCompany);

        Task<RecentlyVisitedCompany[]> GetAllAsync();

        Task<RecentlyVisitedCompany[]> GetLastRecordsAsync(int limit);
    }
}

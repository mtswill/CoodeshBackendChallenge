using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Repositories
{
    public class UserHistoryRepository : BaseRepository, IUserHistoryRepository
    {
        private readonly BcContext _context;

        public UserHistoryRepository(BcContext context)
        {
            _context = context;
        }

        public async Task<bool> AddHistoryAsync(UserHistory userHistory)
        {
            await _context.UserHistories!.AddAsync(userHistory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaginationResult<UserHistory>?> GetUserHistoryAsync(Guid userId, PaginationInput paginationInput)
        {
            var query = _context.UserHistories!.AsQueryable();
            query = query.AsNoTracking().OrderByDescending(x => x.Added);

            var paginatedQuery = BuildPagination(query, paginationInput);
            var history = await paginatedQuery.ToListAsync();

            var result = BuildPaginationResult<UserHistory>(paginationInput, await GetTotalCountAndTotalPagesAsync(query, paginationInput));
            result.Results = history!;

            return result;
        }
    }
}

using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Repositories
{
    public class EntryRepository : BaseRepository, IEntryRepository
    {
        private readonly BcContext _context;

        public EntryRepository(BcContext context)
        {
            _context=context;
        }

        public async Task<PaginationResult<string>> SearchWordAsync(string word, PaginationInput paginationInput)
        {
            var query = _context.Words!.AsQueryable();
            query = query.AsNoTracking().OrderBy(x => x.Text);

            if (!string.IsNullOrEmpty(word))
                query = query.Where(x => x.Text!.StartsWith(word.ToLower()));

            var paginatedQuery = BuildPagination(query, paginationInput);
            var words = await paginatedQuery.Select(x => x.Text).ToListAsync();

            var result = BuildPaginationResult<string>(paginationInput, await GetTotalCountAndTotalPagesAsync(query, paginationInput));
            result.Results = words!;

            return result;
        }
    }
}

using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected virtual IQueryable<T> BuildPagination<T>(IQueryable<T> query, PaginationInput paginationInput)
        {
            if (paginationInput.Limit > 0)
            {
                if (paginationInput.Page > 0)
                {
                    var skip = (paginationInput.Page - 1) * paginationInput.Limit;
                    query = query.Skip(skip);
                }

                query = query.Take(paginationInput.Limit);
            }

            return query;
        }

        protected virtual async Task<(int TotalCount, int TotalPages)> GetTotalCountAndTotalPagesAsync<T>(IQueryable<T> query, PaginationInput paginationInput)
        {
            var totalCount = await query.CountAsync();

            if (paginationInput.Limit! <= 0)
                paginationInput.Limit = totalCount;

            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationInput.Limit!);
            return (totalCount, totalPages);
        }

        protected virtual PaginationResult<T> BuildPaginationResult<T>(PaginationInput paginationInput, (int TotalCount, int TotalPages) totals)
        {
            return new PaginationResult<T>
            {
                TotalDocs = totals.TotalCount,
                TotalPages = totals.TotalPages,
                HasNext = paginationInput.Page < totals.TotalPages,
                HasPrev = paginationInput.Page > 1,
                Page = paginationInput.Page
            };
        }
    }
}

using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly BcContext _context;

        public UserRepository(BcContext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _context.Users!.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users!.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<bool> SaveFavoriteWordAsync(FavoriteWord favoriteWord)
        {
            await _context.FavoriteWords!.AddAsync(favoriteWord);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<FavoriteWord?> GetFavoriteWordAsync(string word, Guid userId)
        {
            return await _context.FavoriteWords!.FirstOrDefaultAsync(x => x.UserId == userId &&
                                                                          x.Word == word);
        }

        public async Task<bool> UnfavoriteWordAsync(FavoriteWord favoriteWord)
        {
            _context.FavoriteWords!.Remove(favoriteWord);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaginationResult<FavoriteWord>?> GetFavoriteWordsAsync(Guid userId, PaginationInput paginationInput)
        {
            var query = _context.FavoriteWords!.AsQueryable();
            query = query.AsNoTracking().OrderBy(x => x.Word);

            var paginatedQuery = BuildPagination(query, paginationInput);
            var words = await paginatedQuery.ToListAsync();

            var result = BuildPaginationResult<FavoriteWord>(paginationInput, await GetTotalCountAndTotalPagesAsync(query, paginationInput));
            result.Results = words!;

            return result;
        }
    }
}

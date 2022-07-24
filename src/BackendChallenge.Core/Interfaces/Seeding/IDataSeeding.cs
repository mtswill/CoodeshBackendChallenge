using BackendChallenge.Core.Result;

namespace BackendChallenge.Core.Interfaces.Seeding
{
    public interface IDataSeeding
    {
        Task<Result<object>> SeedAsync();
    }
}

using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Interfaces.Seeding;
using BackendChallenge.Core.Result;
using BackendChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendChallenge.Infrastructure.Seedings
{
    public class DataSeeding : IDataSeeding
    {
        private readonly BcContext _context;
        private readonly IConfiguration _configuration;

        public DataSeeding(BcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Result<object>> SeedAsync()
        {
            if (await _context.Words!.AnyAsync())
                return new Result<object>().Error("Banco de dados já populado");

            using var client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["WordListURL"])
            };

            var result = await client.GetAsync("");

            if (!result.IsSuccessStatusCode)
                return new Result<object>().Error("Não foi possível buscar as palavras do dicionário");

            var content = await result.Content.ReadAsStringAsync();
            var wordList = content.Split("\n").AsEnumerable();

            var words = new List<Word>();

            foreach (var word in wordList)
            {
                words.Add(new Word
                {
                    Text = word
                });
            }

            await _context.Words!.AddRangeAsync(words);
            var resultSaveChanges = await _context.SaveChangesAsync() > 0;

            if (resultSaveChanges)
                return new Result<object>().Success();

            return new Result<object>().Error("Não foi possível popular o banco de dados");
        }
    }
}

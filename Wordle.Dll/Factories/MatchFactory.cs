using System.Text;
using Wordle.Data.Entities;
using Wordle.Dll.Models;
using Wordle.Dll.Services;

namespace Wordle.Dll.Factories
{
    public class MatchFactory
    {
        private readonly CryptographyService _cryptographyService;
        public MatchFactory(CryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        public Match CreateMatch(MatchModel matchModel)
        {
            var userId = _cryptographyService.DecryptToNumber(GetString(matchModel.HashedUserId));
            var wordId = _cryptographyService.DecryptToNumber(GetString(matchModel.HashedWordId));

            return new Match
            {
                Id = 0,
                UserId = userId,
                WordId = wordId,
                Attempt = matchModel.Attemt,
                Duration = matchModel.Duration,
                Created = DateTime.UtcNow,
                Won = matchModel.Won,
            };
        }

        private string GetString(string base64String)
        {
            var valueBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}

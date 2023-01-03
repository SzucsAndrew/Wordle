using Microsoft.AspNetCore.Mvc;
using System.Text;
using Wordle.Dll.Enums;
using Wordle.Dll.Factories;
using Wordle.Dll.Models;
using Wordle.Dll.Services;

namespace Wordle.Web.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class WordleController : Controller
    {
        private readonly WordleService _wordleService;
        private readonly CryptographyService _cryptographyService;
        private readonly MatchFactory _matchFactory;
        public WordleController(
            WordleService wordleService,
            CryptographyService cryptographyService,
            MatchFactory matchFactory)
        {
            _wordleService = wordleService;
            _cryptographyService = cryptographyService;
            _matchFactory = matchFactory;
        }

        [HttpPost("Check")]
        public async Task<Position[]> CheckAsync([FromBody] CheckModel checkRequest)
        {
            var valueBytes = Convert.FromBase64String(checkRequest.HashedWordId);
            var encryptWordId = Encoding.UTF8.GetString(valueBytes);

            var wordId = _cryptographyService.DecryptToNumber(encryptWordId);
            return await _wordleService.CheckWordAsync(wordId, checkRequest.UserText);
        }

        [HttpGet("IsValidWord")]
        public async Task<bool> IsValidWordAsync(string word)
        {
            return await _wordleService.IsValidWordAsync(word);
        }

        [HttpPut("SaveMatch")]
        public async Task SaveMatchAsync([FromBody] MatchModel matchModel)
        {
            var match = _matchFactory.CreateMatch(matchModel);
            await _wordleService.SaveMatchAsync(match);
        }

        [HttpGet("GetUserId")]
        public async Task<string> GetUserId(string username)
        {
            var user = await _wordleService.GetUserAsync(username);
            var hashedUserId = _cryptographyService.Encrypt(user.Id);

            var bytesString = Encoding.UTF8.GetBytes(hashedUserId);
            return Convert.ToBase64String(bytesString);
        }
    }
}

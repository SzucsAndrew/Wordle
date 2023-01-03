using Microsoft.EntityFrameworkCore;
using Wordle.Data;
using Wordle.Data.Entities;
using Wordle.Dll.Enums;

namespace Wordle.Dll.Services
{
    public class WordleService
    {
        private WordleDbContext _wordleDbContext { get; set; }

        public WordleService(WordleDbContext wordleDbContext)
        {
            _wordleDbContext = wordleDbContext;
        }

        public async Task<int> GetRandomWordAsync()
        {
            return await _wordleDbContext.Words.Where(x => x.IsDeactivated == false)
                                               .Select(x => x.Id)
                                               .OrderBy(x => Guid.NewGuid())
                                               .FirstAsync();
        }

        public async Task<Word?> GetWordAsync(int wordId)
        {
            return await _wordleDbContext.Words.FirstOrDefaultAsync(x => x.Id == wordId && !x.IsDeactivated);
        }

        public async Task<bool> IsValidWordAsync(string? text)
        {
            if (text == null) return false;

            return await _wordleDbContext.Words.AnyAsync(x => x.Text == text && !x.IsDeactivated);
        }

        public async Task<Position[]> CheckWordAsync(int wordId, string userText)
        {
            userText = userText.ToLower();
            await ThrowIfTextsAreNotValid(wordId, userText);

            var word = await GetWordAsync(wordId);
            var text = word!.Text;

            var result = new Position[5];
            var checkedLetters = new bool[5];
            var checkedLettersForText = new bool[5];

            //Find Correct and not include characters
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == userText[i])
                {
                    result[i] = Position.Correct;
                    checkedLetters[i] = true;
                    checkedLettersForText[i] = true;
                }
                else if (!text.Contains(userText[i]))
                {
                    result[i] = Position.NotInclude;
                    checkedLetters[i] = true;
                }
            }

            //Find Include and Not Include
            for (int i = 0; i < checkedLetters.Length; i++)
            {
                if (!checkedLetters[i])
                {
                    checkedLetters[i] = true;
                    var added = false;
                    for (int j = 0; j < text.Length; j++)
                    {
                        if (!checkedLettersForText[j] && text[j] == userText[i])
                        {
                            checkedLettersForText[j] = true;
                            result[i] = Position.Include;
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        result[i] = Position.NotInclude;
                    }
                }
            }

            return result;
        }

        public async Task SaveMatchAsync(Match match) 
        {
            match.Id = 0;
            _wordleDbContext.Matches.Add(match);
            await _wordleDbContext.SaveChangesAsync();
        }

        private async Task ThrowIfTextsAreNotValid(int wordId, string userText)
        {
            var word = await GetWordAsync(wordId);
            if (word == null || !await IsValidWordAsync(userText)) throw new Exception("Invalid word!");
            if (word.Text.Length != userText.Length) throw new Exception("Lengths are not matched!");
        }

        public async Task<User> GetUserAsync(string username)
        {
            var user = await _wordleDbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null) 
            {
                user = new User
                {
                    UserName = username
                };

                await _wordleDbContext.Users.AddAsync(user);
                await _wordleDbContext.SaveChangesAsync();
            }

            return user;
        }
    }
}

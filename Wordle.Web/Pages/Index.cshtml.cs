using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Wordle.Dll.Services;

namespace Wordle.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WordleService _wordleService;
        private readonly CryptographyService _cryptographyService;

        public IndexModel(WordleService wordleService, CryptographyService cryptographyService)
        {
            _wordleService = wordleService;
            _cryptographyService = cryptographyService;
        }

        public string WordId { get; set; }

        public async Task OnGet()
        {
            var wordId = await _wordleService.GetRandomWordAsync();
            var bytesString = Encoding.UTF8.GetBytes(_cryptographyService.Encrypt(wordId));
            WordId = Convert.ToBase64String(bytesString);
        }
    }
}
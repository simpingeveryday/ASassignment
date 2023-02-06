using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASassignment.Pages
{
    [Authorize]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger _logger;

        public PrivacyModel(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("MyPrivacy");
        }

        public void OnGet()
        {
            _logger.LogInformation("GET ASassignment.Pages.Privacy called");
        }
    }
}
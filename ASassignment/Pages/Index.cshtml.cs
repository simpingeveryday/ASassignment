using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASassignment.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SessionService _session;
        

        public IndexModel(ILogger<IndexModel> logger, SessionService session)
        {
            _logger = logger;
            _session = session;
            
        }

        public void OnGet()
        {
            throw new Exception("123");
            _logger.LogInformation("Home page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

            _logger.LogInformation("GET ASassignment.Pages.Index called.");

            string sessionId = Request.Cookies["sessionId"];
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                HttpContext.Session.Remove("sessionId");
                HttpContext.Response.Redirect("Login");
            }

        }
        public void OnPost()
    {
      // Set the session variable.
      _session.Username = "foo@bar";
           
    }
    }
}
using ASassignment.Model;
using ASassignment.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp_Core_Identity.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASassignment.Pages
{
    public class detailpageModel : PageModel
    {

        [BindProperty]
        public ApplicationUser detail { get; set; }
        private UserManager<ApplicationUser> UserManager { get; }
        private readonly GooglereCaptchaService googlereCaptchaService;
        private readonly AuthDbContext _context;
        private readonly ILogger _logger;


        public detailpageModel(UserManager<ApplicationUser> usermanager, GooglereCaptchaService googlereCaptchaService, AuthDbContext _context, ILoggerFactory logger)
        {
            this.UserManager = usermanager;
            this.googlereCaptchaService = googlereCaptchaService;
            this._context = _context;
            this._logger = logger.CreateLogger("Mydetailpage");
        }
        public void OnGet()
        {
            _logger.LogInformation("Home page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

            _logger.LogInformation("GET ASassignment.Pages.detailpage called.");
            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("MySecretKey");
            string userId = UserManager.GetUserId(User);
            ApplicationUser? loggeduser = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
            detail = loggeduser;
            _logger.LogInformation("Retrieving customer details.");
            detail.FirstName = protector.Unprotect(detail.FirstName);
            detail.LastName = protector.Unprotect(detail.LastName);
            detail.Gender = protector.Unprotect(detail.Gender);
            detail.NRIC = protector.Unprotect(detail.NRIC);
            detail.Resume = protector.Unprotect(detail.Resume);
            detail.WhoamI = protector.Unprotect(detail.WhoamI);
            _logger.LogInformation("Retrieved Customer details");


            string sessionId = Request.Cookies["sessionId"];
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                
                HttpContext.Session.Remove("sessionId");
                HttpContext.Response.Redirect("Login");
            }
        }
        private string displayUserName;

        
    }
}

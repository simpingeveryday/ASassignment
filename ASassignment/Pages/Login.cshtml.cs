using ASassignment.Model;
using ASassignment.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using WebApp_Core_Identity.Model;

namespace ASassignment.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        /*[BindProperty]
        public ApplicationUser detail { get; set; }*/
        private readonly SignInManager<ApplicationUser> signInManager; 
        private readonly GooglereCaptchaService googlereCaptchaService;
        private readonly IHttpContextAccessor context;
        private readonly AuthDbContext dbcontextt;
        private UserManager<ApplicationUser> usermanager { get; }

        public LoginModel(SignInManager<ApplicationUser> signInManager, GooglereCaptchaService googlereCaptchaService, IHttpContextAccessor context, AuthDbContext dbcontextt)
        {
            this.signInManager = signInManager;
            this.googlereCaptchaService = googlereCaptchaService;
            this.context = context;
            this.dbcontextt = dbcontextt;
        }
        public void OnGet()
        {
            /*string sessionId = Request.Cookies["sessionId"];

            if (String.IsNullOrEmpty(sessionId) && sessionId.Equals(HttpContext.Session.GetString("sessionId")))
            {
                HttpContext.Session.Remove("sessionId");
                HttpContext.Response.Redirect("Login");

            }
            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("MySecretKey");
            protector.Unprotect(LModel.Email);*/
            

            /*string userId = usermanager.GetUserId(User);
            ApplicationUser? currentUser = dbcontextt.Users.FirstOrDefault(x => x.Id.Equals(userId));
            detail = currentUser;*/
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var _GooglereCaptchah = googlereCaptchaService.VerifyCaptcha(LModel.Token);

            if (!_GooglereCaptchah.Result.success && _GooglereCaptchah.Result.score <= 0.5)
            {
                ModelState.AddModelError(String.Empty, "You not Human");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");
               /* var lol = protector.Unprotect(LModel.Email);*/
                var identityResult = await signInManager.PasswordSignInAsync((LModel.Email), LModel.Password, LModel.RememberMe, true);
                if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "u hve been locked out for 5Minutes");
                }
                else if (identityResult.Succeeded)
                {
                    /*if (identityResult.IsLockedOut)
                    {
                        ModelState.AddModelError("", "u hve been locked out");
                    }*/
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, "c@c.com"),
                        new Claim(ClaimTypes.Email, "c@c.com"),
                       
};
                    var i = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    Guid sessionId = Guid.NewGuid();

                    HttpContext.Session.SetString("sessionId", sessionId.ToString());
                    Response.Cookies.Append("sessionId", sessionId.ToString());


                    return RedirectToPage("Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }
    }
}

using ASassignment.Model;
using ASassignment.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using System.Drawing;
namespace ASassignment.Pages
{
    
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        
    


        public RegisterModel(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
 
        }
        public void OnGet()
        {
            
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");
                
                var user = new ApplicationUser()
                {
                    UserName = (RModel.Email),
                    Email = (RModel.Email),
                    FirstName = protector.Protect(RModel.FirstName),
                    LastName = protector.Protect(RModel.LastName),
                    Gender = protector.Protect(RModel.Gender),
                    NRIC = protector.Protect(RModel.NRIC),
                    DateOfBirth = RModel.DateOfBirth,
                    Resume = protector.Protect(RModel.Resume),
                    WhoamI = protector.Protect(RModel.WhoamI)
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }




            }
            return Page();
        }
    }
}
/*First Name
• Last Name
• Gender
• NRIC (Must be encrpyped)
• Email address(Must be unique)
• Password
• Confirm Password
• Date of Birth
• Resume (.docx or .pdf file)
• WhoamI(allow all special chars)*/
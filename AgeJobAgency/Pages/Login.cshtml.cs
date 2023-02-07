using AgeJobAgency.Models;
using AgeJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgeJobAgency.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly GoogleCaptchaService _captchaService;
        public LoginModel(SignInManager<ApplicationUser> signInManager, GoogleCaptchaService googleCaptchaService)
        {
            this.signInManager = signInManager;
            _captchaService = googleCaptchaService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var _GoogleCaptcha = _captchaService.VerifyreCaptcha(LModel.Token);
            if(!_GoogleCaptcha.Result.success && _GoogleCaptcha.Result.score >= 0.5)
            {
                ModelState.AddModelError("", "You are not human");
                return Page();
            }
      
            if (ModelState.IsValid)
            {
                 var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                 LModel.RememberMe, false);
                 if (identityResult.Succeeded)
                 {
                    return RedirectToPage("Index");
                 }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
    
            return Page();
        }
    }
}

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgeJobAgency.ViewModels;
using AgeJobAgency.Models;
using System.Web;

namespace AgeJobAgency.Pages
{
    public class RegisterModel : PageModel
    {
        public string QrCodeUrl { get; set; }
        public string ManualEntryCode { get; set; }

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private IWebHostEnvironment _environment;

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        protected void btnbtn_submit_Click(object sender, EventArgs e)
        {
            RModel.EmailAddress = HttpUtility.HtmlEncode(RModel.EmailAddress);
            RModel.Password = HttpUtility.HtmlEncode(RModel.Password);
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
                    UserName = RModel.EmailAddress,
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    Gender = RModel.Gender,
                    NRIC = protector.Protect(RModel.NRIC),
                    EmailAddress = RModel.EmailAddress,
                    Password = RModel.Password,
                    ConfirmPassword = RModel.ConfirmPassword,
                    DateofBirth = RModel.DateofBirth,
                    Resume = RModel.Resume,
                    WhoamI = RModel.WhoamI,
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload",
                        "File size cannot exceed 2MB.");
                        return Page();
                    }
                    if (Upload.ContentType != "application/pdf, application/doc, application/docx")
                    {
                        ModelState.AddModelError("Upload", "Only Allow PDF, DOC or DOCx");
                        return Page();
                    }
                    var uploadsFolder = "uploads";
                    var ResumeFile = Guid.NewGuid() + Path.GetExtension(
                    Upload.FileName);
                    var ResumePath = Path.Combine(_environment.ContentRootPath,
                    "wwwroot", uploadsFolder, ResumeFile);
                    using var fileStream = new FileStream(ResumePath,
                    FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    RModel.Resume = string.Format("/{0}/{1}", uploadsFolder,
                    ResumeFile);
                }
            }
            return Page();
        }
    }
}

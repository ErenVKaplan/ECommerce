using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.Data.Entities;
using Project.Data.ViewModels.Users;
using Project.Service.Services;
using Project.Service.Services.Concrete;

namespace Project.Web.Controllers
{
    
    [Area("Account")]
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _UserManager;
        private readonly UserAccountService _UserAccountService;
        private readonly IToastNotification _toastNotification;
		public UserController(UserManager<AppUser> userManager, UserAccountService userAccountService, IToastNotification toastNotification, SignInManager<AppUser> signInManager)
		{

			_UserManager = userManager;
			_UserAccountService = userAccountService;
			_toastNotification = toastNotification;
			_signInManager = signInManager;
		}

		public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSingUpViewModel request)
        {
            ServiceResponse<object> response = await _UserAccountService.CreateUserAsync(request);

            if(response.HasError) 
            {
                foreach (var item in response.ErrorMessages)
                {
                    _toastNotification.AddErrorToastMessage(item, new ToastrOptions { Title = "Hata!" });
                    return View();
                }
            }
            //Başarılı mesajı
            _toastNotification.AddSuccessToastMessage(response.SuccessMessages.First(), new ToastrOptions { Title = "Başarılı!" });
            return View();
     
        }

        public IActionResult SignIn() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel request, string? ReturnUrl = null)
        {
            ReturnUrl = ReturnUrl ?? Url.Action("Index", "Home");//Eğer returnurl null değilse kendi değeri atanır.Nullsa İndex Home gider.
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError(string.Empty, "Lütfen Her Yeri Doldurunuz");
                return View();
            }
            ServiceResponse<object> response = await _UserAccountService.UserLoginAsync(request);

            if (response.HasError)
            {
                foreach (var item in response.ErrorMessages)
                {
                    _toastNotification.AddErrorToastMessage(item, new ToastrOptions { Title = "Hata!" });
                    return View();
                }
            }
            //Başarılı mesajı
            _toastNotification.AddSuccessToastMessage(response.SuccessMessages.First(), new ToastrOptions { Title = "Başarılı!" });
            return RedirectToAction("Index","Home", new {Area=""});
           
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        
    }
}

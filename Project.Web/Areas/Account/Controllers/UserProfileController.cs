using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Project.Data.Entities;
using Project.Data.ViewModels.Users;
using Project.Service.Services;
using Project.Service.Services.Concrete;

namespace Project.Web.Areas.Account.Controllers
{
	[Authorize(Roles ="User,Admin")]
	[Area("Account")]
	public class UserProfileController : Controller
	{
		private readonly UserProfileService _userProfileService;
		private readonly IToastNotification _toastrNotification;
        private readonly UserManager<AppUser> _UserManager;
        public UserProfileController(UserProfileService userProfileService, IToastNotification toastrNotification, UserManager<AppUser> userManager)
        {
            _userProfileService = userProfileService;
            _toastrNotification = toastrNotification;
            _UserManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));
            var response = await _userProfileService.FindUserAndFillInAsync(User.Identity!.Name!);

           
           
                var userProfile = (UserProfileViewModel)response.Data;
                return View(userProfile);
          
      
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel request)
        {
           
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var response = await _userProfileService.UpdateUserEntitiesAsync(request);

            if (response.IsSuccess)
            {
                // Başarı durumunu yönet, örneğin:
                return View((UserProfileViewModel)response.Data);
            }
            else
            {
                // Hata durumunu yönet, örneğin:
                
                return View(request); // veya başka bir işlem yapabilirsiniz.
            }
        }


        public IActionResult ResetPassword()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            
            var user = await _UserManager.FindByNameAsync(User.Identity!.Name!.ToString());
            string username = user!.UserName!;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty,"Bütün verileri doldurunuz");
            }
            ServiceResponse<object> result =await _userProfileService.ResetUserPasswordAsync(request, username);
            if(result.HasError) 
            {
                _toastrNotification.AddErrorToastMessage(result.ErrorMessages.First(), new ToastrOptions { Title = "Hata!" });
                return View(request);
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult UserOrders(Guid UserId)
        {
		  var list= _userProfileService.GetOrderList(UserId);

			return View(list);
        }
    }
}

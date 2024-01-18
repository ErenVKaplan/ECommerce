using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using Project.Data.ViewModels.Users;
using Project.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Concrete
{
    public class UserAccountService:ControllerBase,IUserAccountService

    {
       
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserAccountService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
           
        }
        public async Task<ServiceResponse<object>> CreateUserAsync(UserSingUpViewModel request)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();

            var newUser = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(newUser, request.PasswordConfirm!);

            if (result.Succeeded)
            {
                var userRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == "User");

                if (userRole != null)
                {
                    await _userManager.AddToRoleAsync(newUser, userRole.Name!);
                    response.AddSuccessMessage("Kayıt İşlemi başarılı!");
                }
                else
                {
                    response.AddErrorMessage("User rolü bulunamadı.");
                }

                return response;
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    response.AddErrorMessage(err.Description);
                }

                return response;
            }
        }

        public async Task<ServiceResponse<object>> UserLoginAsync(UserSignInViewModel request)
        {
            ServiceResponse<object> response=new ServiceResponse<object>();
            


            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.AddErrorMessage("Girdiğiniz Bilgilerle Kayıtlı Kullanıcı Bulunamamıştır");
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (result.IsLockedOut) //Eger kullanici lockout olduysa true olur ve hata mesaji gider.
            {
                response.AddErrorMessage("3 dakika boyunca giris yapamazsiniz.");
                return response;
            }
            if (result.Succeeded)
            {
                response.AddSuccessMessage("Giriş İşlemi Başarılı!");
            }
            else
            {
                response.AddErrorMessage("Giriş İşlemi Başarısız!");
            }

            return response;

        }
    }
}
          

using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Project.Data.Context;
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
	public class UserProfileService : IUserProfileService
	{
        private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _UserManager;
		private readonly SignInManager<AppUser> _SignInManager;
		
		public UserProfileService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
		{
			_UserManager = userManager;
			_SignInManager = signInManager;
			_context = context;
		}
		public List<Order> GetOrderList(Guid userId)
		{
			var userOrders = _context.Orders
		.Where(o => o.UserId == userId)
		.Select(p => new Order
		{
			Id = p.Id,
            DeliverAddress=p.DeliverAddress,
            TotalPrice = p.TotalPrice,
            ShippingMethod = p.ShippingMethod,
            PaymentMethod = p.PaymentMethod,
            CreatedDate = p.CreatedDate,
			
		})
		.ToList();

			return userOrders;
		}
		public async Task<ServiceResponse<UserProfileViewModel>> FindUserAndFillInAsync(string UserName)
		{
           
			var CurrentUser = await _UserManager.FindByNameAsync(UserName);

			var response = new ServiceResponse<UserProfileViewModel>();

			if (CurrentUser == null)
			{
			
				response.AddErrorMessage("Kullanıcı bulunamadı.");
				return response;
			}

			var UserProfileViewModel = new UserProfileViewModel()
			{FirstName = CurrentUser.FirstName,
            LastName = CurrentUser.LastName,
				UserName = CurrentUser.UserName!,
				Email = CurrentUser.Email!,
				PhoneNumber = CurrentUser.PhoneNumber,
				Address = CurrentUser.Address,
				BirthDate = CurrentUser.BirthDate,
				Gender = CurrentUser.Gender
			};

			response.Data = UserProfileViewModel;
			return response;
		}

        public async Task<ServiceResponse<object>> UpdateUserEntitiesAsync(UserProfileViewModel request)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();
            var CurrentUser = await _UserManager.FindByNameAsync(request.UserName);

            if (CurrentUser == null)
            {
                response.AddErrorMessage("Kullanıcı bulunamadı.");
                return response;
            }

            CurrentUser.FirstName = request.FirstName;
            CurrentUser.LastName = request.LastName;
            CurrentUser.Email = request.Email;
            CurrentUser.PhoneNumber = request.PhoneNumber;
            CurrentUser.UserName = request.UserName;
            CurrentUser.BirthDate = request.BirthDate;
            CurrentUser.Address = request.Address;
            CurrentUser.Gender= request.Gender;
            var result = await _UserManager.UpdateAsync(CurrentUser);

            if (result.Succeeded)
            {
                response.Data = new UserProfileViewModel
                {
                    UserName = CurrentUser.UserName,
                    Email = CurrentUser.Email,
                    PhoneNumber = CurrentUser.PhoneNumber,
                    BirthDate = CurrentUser.BirthDate,
                    Address = CurrentUser.Address,
                    Gender = CurrentUser.Gender,
                    FirstName = CurrentUser.FirstName,
                    LastName = CurrentUser.LastName
                };

                await _SignInManager.SignOutAsync();
                await _SignInManager.SignInAsync(CurrentUser, true);
                response.AddSuccessMessage("Kullanıcı bilgileri güncellendi.");
            }
            else
            {
                response.AddErrorMessage("Kullanıcı bilgileri güncellenirken bir hata oluştu.");
            }

            return response;
        }

        public async Task<ServiceResponse<object>> ResetUserPasswordAsync(ResetPasswordViewModel request, string username)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();
            var CurrentUser = (await _UserManager.FindByNameAsync(username));
            if (CurrentUser == null) 
            {
                response.AddErrorMessage($"{username} bulunamadı");
                return response;
            }
            var checkOldPassword = await _UserManager.CheckPasswordAsync(CurrentUser,request.OldPassword);
            if(!checkOldPassword) 
            {
                response.AddErrorMessage("Şifreler uyuşmamaktadır!");
                return response;
            }
            var result = await _UserManager.ChangePasswordAsync(CurrentUser, request.OldPassword, request.NewPassword);
            if(!result.Succeeded) 
            {
                response.AddErrorMessage("Şifre değiştirme başarısız oldu!");
                return response;
            }
            await _UserManager.UpdateSecurityStampAsync(CurrentUser);
            await _SignInManager.SignOutAsync();
            await _SignInManager.PasswordSignInAsync(CurrentUser, request.NewPassword, true, false);
            return response;



        }
    }
}

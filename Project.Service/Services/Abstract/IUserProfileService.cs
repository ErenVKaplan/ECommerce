using Project.Data.Entities;
using Project.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Abstract
{
	public interface IUserProfileService
	{
		Task<ServiceResponse<UserProfileViewModel>> FindUserAndFillInAsync(string UserName);
		Task<ServiceResponse<object>> UpdateUserEntitiesAsync(UserProfileViewModel request);
		Task<ServiceResponse<object>> ResetUserPasswordAsync(ResetPasswordViewModel request, string username);
		public List<Order> GetOrderList(Guid userId);


	}
}

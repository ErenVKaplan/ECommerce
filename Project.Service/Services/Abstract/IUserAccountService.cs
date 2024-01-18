using Project.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Abstract
{
    public interface IUserAccountService
    {

        Task<ServiceResponse<object>> CreateUserAsync(UserSingUpViewModel request);

        Task<ServiceResponse<object>> UserLoginAsync(UserSignInViewModel request);
    }
}

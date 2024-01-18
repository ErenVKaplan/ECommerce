using Project.Data.ViewModels.Admin;
using Project.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Abstract
{
	public interface IContactUsService
	{
		public  Task<ServiceResponse<object>> ContactUs(NeedHelpViewModel request);
		List<NeedHelpViewModel> GetContactList();
	}
}

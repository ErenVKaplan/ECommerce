using Project.Data.Context;
using Project.Data.Entities;
using Project.Data.ViewModels.Admin;
using Project.Data.ViewModels.Users;
using Project.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Concrete
{
	public class ContactUsService : IContactUsService
	{
		private readonly AppDbContext _context;

		public ContactUsService(AppDbContext context)
		{
			_context = context;
		}

		public async  Task<ServiceResponse<object>> ContactUs(NeedHelpViewModel request)
		{
			ServiceResponse<object> response = new ServiceResponse<object>();
			var ContactUsModel = new ContactUs
			{
				Email = request.Email,
				Text = request.Text,
				CreatedBy = "Anonymous",
				CreatedDate = DateTime.Now,
			};
			_context.ContactUs.Add(ContactUsModel);
			_context.SaveChanges();
			return response;
		}

		public List<NeedHelpViewModel> GetContactList()
		{
			var info = _context.ContactUs.Select(p => new NeedHelpViewModel
			{
				Email=p.Email,
				Text=p.Text,
			}).ToList();

			return info;
		}
	}
}

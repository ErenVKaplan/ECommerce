using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstract;
using System.Reflection.Metadata.Ecma335;

namespace Project.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {private readonly IContactUsService _contactUsService;

		public HomeController(IContactUsService contactUsService)
		{
			_contactUsService = contactUsService;
		}

		[Route("admin")]
        [Route("admin/home")]
        [Route("admin/home/index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult  GettAllContactMessages() 
        {
            var list =_contactUsService.GetContactList();
            return View(list);
        }
		
	}
}

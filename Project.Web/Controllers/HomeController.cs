using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.Data.ViewModels.Admin;
using Project.Data.ViewModels.Users;
using Project.Service.Services.Abstract;
using Project.Service.Services.Concrete;

namespace Project.Web.Controllers
{
    public class HomeController : Controller
    {   private readonly IContactUsService contactUsService;
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IToastNotification _toastNotification;
		public HomeController(ILogger<HomeController> logger, IProductService productService, IContactUsService contactUsService)
		{
			_logger = logger;
			_productService = productService;
			this.contactUsService = contactUsService;
		}

		public IActionResult Index()
        {
			var productList = _productService.GetProductListII();
			return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NeedHelp(NeedHelpViewModel request) 
        { 
            if (!ModelState.IsValid)
            {
				_toastNotification.AddErrorToastMessage("Lütfen her yeri doldurunuz", new ToastrOptions { Title = "Hata!" });
                return View();
			}
             await contactUsService.ContactUs(request);
			return await Task.FromResult(Redirect(Request.Headers["Referer"].ToString()));
		}
    }
}
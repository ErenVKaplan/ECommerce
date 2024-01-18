using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using NuGet.Protocol;
using Project.Data.ViewModels.Admin;
using Project.Service.Services;
using Project.Service.Services.Abstract;
using Project.Service.Services.Concrete;

namespace Project.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
	{
		private readonly IProductService _productService;
        private readonly IToastNotification _toastrNotification;
        public ProductController(IProductService productService, IToastNotification toastrNotification)
        {
            _productService = productService;
            _toastrNotification = toastrNotification;
        }

        public IActionResult ProductAdd() 
		{
			//var model = await _productService.FindProducyById(id);
			//return View(model);

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ProductAdd(ProductAddViewModel request)
		{	ServiceResponse<object> response=new ServiceResponse<object>();
			var result = await _productService.AddProductAsync(request);
			if(result ==null) 
			{
				response.AddErrorMessage("Ürün bulunamadığı için eklenememiştir");
               
                return View(response);
			}
			response.AddSuccessMessage("Ürün başarıyla eklenmiştir.");
           

			var ProductAddViewModel = new ProductAddViewModel()
			{
				ProductName = request.ProductName,
				ProductDescription = request.ProductDescription,
				ProductPrice = request.ProductPrice,
                ProductDiscount = request.ProductDiscount,
                ProductFeatures= request.ProductFeatures,
			};

            return View(ProductAddViewModel);
		}
        public IActionResult Products()
        {
            var products = _productService.GetProductList();

            

            return View(products);
        }


		
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			 await _productService.DeleteProductAsync(id);
            return RedirectToAction("Products", "Product", new { area = "Admin" });
		}


        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var result = await _productService.FindProductByIdAsyncII(id);

            if (result.HasError)
            {
                result.AddErrorMessage("Ürün getirilemedi");
                return View();
            }

            var productInfo = result.Data;

            return View(productInfo);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct( ProductAddViewModel updatedProduct)
        {
            // Güncellenmiş bilgileri kullanarak ürünü güncelle
            await _productService.UpdateProductAsync(updatedProduct);

            // Başarılı bir güncelleme durumunda, kullanıcıyı güncellenmiş ürünün detaylarını görebileceği bir sayfaya yönlendirin
            return RedirectToAction("Products", "Product", new { Area = "Admin" });
        }


    }
}

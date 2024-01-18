using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Entities;
using Project.Data.ViewModels.Shop;
using Project.Service.Extensions;
using Project.Service.Services.Abstract;
using Project.Service.Services.Concrete;
using System.Security.Claims;
using Project.Web.Extensions;
using NToastNotify;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Data.ViewModels.Users;
using Project.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;

namespace Project.Web.Areas.Account.Controllers
{
	[Area("Account")]
	public class ShopController : Controller
	{ private readonly IProductService _productService;
		private readonly IShopService _shopService;
        private readonly CommentService _CommentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToastNotification toastNotification;
		public ShopController(IShopService shopService, CommentService commentService, IHttpContextAccessor httpContextAccessor, IProductService productService, IToastNotification toastNotification)
		{
			_shopService = shopService;
			_CommentService = commentService;
			_httpContextAccessor = httpContextAccessor;
			_productService = productService;
			this.toastNotification = toastNotification;
		}

		[HttpGet]
        public async Task<IActionResult> QuickView(Guid id)
        {
            var response = await _shopService.ProductQuickViewAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
                
                return Redirect(Request.Headers["Referer"].ToString()); 
            }
        }

        

        [HttpPost]
        public IActionResult CreateProductReview(CommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                
                var comment = new Comment
                {
                    ProductId = viewModel.ProductId,
                    CommentText = viewModel.CommentText,
                    Rating = viewModel.Rating,
                    UserId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId(),
                CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity!.Name!,
					LikeCount= viewModel.LikeCount,
                };

                _CommentService.UpdateProductRating(viewModel.ProductId, viewModel.Rating);
                _CommentService.AddComment(comment);

                return RedirectToAction("QuickView", "Shop", new { productId = comment.ProductId });
            }

           return  Redirect(Request.Headers["Referer"].ToString());
        }

		[HttpGet]
		public async Task<IActionResult> AddToCart(Guid Id)
		{
            Dictionary<Guid, int> ProductIdsAndQuantities = HttpContext.Session.GetObject<Dictionary<Guid, int>>("cart");


            if (ProductIdsAndQuantities == null)
                ProductIdsAndQuantities = new Dictionary<Guid, int>();


            if (ProductIdsAndQuantities.ContainsKey(Id))
            {
                ProductIdsAndQuantities[Id]++;
            }
            else
            { 
                ProductIdsAndQuantities[Id] = 1;
            }


            HttpContext.Session.SetObject("cart", ProductIdsAndQuantities);
            toastNotification.AddSuccessToastMessage("Ürün sepete ekelndi!", new ToastrOptions { Title = "Başarılı!" });
            return await Task.FromResult(Redirect(Request.Headers["Referer"].ToString()));
		}
		[HttpGet]
		public IActionResult Cart()
		{
			var cart = HttpContext.Session.GetObject<Dictionary<Guid, int>>("cart");
			List<BasketProduct> products = new List<BasketProduct>();
			if (cart != null)
			{
				products = _productService.GetProductsFromIds(cart);
			}

			float totalPrice = products.Sum(x => x.Product.ProductPrice - (x.Product.ProductPrice * (x.Product.ProductDiscount / 100)));

			ViewBag.ShippingMethod = new SelectList(Enum.GetNames(typeof(ShippingMethod)));
			ViewBag.PaymentMethod = new SelectList(Enum.GetNames(typeof(PaymentMethod)));

			var orderViewModel = new OrderViewModel
			{
				TotalPrice = totalPrice
				
			};

			return View(orderViewModel);
		}
		[HttpPost]
		public IActionResult Cart(OrderViewModel orderViewModel)
		{
			if (ModelState.IsValid)
			{
				if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
				{
					
					var productsInSession = HttpContext.Session.GetObject<Dictionary<Guid, int>>("cart");

					if (productsInSession != null && productsInSession.Any())
					{

						var orderEntity = new Order
						{
							UserId = userId,
							DeliverAddress = orderViewModel.DeliverAddress,
							TotalPrice = orderViewModel.TotalPrice,
							ShippingMethod = orderViewModel.ShippingMethod,
							PaymentMethod = orderViewModel.PaymentMethod,
							CreatedBy = User.Identity.Name,
							CreatedDate = DateTime.Now
						};

						var basketEntity = new Basket
						{
							Order = orderEntity,  // Parent-child ilişkisi
							CreatedBy = User.Identity.Name,
							CreatedDate = DateTime.Now
						};

						var basketProductEntities = productsInSession.Select(productViewModel => new BasketProduct
						{
							ProductId = productViewModel.Key,
							Quantity = productViewModel.Value,
							Basket = basketEntity,
							CreatedBy= User.Identity.Name,
							CreatedDate = DateTime.Now
						}).ToList();


						_shopService.AddOrder(orderEntity);
						_shopService.AddBasketEntity(basketEntity);
						foreach (var basketProductEntity in basketProductEntities)
						{
							basketProductEntity.BasketId = basketEntity.Id;
						}

						_shopService.AddRangeOfEntities(basketProductEntities);

						
						HttpContext.Session.Remove("cart");

						return RedirectToAction("Index", "Home", new { Area = "" });
					}
				}
				else
				{
					return RedirectToAction("ViewCart", "Home", new { Area = "Account" });
				}
			}

			ViewBag.ShippingMethod = new SelectList(Enum.GetNames(typeof(ShippingMethod)));
			ViewBag.PaymentMethod = new SelectList(Enum.GetNames(typeof(PaymentMethod)));

			return View(orderViewModel);
		}




		[HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            var cart = HttpContext.Session.GetObject<Dictionary<Guid, int>>("cart");

			if (cart != null && cart.Any(item => item.Key == productId))
			{
				cart[productId]--;

				if (cart[productId] == 0)
				{
					cart.Remove(productId);
				}

				HttpContext.Session.SetObject("cart", cart);
			}


			return await Task.FromResult(Redirect(Request.Headers["Referer"].ToString()));
        }

		[HttpGet]
		public IActionResult LikeCounter(Guid id)
		{
			_CommentService.UpdateCommentLike(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult DislikeCounter(Guid id)
        {
            _CommentService.UpdateCommentDislike(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public  async Task<IActionResult> ViewCart()
        {
            return View();
        }
	}

}

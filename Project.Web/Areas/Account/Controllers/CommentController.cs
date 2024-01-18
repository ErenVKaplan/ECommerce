using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Project.Data.Entities;
using Project.Data.ViewModels.Shop;
using Project.Service.Services.Concrete;

namespace Project.Web.Areas.Account.Controllers
{
	public class CommentController : Controller
	{
		private readonly AppUser _appUser;
		private readonly ProductService _productService;
		private readonly CommentService _CommentService;

		public CommentController(CommentService commentService, ProductService productService, AppUser appUser)
		{
			_CommentService = commentService;
			_productService = productService;
			_appUser = appUser;
		}
		[HttpGet]
		public async Task<IActionResult>CreateComment(Guid ProductId)
		{
			var product = await _productService.FindProducyById(ProductId);
			if (product == null)
			{
				return NotFound();
			}

			return View(new CommentViewModel { ProductId = ProductId });
		}
		[HttpPost]
		public IActionResult CreateComment(CommentViewModel request)
		{
			if (ModelState.IsValid)
			{
				
				var comment = new Comment
				{
					ProductId = request.ProductId,
					CommentText = request.CommentText,
					Rating = request.Rating
				};
				
				_CommentService.AddComment(comment);

				
				return RedirectToAction("Details", "Product", new { id = request.ProductId });
			}

			return View(request);
		}
	}
}

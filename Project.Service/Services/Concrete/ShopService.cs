using Microsoft.EntityFrameworkCore;
using Project.Data.Context;
using Project.Data.Entities;
using Project.Data.ViewModels.Shop;
using Project.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Concrete
{
	public class ShopService : IShopService
	{
		private readonly AppDbContext _context;

		public ShopService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<object>> ProductQuickViewAsync(Guid id)
		{
			ServiceResponse<object> response = new ServiceResponse<object>();
			try
			{
				var currentProduct = await _context.Products.Include(x => x.Comments).ThenInclude(x => x.AppUser).FirstOrDefaultAsync(a => a.Id == id);

				if (currentProduct != null)
				{

					response.IsSuccess = true;
					response.Data = currentProduct;
				}
				else
				{
					// Ürün bulunamadıysa hata durumu
					response.IsSuccess = false;
					response.AddErrorMessage("Ürün bulunamadı.");
				}
			}
			catch (Exception ex)
			{
				// Hata durumunu loglama veya handle etmek için uygun bir işlem yapabilirsiniz.
				response.IsSuccess = false;
				response.AddErrorMessage("Ürün detayları alınamadı.");
			}

			return response;
		}

		public void AddOrder(Order order)
		{
			_context.Orders.Add(order);
			_context.SaveChanges();
		}
		public void AddBasketEntity(Basket entity)
		{
			_context.Baskets.Add(entity);
			_context.SaveChanges();
		}
		public void AddRangeOfEntities(IEnumerable<BasketProduct> entities)
		{
			_context.BasketProducts.AddRange(entities);
			_context.SaveChanges();
		}
		public void AddEntity(BasketProduct entity)
		{
			
			var existingEntity = _context.BasketProducts.FirstOrDefault(bp => bp.Id == entity.Id);
				_context.BasketProducts.Add(entity);
				_context.SaveChanges();
			
		}
	}
	}

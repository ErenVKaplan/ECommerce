using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Abstract
{
	public interface IShopService
	{
        Task<ServiceResponse<object>> ProductQuickViewAsync(Guid id);
		void AddOrder(Order order);
		public void AddRangeOfEntities(IEnumerable<BasketProduct> entities);
		public void AddEntity(BasketProduct entity);
		public void AddBasketEntity(Basket entity);
	}
}

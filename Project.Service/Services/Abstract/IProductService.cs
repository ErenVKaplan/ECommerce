using Project.Data.Entities;
using Project.Data.ViewModels.Admin;
using Project.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Abstract
{
    public interface IProductService
    {
        List<ProductListViewModel> GetProductListII();
		List<ProductListViewModel> GetProductList();

		Task<ServiceResponse<object>> FindProducyById(Guid id);
        Task<ServiceResponse<object>> AddProductAsync(ProductAddViewModel request);

        Task<ServiceResponse<object>> DeleteProductAsync(Guid id);
        Task<ServiceResponse<ProductAddViewModel>> FindProductByIdAsyncII(Guid id);

        Task<ServiceResponse<object>> UpdateProductAsync(ProductAddViewModel updatedProduct);
        List<BasketProduct> GetProductsFromIds(Dictionary<Guid, int> productIdsAndQuantities);


    }
}

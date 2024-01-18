using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Project.Data.Context;
using Project.Data.Entities;
using Project.Data.ViewModels.Admin;
using Project.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        public ProductService(AppDbContext context, IFileProvider fileProvider, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _fileProvider = fileProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<ProductListViewModel> GetProductList()
        {
            var products = _context.Products.Select(p => new ProductListViewModel
            {
                id= p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                ProductDiscount = p.ProductDiscount,
                ProductFeatures = p.ProductFeatures,
            }).ToList();

            return products;
        }
		public List<ProductListViewModel> GetProductListII()
		{
            var products = _context.Products.Select(p => new ProductListViewModel
            {
                id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                ProductPicUrl = p.ProductPicture,
                ProductDiscount= p.ProductDiscount,
                ProductFeatures = p.ProductFeatures
            }).ToList();

			return products;
		}
		public async Task<ServiceResponse<object>> FindProducyById(Guid id)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();

            var currentProduct = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (currentProduct != null)
            {
                var productAddViewModel = new ProductAddViewModel()
                {   
                    
                    ProductName = currentProduct.ProductName,
                    ProductDescription = currentProduct.ProductDescription,
                    ProductPrice = currentProduct.ProductPrice,
                };

                response.Data = productAddViewModel;
            }
            else
            {
                response.IsSuccess = false;
                response.AddErrorMessage("Ürün bulunamadı.");
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<object>> AddProductAsync(ProductAddViewModel request)
        {
           
                ServiceResponse<object> response = new ServiceResponse<object>();

                try
                {
                    
                    var newProduct = new Product
                    {
                        ProductName = request.ProductName,
                        ProductPrice = request.ProductPrice,
                        ProductDescription = request.ProductDescription,
                        CreatedBy="Admin",
                        ProductDiscount= (float)request.ProductDiscount,
                        ProductFeatures= request.ProductFeatures
                    };

                    
                    if (request.PictureUrl != null && request.PictureUrl.Length > 0)
                    {
                        var wwwroot = _fileProvider.GetDirectoryContents("wwwroot");
                        string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.PictureUrl.FileName)}";
                        var newPicturePath = Path.Combine(wwwroot.First(x => x.Name == "ProductPicture").PhysicalPath!, randomFileName);

                        // Resmi kaydet
                        using var stream = new FileStream(newPicturePath, FileMode.Create);
                        await request.PictureUrl.CopyToAsync(stream);

                        newProduct.ProductPicture = randomFileName;
                    }

                   
                    _context.Products.Add(newProduct);
                    await _context.SaveChangesAsync();

                    response.Data = true;
                }
                catch (Exception ex)
                {
                    response.AddErrorMessage("Ürün eklenirken bir hata meydana geldi!");
                    response.Data = false;
                  
                }

                return response;
            

        }

        public async Task<ServiceResponse<object>> DeleteProductAsync(Guid id)
        {
            ServiceResponse<object> response=new ServiceResponse<object>();
            var CurrentProduct = await _context.Products.FindAsync(id);
            if (CurrentProduct == null) 
            {
                response.AddErrorMessage("Ürün Bulunamadı!");
                return response;
            }
            _context.Products.Remove(CurrentProduct);
            await _context.SaveChangesAsync();
            response.AddSuccessMessage("Ürün başarıyla silindi");
            
            return response;
        }

        public async Task<ServiceResponse<ProductAddViewModel>> FindProductByIdAsyncII(Guid id)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();
            var currentProduct = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (currentProduct == null)
            {
                response.AddErrorMessage("Ürün Bulunamadı");
                return new ServiceResponse<ProductAddViewModel> { HasError = true};
            }

            var newProductInfo = new ProductAddViewModel()
            {
                ProductName = currentProduct.ProductName,
                ProductDescription = currentProduct.ProductDescription,
                ProductPrice = currentProduct.ProductPrice,
            };

            return new ServiceResponse<ProductAddViewModel> { Data = newProductInfo };
        }

        public async Task<ServiceResponse<object>> UpdateProductAsync(ProductAddViewModel updatedProduct)
        {
            ServiceResponse<object> response = new ServiceResponse<object>();

            try
            {
                // Önce güncellenecek ürünün var olduğunu kontrol edin
                var existingProduct = await _context.Products.FindAsync(updatedProduct.id);

                if (existingProduct == null)
                {
                    response.AddErrorMessage("Güncellenmek istenen ürün bulunamadı.");
                    return response;
                }

               
                existingProduct.ProductName = updatedProduct.ProductName;
                existingProduct.ProductDescription = updatedProduct.ProductDescription;
                existingProduct.ProductPrice = updatedProduct.ProductPrice;
                existingProduct.ProductDiscount = (float)updatedProduct.ProductDiscount;
                existingProduct.ProductFeatures= updatedProduct.ProductFeatures;
                
                if (updatedProduct.PictureUrl != null && updatedProduct.PictureUrl.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // IFormFile'ı MemoryStream'e kopyala
                        await updatedProduct.PictureUrl.CopyToAsync(memoryStream);

                        // MemoryStream'in başına giderek dosya adını al
                        memoryStream.Position = 0;
                        var fileName = $"{Guid.NewGuid()}_{updatedProduct.PictureUrl.FileName}";

                        // Dosyayı belirli bir konuma kaydet (örneğin wwwroot içinde bir klasöre)
                        var directoryPath = _webHostEnvironment.WebRootPath;
                        var filePath = Path.Combine(directoryPath, "uploads", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            // MemoryStream içindeki verileri FileStream'e kopyala
                            await memoryStream.CopyToAsync(fileStream);
                        }

                        // existingProduct.PictureUrl'a dosya adını ata
                        existingProduct.ProductPicture = fileName;
                    }
                }

                
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();

                response.Data = existingProduct.Id; 

                return response;
            }
            catch (Exception ex)
            {
                response.AddErrorMessage($"Ürün güncellenirken bir hata oluştu: {ex.Message}");
                return response;
            }
        }

		public List<BasketProduct> GetProductsFromIds(Dictionary<Guid, int> productIdsAndQuantities)
		{
			List<BasketProduct> basketProducts = new List<BasketProduct>();

			foreach (var entry in productIdsAndQuantities)
			{
				Guid productId = entry.Key;
				int quantity = entry.Value;

				Product product = _context.Products.Find(productId);

				if (product != null)
				{
					BasketProduct basketProduct = new BasketProduct
					{
						Product = product,
						Quantity = quantity
					};

					basketProducts.Add(basketProduct);
				}
			}

			return basketProducts;
		}



	}
}

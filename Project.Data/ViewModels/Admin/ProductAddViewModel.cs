using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Admin
{
	public class ProductAddViewModel
	{
        public Guid? id { get; set; }
        public string ProductName { get; set; }
		public string? ProductDescription { get; set; }
        public float  ProductPrice { get; set; }
        public float? ProductDiscount { get; set; }
        public string ProductFeatures { get; set; }
		public IFormFile? PictureUrl { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
	public class Product : EntityBase
	{
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public string ProductFeatures { get; set; }
		public float ProductPrice { get; set; }
		public string ProductPicture { get; set; }
        public float ProductDiscount { get; set; }
		public float PrdouctRating { get; set; } = 0;
        public ICollection<BasketProduct> BasketProducts { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}

using Microsoft.AspNetCore.Http;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Shop
{
	public class QuickViewViewModel
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public string? ProductDescription { get; set; }
		public float ProductPrice { get; set; }
		public string? PictureUrl { get; set; }

		public List<Comment>Comments { get; set; } = new List<Comment>();
	}
}

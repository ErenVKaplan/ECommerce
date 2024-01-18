using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Shop
{
	public class OrderViewModel
	{
		public Guid UserId { get; set; }
		public string DeliverAddress { get; set; }
		public float TotalPrice { get; set; }
		public ShippingMethod ShippingMethod { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
	}
}

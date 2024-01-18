using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
	public class Order : EntityBase
	{
       
		public Guid UserId { get; set; }
		public Guid BasketId { get; set; }
        public string DeliverAddress { get; set; }
        public float TotalPrice { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public ShippingMethod ShippingMethod { get; set; }
		
		public AppUser AppUser { get; set; }

		public Basket Basket { get; set; }
	}
}

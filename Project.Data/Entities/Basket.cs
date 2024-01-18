using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
	public class Basket:EntityBase
	{
      
       
		public Guid OrderId { get; set; }
        
		public ICollection<BasketProduct> BasketProducts { get; set; } = new HashSet<BasketProduct>();
		public Order Order { get; set; }
		
	}
}

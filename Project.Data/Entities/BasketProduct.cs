using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
	public class BasketProduct :EntityBase
	{//Cartitem
        
		public Guid ProductId { get; set; }
		public Guid BasketId { get; set; }
        public  int  Quantity { get; set; }
        

		public Basket Basket { get; set; }
		public Product Product { get; set; }

    }
}

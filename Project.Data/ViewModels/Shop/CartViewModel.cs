using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Shop
{
    public class CartViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
       
        public float ProductPrice { get; set; }
        public string? PictureUrl { get; set; }

        
    }
}

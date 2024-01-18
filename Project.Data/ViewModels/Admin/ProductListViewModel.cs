using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Admin
{
    public class ProductListViewModel
    {   
        public Guid id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductFeatures { get; set; }
        public float ProductPrice { get; set; }
        public float ProductDiscount { get; set; }
        public string? ProductPicUrl { get; set; }   

       
    }
}

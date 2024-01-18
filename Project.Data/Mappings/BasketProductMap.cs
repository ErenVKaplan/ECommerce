using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Mappings
{
	public class BasketProductMap : IEntityTypeConfiguration<BasketProduct>
	{
		

		public void Configure(EntityTypeBuilder<BasketProduct> builder)
		{
			builder.HasKey(b => b.Id);
			//BasketProduct-BireCok Basket ile
			builder.HasOne(b=>b.Basket).WithMany(ba=>ba.BasketProducts).HasForeignKey(b => b.BasketId).OnDelete(DeleteBehavior.NoAction);
			//BasketProduct BireCok Product ile
			builder.HasOne(b => b.Product).WithMany(ba => ba.BasketProducts).HasForeignKey(b => b.ProductId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}

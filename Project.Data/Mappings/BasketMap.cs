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
	public class BasketMap : IEntityTypeConfiguration<Basket>
	{
		public void Configure(EntityTypeBuilder<Basket> builder)
		{
			builder.HasKey(x => x.Id);

			

			builder.HasOne(b => b.Order).WithOne(x => x.Basket).HasForeignKey<Basket>(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

		}
	}
}

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
	public class OrderMap : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(o=>o.AppUser).WithMany(a=>a.Orders).HasForeignKey(o=>o.UserId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}

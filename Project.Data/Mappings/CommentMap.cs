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
	public class CommentMap : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(c => c.Id);
			//Comment bire cok  product ile foreign keyi commentid
			builder.HasOne(c=>c.Product).WithMany(p=>p.Comments).HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.NoAction);
			//Comment  bire cok appuser ile foreign keyi commentId
			builder.HasOne(c=>c.AppUser).WithMany(a=>a.Comments).HasForeignKey(a=>a.UserId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}

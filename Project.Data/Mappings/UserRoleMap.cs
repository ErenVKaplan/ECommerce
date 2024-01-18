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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
                new AppUserRole
                {
                    UserId = Guid.Parse("19B1856D-44A9-401B-B188-563B8B48EE21"),
                    RoleId = Guid.Parse("30144A78-FA5F-45D8-9DBC-EB595667ADD7")
                },
                new AppUserRole
                {
                    UserId = Guid.Parse("361936EA-8C5D-410E-8A5A-7F3F5C302D87"),
                    RoleId = Guid.Parse("70D5392A-4F59-41C5-9DD0-1E026005F59C")
                }
                );
        }
    }
}

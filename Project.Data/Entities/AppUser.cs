using Microsoft.AspNetCore.Identity;
using Project.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

		public string? Address { get; set; }
		public DateTime? BirthDate { get; set; }
		public Gender? Gender { get; set; }
		
		public ICollection<Order> Orders { get; set; }
		public ICollection<Comment> Comments { get; set; }
		
		
	}
}

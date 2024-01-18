using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Users
{
	public class UserProfileViewModel
	{
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        public Gender? Gender { get; set; }
    }
}

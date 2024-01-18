using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
	public class Comment : EntityBase
	{
        
		
		public Guid ProductId { get; set; }

        public Guid  UserId { get; set; }

        public string CommentText { get; set; }

		public float Rating { get; set; }

		public int LikeCount { get; set; }

		public int DislikeCount { get; set; }
        public Product Product { get; set; }
		public virtual AppUser AppUser { get; set; }
	}
}

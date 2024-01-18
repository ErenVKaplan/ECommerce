using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.ViewModels.Shop
{
	public class CommentViewModel
	{
		
		
			public Guid ProductId { get; set; }
		
			public string CommentText { get; set; }
			public float Rating { get; set; }

        public int LikeCount { get; set; }

    }
}

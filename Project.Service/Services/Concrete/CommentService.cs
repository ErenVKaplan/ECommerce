using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Data.Context;
using Project.Data.Entities;
using Project.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Concrete
{
	
	public class CommentService
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public void AddComment(Comment entity)
		{
			_context.Comments.Add(entity);
          
            _context.SaveChanges();
		}

		public  void UpdateProductRating(Guid id,float rating)
		{
			 var productToRating=_context.Products.Include(c=>c.Comments).FirstOrDefault(x=>x.Id==id);
			float newRating = productToRating.PrdouctRating * productToRating.Comments.Count() + rating;
			newRating=newRating / (productToRating.Comments.Count() + 1);
			productToRating.PrdouctRating= newRating;
			_context.SaveChanges();
		}
		public void UpdateCommentLike(Guid CommentId)
		{
		 var Comment=_context.Comments.FirstOrDefault(x => x.Id==CommentId);

			Comment.LikeCount++;
			_context.SaveChanges();
		}
        public void UpdateCommentDislike(Guid CommentId)
        {
            var Comment = _context.Comments.FirstOrDefault(x => x.Id == CommentId);

            Comment.DislikeCount++;
            _context.SaveChanges();
        }
        public IEnumerable<Comment> GetCommentsByProductId(Guid productId)
		{
			return _context.Comments.Where(c => c.ProductId == productId).ToList();
		}
	}
}

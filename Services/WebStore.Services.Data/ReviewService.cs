using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;

namespace WebStore.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;

        public ReviewService(IDeletableEntityRepository<Review> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task CreateAsync(string text, string userId, int productId)
        {
            var review = new Review()
            {
                Text = text,
                UserId = userId,
                ProductiD = productId,
            };

            await this.reviewRepository.AddAsync(review);
            await this.reviewRepository.SaveChangesAsync();
        }

        public async Task Delete(int reviewId)
        {
            var review = this.reviewRepository.All().Where(x => x.Id == reviewId).FirstOrDefault();

            this.reviewRepository.Delete(review);
            await this.reviewRepository.SaveChangesAsync();
        }

        public bool IsExist(int id)
        {
            return this.reviewRepository.All().Any(x => x.Id == id);
        }

        void IReviewService.Delete(int reviewId)
        {
            throw new NotImplementedException();
        }
    }
}

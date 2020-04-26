using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

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
            if (review == null)
            {
                return;
            }

            this.reviewRepository.Delete(review);
            await this.reviewRepository.SaveChangesAsync();
        }

        public string GetReviewAuthorIdById(int reviewId)
        {
            var review = this.reviewRepository.All().Where(x => x.Id == reviewId);
            if (!review.Any())
            {
                return null;
            }

            return review.Select(x => x.UserId).FirstOrDefault();
        }

        public T GetReviewById<T>(int reviewId)
        {
            var review = this.reviewRepository
                     .All()
                     .Where(x => x.Id == reviewId)
                     .To<T>()
                     .FirstOrDefault();

            return review;
        }

        public bool IsExist(int id)
        {
            return this.reviewRepository.All().Any(x => x.Id == id);
        }

        public async Task Update(int reviewId, string text)
        {
            var review = this.reviewRepository.All().Where(x => x.Id == reviewId).FirstOrDefault();

            if (review == null)
            {
                return;
            }

            review.Text = text;
            this.reviewRepository.Update(review);
            await this.reviewRepository.SaveChangesAsync();
        }

    }
}

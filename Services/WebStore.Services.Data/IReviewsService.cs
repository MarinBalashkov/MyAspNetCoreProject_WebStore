namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task CreateAsync(string text, string userId, int productId);

        Task Delete(int reviewId);

        bool IsExist(int id);

        Task Update(int reviewId, string text);

        T GetReviewById<T>(int reviewId);

        string GetReviewAuthorIdById(int reviewId);
    }
}

namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task CreateAsync(string text, string userId, int productId);

        void Delete(int reviewId);

        bool IsExist(int id);

    }
}

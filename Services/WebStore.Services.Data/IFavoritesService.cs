using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Services.Data
{
    public interface IFavoritesService
    {
        IEnumerable<T> GetAllByUserId<T>(string userId, int? take = null, int skip = 0);

        int GetCountGyUserId(string userId);

        Task AddAsync(string userId, int productId);

        bool IsInMyFavorites(string userId, int productId);

    }
}

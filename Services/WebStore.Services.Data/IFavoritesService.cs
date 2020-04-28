using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Services.Data
{
    public interface IFavoritesService
    {
        IEnumerable<T> GetAll<T>(string userId);

        Task AddAsync(string userId, int productId);

    }
}

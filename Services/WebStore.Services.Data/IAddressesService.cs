using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Services.Data
{
    public interface IAddressesService
    {
        Task<int> CreateAsync(string userId, string district, string city, string street);
    }
}

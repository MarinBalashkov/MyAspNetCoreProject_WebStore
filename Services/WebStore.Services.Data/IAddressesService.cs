namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressesService
    {
        Task<int> CreateAsync(string userId, string district, string city, string street);

        IEnumerable<T> GetMyAddresses<T>(string userId);

        T GetById<T>(int? addressId, string userId);
    }
}

namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class AddressesService : IAddressesService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressesService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<int> CreateAsync(string userId, string district, string city, string street)
        {
            var address = new Address()
            {
                UserId = userId,
                District = district,
                City = city,
                Street = street,
            };

            await this.addressRepository.AddAsync(address);
            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }

        public T GetById<T>(int? addressId, string userId)
        {
            var query = this.addressRepository
                .All()
                .Where(x => x.UserId.Equals(userId) && x.Id == addressId);

            return query.To<T>().FirstOrDefault();

        }

        public IEnumerable<T> GetMyAddresses<T>(string userId)
        {
            var query = this.addressRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn);

            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToArray();
        }

        public async Task<int> UpdateAddressAsync(string userId, int? addressId, string district, string city, string street)
        {
            var address = this.addressRepository.All()
                 .Where(x => x.Id == addressId && x.UserId.Equals(userId))
                 .FirstOrDefault();

            if (address == null)
            {
                return 0;
            }

            address.District = district;
            address.City = city;
            address.Street = street;

            this.addressRepository.Update(address);
            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }
    }
}

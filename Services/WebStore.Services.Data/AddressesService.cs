using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;

namespace WebStore.Services.Data
{
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
    }
}

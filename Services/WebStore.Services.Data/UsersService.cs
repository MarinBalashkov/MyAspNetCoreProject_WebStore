using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public T GetUser<T>(string userId)
        {
            var user = this.userRepository
                .All()
                .Where(x => x.Id == userId)
                .To<T>()
                .FirstOrDefault();

            return user;
        }
    }
}

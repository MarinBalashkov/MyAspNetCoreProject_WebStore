namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;

    public class RequestToUsService : IRequestToUsService
    {
        private readonly IDeletableEntityRepository<RequestToUs> requestsToUsRepository;

        public RequestToUsService(IDeletableEntityRepository<RequestToUs> requestsToUsRepository)
        {
            this.requestsToUsRepository = requestsToUsRepository;
        }

        public async Task AddAsync(string name, string email, string title, string content)
        {
            var requestToUs = new RequestToUs()
            {
                Name = name,
                Email = email,
                Title = title,
                Content = content,
            };

            await this.requestsToUsRepository.AddAsync(requestToUs);
            await this.requestsToUsRepository.SaveChangesAsync();
        }
    }
}

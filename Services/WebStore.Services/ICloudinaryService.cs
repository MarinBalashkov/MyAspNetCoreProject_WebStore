namespace WebStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> profilePictures);

    }
}

namespace WebStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> productPictures)
        {
            var urlList = new List<string>();

            foreach (var file in productPictures)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, destinationStream),
                    };

                    var result = await this.cloudinary.UploadAsync(uploadParams);

                    urlList.Add(result.Uri.AbsoluteUri);
                }

            }

            return urlList;
        }
    }
}

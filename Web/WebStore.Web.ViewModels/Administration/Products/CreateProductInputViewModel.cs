using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Web.ViewModels.Categoties;

namespace WebStore.Web.ViewModels.Administration.Products
{

    public class CreateProductInputViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]

        public string Color { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public int MainCategoryId { get; set; }

        public IEnumerable<CategoryViewModel> SubCategories { get; set; }

        public IEnumerable<IFormFile> SecondaryPicturs{ get; set; }

        public IFormFile MainPicture { get; set; }

        public int? ManufacturerId { get; set; }

        public CreateProductItemsInputModel ProductItemsInputModel { get; set; }
    }
}

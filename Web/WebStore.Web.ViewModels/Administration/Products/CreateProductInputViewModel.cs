using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Web.ViewModels.Categoties;

namespace WebStore.Web.ViewModels.Administration.Products
{

    public class CreateProductInputViewModel
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Color { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
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

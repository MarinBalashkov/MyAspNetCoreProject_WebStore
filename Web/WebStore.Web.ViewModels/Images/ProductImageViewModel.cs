namespace WebStore.Web.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ProductImageViewModel : IMapFrom<Image>
    {
        public string ImageUrl { get; set; }
    }
}

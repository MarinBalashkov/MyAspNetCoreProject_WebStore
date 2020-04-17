namespace WebStore.Web.ViewModels.Categoties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentCartegoryId { get; set; }
    }
}

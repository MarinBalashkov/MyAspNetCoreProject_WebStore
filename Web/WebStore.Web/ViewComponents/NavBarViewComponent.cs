using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Categoties;
using WebStore.Web.ViewModels.ViewComponents;

namespace WebStore.Web.ViewComponents
{
    [ViewComponent(Name = "NavBar")]
    public class NavBarViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public NavBarViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var navBarItems = new List<NavBarItemViewModel>();
            var parentCategories = this.categoryService.GetAllParentCategories<CategoryViewModel>();

            if (parentCategories != null)
            {
                foreach (var parenCategory in parentCategories)
                {
                    var subCategories = this.categoryService.GetAllSubCategoriesByParentId<CategoryViewModel>(parenCategory.Id);

                    var navBarItem = new NavBarItemViewModel();
                    navBarItem.ParentCategory = parenCategory;
                    if (subCategories != null)
                    {
                        navBarItem.SubCategories = subCategories;
                    }
                    else
                    {
                        navBarItem.SubCategories = new List<CategoryViewModel>();
                    }

                    navBarItems.Add(navBarItem);
                }
            }

            var viewModel = new NavBarViewModel()
            {
                NavBarItems = navBarItems,
            };

            return this.View(viewModel);
        }
    }
}

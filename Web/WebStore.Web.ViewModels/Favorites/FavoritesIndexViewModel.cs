namespace WebStore.Web.ViewModels.Favorites
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FavoritesIndexViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<FavoriteProductViewModel> FavoriteProducts { get; set; }
    }
}

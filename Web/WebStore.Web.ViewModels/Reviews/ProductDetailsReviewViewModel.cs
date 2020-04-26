using Ganss.XSS;
using System;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Products
{
    public class ProductDetailsReviewViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public int ProductiD { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Text { get; set; }

        public string SanitizedText => new HtmlSanitizer().Sanitize(this.Text);

        public string UserUserName { get; set; }

    }
}
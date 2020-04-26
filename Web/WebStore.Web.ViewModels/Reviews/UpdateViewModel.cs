using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Reviews
{
    public class UpdateViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Text { get; set; }
    }
}

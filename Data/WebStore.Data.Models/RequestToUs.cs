using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class RequestToUs : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}

namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.Reviews;

    public class ReviewController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            //if (parentId.HasValue)
            //{
            //    if (!this.commentsService.IsInPostId(parentId.Value, input.PostId))
            //    {
            //        return this.BadRequest();
            //    }
            //}

            var userId = this.userManager.GetUserId(this.User);
            return this.RedirectToAction("Details", "Products", new { id = input.ProductId});
        }
    }
}

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
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Reviews;

    public class ReviewController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReviewService reviewService;
        private readonly IProductService productService;

        public ReviewController(UserManager<ApplicationUser> userManager, IReviewService reviewService, IProductService productService)
        {
            this.userManager = userManager;
            this.reviewService = reviewService;
            this.productService = productService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            if (this.productService.IsExist(input.ProductId) == false)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "Products", new { id = input.ProductId});
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.reviewService.CreateAsync(input.Text, userId, input.ProductId);
            return this.RedirectToAction("Details", "Products", new { id = input.ProductId});
        }
    }
}

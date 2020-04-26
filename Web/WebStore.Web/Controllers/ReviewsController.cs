namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Products;
    using WebStore.Web.ViewModels.Reviews;

    public class ReviewsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReviewService reviewService;
        private readonly IProductService productService;

        public ReviewsController(UserManager<ApplicationUser> userManager, IReviewService reviewService, IProductService productService)
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int reviewId)
        {
            if (this.reviewService.IsExist(reviewId) == false)
            {
                return this.NotFound();
            }

            var authorId = this.reviewService.GetReviewAuthorIdById(reviewId);
            var userId = this.userManager.GetUserId(this.User);
            if (userId != authorId && this.User.IsInRole(GlobalConstants.AdministratorRoleName) == false)
            {
                return this.Unauthorized();
            }

            var model = this.reviewService.GetReviewById<ProductDetailsReviewViewModel>(reviewId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int reviewId, int productId)
        {
            if (this.reviewService.IsExist(reviewId) == false)
            {
                return this.NotFound();
            }

            var authorId = this.reviewService.GetReviewAuthorIdById(reviewId);
            var userId = this.userManager.GetUserId(this.User);
            if (userId != authorId && this.User.IsInRole(GlobalConstants.AdministratorRoleName) == false)
            {
                return this.Unauthorized();
            }

            await this.reviewService.Delete(reviewId);
            return this.RedirectToAction("Details", "Products", new { id = productId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int reviewId, int productId, string text)
        {
            if (this.reviewService.IsExist(reviewId) == false)
            {
                return this.NotFound();
            }

            var authorId = this.reviewService.GetReviewAuthorIdById(reviewId);

            var userId = this.userManager.GetUserId(this.User);
            if (userId != authorId || this.User.IsInRole(GlobalConstants.AdministratorRoleName) == false)
            {
                return this.Unauthorized();
            }

            await this.reviewService.Update(reviewId, text);
            return this.RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}

﻿namespace WebStore.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Services.Data;
    using WebStore.Services.Messaging;
    using WebStore.Web.ViewModels;
    using WebStore.Web.ViewModels.Home;
    using WebStore.Web.ViewModels.Products;

    public class HomeController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IEmailSender emailSender;
        private readonly IRequestToUsService requestToUsService;

        public HomeController(IProductService productService, ICategoryService categoryService, IEmailSender emailSender, IRequestToUsService requestToUsService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.emailSender = emailSender;
            this.requestToUsService = requestToUsService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                LatestProducts = this.productService.GetLatestProducts<HomeIndexProductViewModel>(20),
                MostLikedProducts = this.productService.GetMostLikedProducts<HomeIndexProductViewModel>(16),
                SubCategoriesNames = this.categoryService.GetAllSubCategoriesNames(),
            };

            return this.View(model);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.requestToUsService.AddAsync(model.Name, model.Email, model.Title, model.Content);

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SendGridCompanyEmail,
                model.Title,
                model.Content,
                model.Email,
                model.Name);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

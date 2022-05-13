using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lombard.Controllers
{

    public class ProductController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<ProductController> _logger;
        readonly IServiceFactory _serviceFactory;

        public ProductController(IServiceFactory serviceFactory, ILogger<ProductController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        IProductService ProductService { get; set; }

        #endregion
        public async Task<IActionResult> Index()
        {
            ProductService = _serviceFactory.ProductService;
            var productList = await ProductService.GetALL();
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                ProductService = _serviceFactory.ProductService;
                newProduct.CreateAt = DateTime.Now;
                newProduct.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await ProductService.AddNew(newProduct);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(newProduct);
        }


        public async Task<IActionResult> Delete(long Id)
        {
            ProductService = _serviceFactory.ProductService;
            var product = await ProductService.GetById(Id);
            await ProductService.Remove(product);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }


        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

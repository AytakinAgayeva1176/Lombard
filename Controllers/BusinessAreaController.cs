using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class BusinessAreaController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<BusinessAreaController> _logger;
        readonly IServiceFactory _serviceFactory;

        public BusinessAreaController(IServiceFactory serviceFactory, ILogger<BusinessAreaController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        IBusinessAreaService BusinessAreaService { get; set; }

        #endregion
        public async Task<IActionResult> Index()
        {
            BusinessAreaService = _serviceFactory.BusinessAreaService;
            var BusinessAreaList = await BusinessAreaService.GetALL();
            return View(BusinessAreaList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(BusinessArea newBusinessArea)
        {
            if (ModelState.IsValid)
            {
                BusinessAreaService = _serviceFactory.BusinessAreaService;
                newBusinessArea.CreateAt = DateTime.Now;
                newBusinessArea.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await BusinessAreaService.AddNew(newBusinessArea);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(newBusinessArea);
           // return PartialView("_BusinessAreaCreate", newBusinessArea);
        }


        public async Task<IActionResult> Delete(long Id)
        {
            BusinessAreaService = _serviceFactory.BusinessAreaService;
            var BusinessArea = await BusinessAreaService.GetById(Id);
            await BusinessAreaService.Remove(BusinessArea);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }


        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

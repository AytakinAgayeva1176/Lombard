using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class BankController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<BankController> _logger;
        readonly IServiceFactory _serviceFactory;

        public BankController(IServiceFactory serviceFactory, ILogger<BankController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        IBankService BankService { get; set; }

        #endregion
        public async Task<IActionResult> Index()
        {
            BankService = _serviceFactory.BankService;
            var bankList= await BankService.GetALL();
            return View(bankList);
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Create(Bank newBank)
        {
            if (ModelState.IsValid)
            {
                BankService = _serviceFactory.BankService;
                newBank.CreateAt = DateTime.Now;
                newBank.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await BankService.AddNew(newBank);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            //return View();
            return PartialView("_BankCreate", newBank);
        }

        public async Task<IActionResult> Details(long Id)
        {
            BankService = _serviceFactory.BankService;
            var bank = await BankService.GetById(Id);
            return View(bank);
        }


        public async Task<IActionResult> Delete(long Id)
        {
            BankService = _serviceFactory.BankService;
            var bank = await BankService.GetById(Id);
            await BankService.Remove(bank);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }


        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

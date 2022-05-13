using Microsoft.AspNetCore.Mvc;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Lombard.Domain.ViewModels;

namespace Lombard.Controllers
{
    public class GoldSettingsController : Controller,IDisposable
    {
        #region This Ctor
        private readonly ILogger<GoldSettingsController> _logger;
        readonly IServiceFactory _serviceFactory;

        public GoldSettingsController(IServiceFactory serviceFactory, ILogger<GoldSettingsController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces

        IHallmarkService HallmarkService { get; set; }
        IGoldTypeService GoldTypeService { get; set; }

        #endregion
        public IActionResult Index()
        {
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            var goldTypes = GoldTypeService.GetALL().Result;
            var hallmark = HallmarkService.GetALL().Result;
            GoldSettingVm goldSetting = new GoldSettingVm()
            {
                GoldTypes = goldTypes,
                Hallmarks = hallmark
            };
            return View(goldSetting);
        }

        public IActionResult CreateGoldType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoldType(GoldType goldType)
        {
            if (ModelState.IsValid)
            {
                GoldTypeService = _serviceFactory.GoldTypeService;
                goldType.CreateAt = DateTime.Now;
                goldType.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await GoldTypeService.AddNew(goldType);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(goldType);
        }


        public async Task<IActionResult> DeleteGoldType(long Id)
        {
            GoldTypeService = _serviceFactory.GoldTypeService;
            var goldType = await GoldTypeService.GetById(Id);
            await GoldTypeService.Remove(goldType);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }


        public IActionResult CreateHallmark()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHallmark(Hallmark hallmark)
        {
            if (ModelState.IsValid)
            {
                HallmarkService = _serviceFactory.HallmarkService;
                hallmark.CreateAt = DateTime.Now;
                hallmark.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await HallmarkService.AddNew(hallmark);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(hallmark);
        }
        public async Task<IActionResult> EditHallmark(long Id)
        {
            HallmarkService = _serviceFactory.HallmarkService;
            var hallmark = await HallmarkService.GetById(Id);
            return View(hallmark);
        }

        [HttpPost]
        public async Task<IActionResult> EditHallmark(Hallmark hallmark)
        {
            if (ModelState.IsValid)
            {
                HallmarkService = _serviceFactory.HallmarkService;
                await HallmarkService.Update(hallmark);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(hallmark);
        }


        public async Task<IActionResult> Details(long Id)
        {
            HallmarkService = _serviceFactory.HallmarkService;
            var bank = await HallmarkService.GetById(Id);
            return View(bank);
        }

        public async Task<IActionResult> DeleteHallmark(long Id)
        {
            HallmarkService = _serviceFactory.HallmarkService;
            var hallmark = await HallmarkService.GetById(Id);
            await HallmarkService.Remove(hallmark);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }
        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}




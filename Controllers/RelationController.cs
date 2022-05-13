


using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class RelationController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<RelationController> _logger;
        readonly IServiceFactory _serviceFactory;

        public RelationController(IServiceFactory serviceFactory, ILogger<RelationController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        IRelationTypeService RelationTypeService { get; set; }

        #endregion
        public async Task<IActionResult> Index()
        {
            RelationTypeService = _serviceFactory.RelationTypeService;
            var RelationList = await RelationTypeService.GetALL();
            return View(RelationList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RelationType newRelation)
        {
            if (ModelState.IsValid)
            {
                RelationTypeService = _serviceFactory.RelationTypeService;
                newRelation.CreateAt = DateTime.Now;
                newRelation.RecordStatus = Domain.Enums.RecordStatus.IsActive;
                await RelationTypeService.AddNew(newRelation);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Index");
            }
            //return View();
            return PartialView("_RelationCreate", newRelation);
        }


        public async Task<IActionResult> Delete(long Id)
        {
            RelationTypeService = _serviceFactory.RelationTypeService;
            var Relation = await RelationTypeService.GetById(Id);
            await RelationTypeService.Remove(Relation);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }


        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}
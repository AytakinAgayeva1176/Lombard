using Lombard.Domain.Contracts.Services;
using Lombard.Models;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.Controllers
{

    public class HomeController : Controller
    {
        #region This Ctor
        private readonly ILogger<HomeController> _logger;
        readonly IServiceFactory _serviceFactory;

        public HomeController(IServiceFactory serviceFactory, ILogger<HomeController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces

       
        ILegalDebtorService LegalDebtorService { get; set; }
        IIndividualDebtorService IndividualDebtorService { get; set; }
        #endregion
        public IActionResult Index()
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;

            ViewData["LegalDebtorCount"] = LegalDebtorService.GetALL().Result.Count();
            ViewData["IndividualDebtorCount"] = IndividualDebtorService.GetALL().Result.Count();
            return View();
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Ganss.Excel;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Lombard.Domain.ViewModels;
using Lombard.Helpers;
using Lombard.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class PledgeContractController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<PledgeContractController> _logger;
        readonly IServiceFactory _serviceFactory;
        private readonly IWebHostEnvironment webHost;

        public PledgeContractController(IServiceFactory serviceFactory, ILogger<PledgeContractController> logger, IWebHostEnvironment webHost)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
            this.webHost = webHost;
        }
        #endregion

        #region Interfaces
        IPledgeContractService PledgeContractService { get; set; }
        IPledgeService PledgeService { get; set; }
        IHallmarkService HallmarkService { get; set; }
        ILegalDebtorService LegalDebtorService { get; set; }
        IIndividualDebtorService IndividualDebtorService { get; set; }
        ICorporativeContractService CorporativeContractService { get; set; }
        #endregion

        #region PledgeContract

        public IActionResult Create(long? CorporativeContractId)
        {
            #region ViewData

            HallmarkService = _serviceFactory.HallmarkService;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");



            if (CorporativeContractId != null)
            {
                ViewData["CorporativeContractId"] = CorporativeContractId;
            }

            CorporativeContractService = _serviceFactory.CorporativeContractService;
            var corporativeContract = CorporativeContractService.GetCorporativeContractWithAllInfo(CorporativeContractId.Value).Result;
            var count = corporativeContract.AddedPledgesContract.Count + 1;

            ViewData["ContractNumber"] = count;

            #endregion

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form, long? CorporativeContractId)
        {
            PledgeContractService = _serviceFactory.PledgeContractService;

            List<Pledge> pledges = new List<Pledge>();


            if (form["pledgeCount"] != "")
            {
                for (int i = 0; i <= int.Parse(form["pledgeCount"]); i++)
                {
                    double result;


                    Pledge pledge = new Pledge()
                    {
                        ClientFullName = form["ClientFullName_" + i],
                        PackageNumber = form["PackageNumber_" + i],
                        AdditionalPledgeContractNumber = form["ContractNumber"],
                        NetWeight_375 = double.TryParse(form["NetWeight_375_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight_500 = double.TryParse(form["NetWeight_500_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight583_585 = double.TryParse(form["NetWeight583_585_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight_750 = double.TryParse(form["NetWeight_750_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight_875 = double.TryParse(form["NetWeight_875_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight_916 = double.TryParse(form["NetWeight_916_" + i].ToString(), out result) ? (double?)result : null,
                        NetWeight_999 = double.TryParse(form["NetWeight_999_" + i].ToString(), out result) ? (double?)result : null,

                        TotalWeight_375 = double.TryParse(form["TotalWeight_375_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight_500 = double.TryParse(form["TotalWeight_500_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight583_585 = double.TryParse(form["TotalWeight583_585_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight_750 = double.TryParse(form["TotalWeight_750_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight_875 = double.TryParse(form["TotalWeight_875_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight_916 = double.TryParse(form["TotalWeight_916_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight_999 = double.TryParse(form["TotalWeight_999_" + i].ToString(), out result) ? (double?)result : null,

                        NetWeight = double.TryParse(form["NetWeight_" + i].ToString(), out result) ? (double?)result : null,
                        TotalWeight = double.TryParse(form["TotalWeight_" + i].ToString(), out result) ? (double?)result : null,

                        StorePrice = double.TryParse(form["StorePrice_" + i].ToString(), out result) ? (double?)result : null,
                        LikvidPrice = double.TryParse(form["LikvidPrice_" + i].ToString(), out result) ? (double?)result : null,

                        CreateAt = DateTime.Now,
                        RecordStatus = RecordStatus.IsActive,

                    };

                    if (pledge.ClientFullName != null)
                    {
                        pledges.Add(pledge);
                    }

                }
            }



            PledgeContract contract = new PledgeContract()
            {
                PledgeList = pledges,
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                Date = DateTime.Parse(form["Date"]),
                TotalGoldLikvidPrice = Convert.ToDouble(form["TotalGoldLikvidPrice"]),
                TotalGoldMarketPrice = Convert.ToDouble(form["TotalGoldMarketPrice"]),
                TotalGoldNetWeight = Convert.ToDouble(form["TotalGoldNetWeight"]),
                TotalGoldWeight = Convert.ToDouble(form["TotalGoldWeight"]),
                ContractNumber = form["ContractNumber"],
                CorporativeContractId = CorporativeContractId.Value,
                ActType = ActTypes.Addition
            };


            if (CorporativeContractId != null)
            {
                contract.CorporativeContractId = CorporativeContractId.Value;
            }

            await PledgeContractService.AddNew(contract);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Details", "CorporativeContract", new { id = contract.CorporativeContractId });
        }


        public async Task<IActionResult> PledgeExtraction(long CorporativeContractId)
        {
            ViewData["CorporativeContractId"] = CorporativeContractId;
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            var corporativeContract =await CorporativeContractService.GetCorporativeContractWithAllInfo(CorporativeContractId);

            PledgeContractVM contractVM = new PledgeContractVM();

            foreach (var item in corporativeContract.AddedPledgesContract)
            {
                foreach (var pledge in item.PledgeList)
                {
                    if (pledge.ExtractionContractId == 0)
                    {
                        contractVM.PledgeList.Add(pledge);
                    }

                }
            }

            var count = corporativeContract.ExtractedPledgesContract.Count + 1;
            ViewData["ContractNumber"] = count;
            contractVM.ContractNumber = count.ToString();
            return View(contractVM);
        }


        [HttpPost]
        public async Task<IActionResult> PledgeExtraction(PledgeContractVM contractVM)
        {
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            PledgeContractService = _serviceFactory.PledgeContractService;
            PledgeService = _serviceFactory.PledgeService;

            #region Create PledgeContract
            PledgeContract contract = new PledgeContract()
            {
                TotalGoldLikvidPrice = contractVM.TotalGoldLikvidPrice,
                TotalGoldMarketPrice = contractVM.TotalGoldMarketPrice,
                TotalGoldNetWeight = contractVM.TotalGoldNetWeight,
                TotalGoldWeight = contractVM.TotalGoldWeight,
                ContractNumber = contractVM.ContractNumber,
                Date = contractVM.Date,
                CorporativeContractId = contractVM.CorporativeContractId,
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                ActType = ActTypes.Extractions
            };

            var extractionContract = await PledgeContractService.AddNew(contract);
            await _serviceFactory.SaveAsync();
            #endregion

            List<Pledge> pledgeList = new List<Pledge>();

            foreach (var pledgeId in contractVM.PledgeIdList)
            {
                var pledge = PledgeService.GetById(pledgeId).Result;
                pledge.ExtractionContractId = extractionContract.Id;
                pledgeList.Add(pledge);
            }

            await PledgeService.UpdateRange(pledgeList);

            var list1 = extractionContract.PledgeList;
            var list11 = pledgeList;

            await _serviceFactory.SaveAsync();

            var list2 = extractionContract.PledgeList;
            var list22 = pledgeList;

            return RedirectToAction("Details", "CorporativeContract", new { id = extractionContract.CorporativeContractId });
        }




        //public async Task<IActionResult> Delete(long Id)
        //{
        //    PledgeContractService = _serviceFactory.PledgeContractService;
        //    var contract = await PledgeContractService.GetById(Id);

        //    await PledgeContractService.Remove(contract);
        //    await _serviceFactory.SaveAsync();

        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> Details(long id)
        {
            PledgeContractService = _serviceFactory.PledgeContractService;
            var contract = await PledgeContractService.GetById(id);

            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }





        [HttpPost]
        public async Task<IActionResult> EditPledgeAsExcel(Pledge pledge, long contractId, long Id)
        {
            if (ModelState.IsValid)
            {
                pledge.PledgeContractId = contractId;
                pledge.Id = Id;
                PledgeService = _serviceFactory.PledgeService;
                await PledgeService.Update(pledge);

                await _serviceFactory.SaveAsync();


            }
            return RedirectToAction("Details", new { id = contractId });
        }





        public async Task<IActionResult> DeletePledge(long Id)
        {
            PledgeService = _serviceFactory.PledgeService;
            var pledge = await PledgeService.GetById(Id);

            await PledgeService.Remove(pledge);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }



        #endregion




        public async Task<JsonResult> GetHallmarkDetails(long? hallmarkId, string typeName)
        {
            HallmarkService = _serviceFactory.HallmarkService;
            Hallmark hallmark = new Hallmark();
            if (hallmarkId != null)
            {
                hallmark = await HallmarkService.GetById(hallmarkId.Value);

            }
            else if (typeName != "")
            {
                var halmarkList = HallmarkService.GetALL().Result;
                foreach (var item in halmarkList)
                {
                    if (item.TypeName == typeName)
                    {
                        hallmark = item;
                    }
                }
            }
            //return hallmark;

            return Json(new
            {
                LikvidPriceOfOneGram = hallmark.LikvidPriceOfOneGram,
                MarketPriceOfOneGram = hallmark.MarketPriceOfOneGram
            });

        }



        public ActionResult AddPledgeToPledgeContract(int hCnt)
        {
            int newCount = hCnt;
            ViewData["newCount"] = newCount;

            return PartialView("_AddPledgeToPledgeContract");
        }




        public IEnumerable<PledgeVMforExcel> ExcelToList(IFormFile excel)
        {

            var newFileName =
               $"{Path.GetFileNameWithoutExtension(excel.FileName)}" +
               $"-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}" +
               $"{Path.GetExtension(excel.FileName)}";

            var path = Path.Combine(webHost.WebRootPath, "ExcelFiles");


            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            var rootPath = Path.Combine(path, newFileName);
            using (var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                excel.CopyTo(fileStream);
            }

            var pledges = new ExcelMapper(rootPath).Fetch<PledgeVMforExcel>();


            return pledges;
        }

        public IActionResult ExcelToListView()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ExcelToListView(IFormFile excel)
        {

            var newFileName =
               $"{Path.GetFileNameWithoutExtension(excel.FileName)}" +
               $"-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}" +
               $"{Path.GetExtension(excel.FileName)}";

            var path = Path.Combine(webHost.WebRootPath, "ExcelFiles");


            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            var rootPath = Path.Combine(path, newFileName);
            using (var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                excel.CopyTo(fileStream);
            }

            var pledges = new ExcelMapper(rootPath).Fetch<PledgeVMforExcel>();

            return View(pledges);
        }

        public IActionResult Cancel(string returnUrl, long? CorporativeContractId)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Details", "CorporativeContract", new { id = CorporativeContractId }); ;
        }


        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

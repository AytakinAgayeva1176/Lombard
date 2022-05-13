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
    public class ActController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<ActController> _logger;
        readonly IServiceFactory _serviceFactory;
        private readonly IWebHostEnvironment webHost;

        public ActController(IServiceFactory serviceFactory, ILogger<ActController> logger, IWebHostEnvironment webHost)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
            this.webHost = webHost;
        }
        #endregion

        #region Interfaces
        IActService ActService { get; set; }
        IGoldTypeService GoldTypeService { get; set; }
        IGoldService GoldService { get; set; }
        IHallmarkService HallmarkService { get; set; }
        ILegalDebtorService LegalDebtorService { get; set; }
        IIndividualDebtorService IndividualDebtorService { get; set; }

        #endregion

        #region Acts
        public async Task<IActionResult> Index()
        {
            ActService = _serviceFactory.ActService;
            var list = await ActService.GetALL();
            return View(list);

        }

        public IActionResult Create(long? parentId, string Debtor, string returnUrl, string actType, string DebtorId)
        {

            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            ActService = _serviceFactory.ActService;
            #endregion

            #region ViewData
            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> physicalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> legalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();
            foreach (var item in IndividualDebtorList)
            {
                physicalDebtors[item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                legalDebtors[item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["PhysicalDebtors"] = new SelectList(physicalDebtors, "Key", "Value");
            ViewData["LegalDebtors"] = new SelectList(legalDebtors, "Key", "Value");
            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");
           // ViewData["ActTypes"] = new SelectList(EnumHelper.GetDescriptions<ActTypes>(), "Key", "Value");


            var IsMain = "selected";
            ViewData["IsSubAct"] = "False";
            if (parentId != null)
            {
                IsMain = "notSelected";

                ActService = _serviceFactory.ActService;
                var parentAct = ActService.GetActWithAllInfo(parentId.Value).Result;
                ViewData["ActName"] = parentAct.Name + "-" + parentAct.Acts.Count;
                ViewData["IsSubAct"] = "SubAct";
            }

            if (Debtor != null)
            {
                ViewData["Debtor"] = Debtor;
            }

            if (actType != null)
            {
                ViewData["actType"] = actType;
                IsMain = "selected";
            }
            if (DebtorId != null)
            {
                ViewData["DebtorId"] = DebtorId;
            }


            ViewData["IsSelected"] = IsMain;
            ViewData["parentId"] = parentId;
            ViewData["returnUrl"] = returnUrl;

            #endregion

            return View();
        }


        public async Task<JsonResult> GetHallmarkDetails(long hallmarkId)
        {
            HallmarkService = _serviceFactory.HallmarkService;
            var hallmark = await HallmarkService.GetById(hallmarkId);

            //return hallmark;

            return Json(new
            {
                LikvidPriceOfOneGram = hallmark.LikvidPriceOfOneGram,
                MarketPriceOfOneGram = hallmark.MarketPriceOfOneGram
            });

        }


        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form, string returnUrl, string actType, string DebtorId)
        {
            ActService = _serviceFactory.ActService;

            List<Gold> golds = new List<Gold>();
            if (form["goldCount"] != "")
            {
                for (int i = 0; i <= int.Parse(form["goldCount"]); i++)
                {
                    Gold gold = new Gold()
                    {
                        GoldTypeId = Convert.ToInt64(form["GoldTypeId_" + i]),
                        HallmarkId = Convert.ToInt64(form["HallmarkId_" + i]),
                        ItemsCount = Convert.ToInt32(form["ItemsCount_" + i]),
                        OneGramLikvidPrice = Convert.ToDouble(form["OneGramLikvidPrice_" + i]),
                        OneGramStorePrice = Convert.ToDouble(form["OneGramStorePrice_" + i]),
                        MarketPrice = Convert.ToDouble(form["MarketPrice_" + i]),
                        LikvidPrice = Convert.ToDouble(form["LikvidPrice_" + i]),
                        TotalWeight = Convert.ToDouble(form["TotalWeight_" + i]),
                        NetWeight = Convert.ToDouble(form["NetWeight_" + i]),
                        JewelWeight = Convert.ToDouble(form["JewelWeight_" + i]),
                        Description = form["Description_" + i],
                        CreateAt = DateTime.Now,
                        RecordStatus = RecordStatus.IsActive,
                    };

                    if (gold.GoldTypeId!=0 && gold.HallmarkId != 0)
                    {
                        golds.Add(gold);
                    }
                  
                }
            }



            Act act = new Act()
            {
                Golds = golds,
                Name = form["Name"],
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                Date = DateTime.Parse(form["Date"]),
                TotalGoldLikvidPrice = Convert.ToDouble(form["TotalGoldLikvidPrice"]),
                TotalGoldMarketPrice = Convert.ToDouble(form["TotalGoldMarketPrice"]),
                TotalGoldNetWeight = Convert.ToDouble(form["TotalGoldNetWeight"]),
                TotalGoldWeight = Convert.ToDouble(form["TotalGoldWeight"]),
                TotalJewelsWeight = Convert.ToDouble(form["TotalJewelsWeight"]),
                TotalGoldCount = Convert.ToDouble(form["TotalGoldCount"])
            };


            if (actType == "Addition")
            {
                act.ActType = ActTypes.Addition;
            }

            else if (actType == "Extraction")
            {
                act.ActType = ActTypes.Extractions;
            }

            else if (form.ContainsKey("ActType"))
            {
                act.ActType = (ActTypes)int.Parse(form["ActType"]);
            }
            #region Save Image


            if (form.Files.Count != 0)
            {
                var image = form.Files[0];
                var newFileName =
                $"{Path.GetFileNameWithoutExtension(image.FileName)}" +
                $"-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}" +
                $"{Path.GetExtension(image.FileName)}";

                var path = Path.Combine(webHost.WebRootPath, "Golds");

                if (act.LegalDebtorId != null)
                {
                    path = Path.Combine(webHost.WebRootPath, "Golds", act.LegalDebtorId.ToString());
                }
                else if (act.IndividualDebtorId != null)
                {
                    path = Path.Combine(webHost.WebRootPath, "Golds", act.IndividualDebtorId.ToString());
                }

                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                var rootPath = Path.Combine(path, newFileName);
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                act.FileName = newFileName;
            }
            #endregion


            if (DebtorId != null)
            {
                if (DebtorId.Length != 0 && DebtorId.Substring(0, 2) == "L+")
                {
                    act.LegalDebtorId = Convert.ToInt64(DebtorId.Substring(2));
                }
                else if (DebtorId.Length != 0 && DebtorId.Substring(0, 2) == "P+")
                {
                    act.IndividualDebtorId = Convert.ToInt64(DebtorId.Substring(2));
                }
            }

            else if (form.ContainsKey("IndividualDebtorId"))
            {
                act.IndividualDebtorId = Convert.ToInt64(form["IndividualDebtorId"]);
            }
            else if (form.ContainsKey("LegalDebtorId"))
            {
                act.LegalDebtorId = Convert.ToInt64(form["LegalDebtorId"]);
            }


            if (form["ParentId"] != "")
            {
                act.ParentId = Convert.ToInt64(form["ParentId"]);
                var parentAct = ActService.GetActWithAllInfo(act.ParentId.Value).Result;
                if (parentAct != null)
                {
                    act.IndividualDebtorId = parentAct.IndividualDebtorId;
                    act.LegalDebtorId = parentAct.LegalDebtorId;
                    act.Name = parentAct.Name + "-" + parentAct.Acts.Count;
                }


            }

            else if (form["ParentId"] == "")
            {
                act.ActType = ActTypes.Main;
                act.ParentId = null;
            }


            await ActService.AddNew(act);
            await _serviceFactory.SaveAsync();

            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            ActService = _serviceFactory.ActService;
            #endregion

            #region ViewData
            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> physicalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> legalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();
           
            foreach (var item in IndividualDebtorList)
            {
                physicalDebtors[item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                legalDebtors[item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["PhysicalDebtors"] = new SelectList(physicalDebtors, "Key", "Value");
            ViewData["LegalDebtors"] = new SelectList(legalDebtors, "Key", "Value");
            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");


            var IsMain = "selected";
            ViewData["IsSubAct"] = "False";
            if (form["ParentId"] != "" )
            {
                IsMain = "notSelected";
               var parentId = Convert.ToInt64(form["ParentId"]);
                ActService = _serviceFactory.ActService;
                var parentAct = ActService.GetActWithAllInfo(parentId).Result;
                ViewData["ActName"] = parentAct.Name + "-" + parentAct.Acts.Count;
                ViewData["IsSubAct"] = "SubAct";
                ViewData["parentId"] = parentId;
            }

            if (form["Debtor"] != "")
            {
                ViewData["Debtor"] = form["Debtor"];
            }

            if (actType != null)
            {
                ViewData["actType"] = actType;
                IsMain = "selected";
            }
            if (DebtorId != null)
            {
                ViewData["DebtorId"] = DebtorId;
            }


            ViewData["IsSelected"] = IsMain;
            ViewData["returnUrl"] = returnUrl;

            #endregion
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index");

        }



        public async Task<IActionResult> Delete(long Id)
        {
            ActService = _serviceFactory.ActService;
            var act = await ActService.GetById(Id);

            await ActService.Remove(act);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(long id)
        {
            ActService = _serviceFactory.ActService;
            var act = await ActService.GetActWithAllInfo(id);

            if (act == null)
            {
                return NotFound();
            }
            ViewData["returnUrl"] = "/Act/Details/" + id;
            #region Services
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            #endregion

            #region ViewData
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();

            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

            #endregion
            return View(act);
        }

        public async Task<IActionResult> Edit(long id, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            ActService = _serviceFactory.ActService;
            var act = await ActService.GetById(id);
            if (act == null)
            {
                return NotFound();
            }
            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            #endregion

            #region ViewData

            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> physicalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> legalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();
            foreach (var item in IndividualDebtorList)
            {
                physicalDebtors[item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                legalDebtors[item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["PhysicalDebtors"] = new SelectList(physicalDebtors, "Key", "Value");
            ViewData["LegalDebtors"] = new SelectList(legalDebtors, "Key", "Value");
            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");
            ViewData["ActTypes"] = new SelectList(EnumHelper.GetDescriptions<ActTypes>(), "Key", "Value");

            #endregion

            return View(act);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Act act, string returnUrl)
        {
            ActService = _serviceFactory.ActService;
            if (ModelState.IsValid)
            {
                #region Save Image

                if (act.Image != null)
                {

                    var newFileName =
                    $"{Path.GetFileNameWithoutExtension(act.Image.FileName)}" +
                    $"-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}" +
                    $"{Path.GetExtension(act.Image.FileName)}";

                    var path = Path.Combine(webHost.WebRootPath, "Golds");

                    if (act.LegalDebtorId != null)
                    {
                        path = Path.Combine(webHost.WebRootPath, "Golds", act.LegalDebtorId.ToString());
                    }
                    else if (act.IndividualDebtorId != null)
                    {
                        path = Path.Combine(webHost.WebRootPath, "Golds", act.IndividualDebtorId.ToString());
                    }

                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }

                    var rootPath = Path.Combine(path, newFileName);

                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        act.Image.CopyTo(fileStream);
                    }

                    act.FileName = newFileName;
                }
                #endregion

                await ActService.Update(act);
                await _serviceFactory.SaveAsync();

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }

            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            #endregion

            #region ViewData

            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> physicalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> legalDebtors = new Dictionary<long, string>();
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();
            foreach (var item in IndividualDebtorList)
            {
                physicalDebtors[item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                legalDebtors[item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["PhysicalDebtors"] = new SelectList(physicalDebtors, "Key", "Value");
            ViewData["LegalDebtors"] = new SelectList(legalDebtors, "Key", "Value");
            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");
            ViewData["ActTypes"] = new SelectList(EnumHelper.GetDescriptions<ActTypes>(), "Key", "Value");

            #endregion

            return View(act);
        }


        public ActionResult DisplayAddGoldToAct(int gCnt)
        {
            int newCount = gCnt;
            ViewData["newCount"] = newCount;

            #region ViewData

            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();

            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

            #endregion
            return PartialView("_AddGoldToAct");
        }
        #endregion

        #region Golds


        public IActionResult AddGold(string returnUrl)
        {
            #region Services
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            #endregion

            #region ViewData
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();

            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

            #endregion
            ViewData["returnUrl"] = returnUrl;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddGold(long Id, GoldVM gold, string returnUrl)
        {
            GoldService = _serviceFactory.GoldService;
            Gold newGold = new Gold()
            {
                ActId = Id,
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                GoldTypeId = gold.GoldTypeId,
                HallmarkId = gold.HallmarkId,
                OneGramStorePrice = gold.OneGramStorePrice,
                OneGramLikvidPrice = gold.OneGramLikvidPrice,
                LikvidPrice = gold.LikvidPrice,
                Description = gold.Description,
                NetWeight = gold.NetWeight,
                JewelWeight = gold.JewelWeight,
                TotalWeight = gold.TotalWeight,
                ItemsCount = gold.ItemsCount,
                MarketPrice = gold.MarketPrice
            };
            await GoldService.AddNew(newGold);

            await _serviceFactory.SaveAsync();
            // return RedirectToAction("Index");

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index");
            //return PartialView("_ActCreate", newAct);
        }


        public async Task<IActionResult> DeleteGold(long Id)
        {
            GoldService = _serviceFactory.GoldService;
            var gold = await GoldService.GetById(Id);
            await GoldService.Remove(gold);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Details", new { id = gold.ActId });
        }

        public async Task<IActionResult> GoldDetails(long Id)
        {
            GoldService = _serviceFactory.GoldService;
            var gold = await GoldService.GetById(Id);

            return View(gold);
        }

        [HttpPost]
        public async Task<IActionResult> EditGoldAsExcel(Gold gold, long actId, long Id)
        {
            ActService = _serviceFactory.ActService;
            if (ModelState.IsValid)
            {
                gold.ActId = actId;
                gold.Id = Id;
                GoldService = _serviceFactory.GoldService;
                await GoldService.Update(gold);
                var act = ActService.GetById(actId).Result;  
                double totalWeight = 0;
                double likvidPrice = 0;
                double storePrice = 0;
                double netWeight = 0;
                double jewelWeight = 0;
                foreach (var item in act.Golds)
                {
                    totalWeight += item.TotalWeight;
                    netWeight += item.NetWeight;
                    jewelWeight += item.JewelWeight;
                    likvidPrice += item.LikvidPrice;
                    storePrice += item.MarketPrice;
                }

                act.TotalGoldLikvidPrice = likvidPrice;
                act.TotalGoldMarketPrice = storePrice;
                act.TotalGoldNetWeight = netWeight;
                act.TotalGoldWeight = totalWeight;
                act.TotalJewelsWeight = jewelWeight;

                await ActService.Update(act);
                await _serviceFactory.SaveAsync();

                return RedirectToAction("Details", new { id = gold.ActId });
            }


            #region Services
            GoldTypeService = _serviceFactory.GoldTypeService;
            HallmarkService = _serviceFactory.HallmarkService;
            #endregion

            #region ViewData
            var goldTypeList = GoldTypeService.GetALL().Result;
            var hallmarkList = HallmarkService.GetALL().Result;
            Dictionary<long, string> goldTypes = new Dictionary<long, string>();
            Dictionary<long, string> hallmarks = new Dictionary<long, string>();

            foreach (var item in goldTypeList)
            {
                goldTypes[item.Id] = item.Name;
            }
            foreach (var item in hallmarkList)
            {
                hallmarks[item.Id] = item.TypeName;
            }

            ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
            ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

            #endregion

            return RedirectToAction("Details", new { id = gold.ActId });
        }




        //public async Task<IActionResult> EditGold(long Id)
        //{

        //    #region Services
        //    GoldTypeService = _serviceFactory.GoldTypeService;
        //    HallmarkService = _serviceFactory.HallmarkService;
        //    #endregion

        //    #region ViewData
        //    var goldTypeList = GoldTypeService.GetALL().Result;
        //    var hallmarkList = HallmarkService.GetALL().Result;
        //    Dictionary<long, string> goldTypes = new Dictionary<long, string>();
        //    Dictionary<long, string> hallmarks = new Dictionary<long, string>();

        //    foreach (var item in goldTypeList)
        //    {
        //        goldTypes[item.Id] = item.Name;
        //    }
        //    foreach (var item in hallmarkList)
        //    {
        //        hallmarks[item.Id] = item.TypeName;
        //    }

        //    ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
        //    ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

        //    #endregion

        //    GoldService = _serviceFactory.GoldService;
        //    var gold = await GoldService.GetById(Id);
        //    return View(gold);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditGold(Gold gold)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        GoldService = _serviceFactory.GoldService;
        //        await GoldService.Update(gold);

        //        await _serviceFactory.SaveAsync();

        //        return RedirectToAction("Details", new { id = gold.ActId });
        //    }


        //    #region Services
        //    GoldTypeService = _serviceFactory.GoldTypeService;
        //    HallmarkService = _serviceFactory.HallmarkService;
        //    #endregion

        //    #region ViewData
        //    var goldTypeList = GoldTypeService.GetALL().Result;
        //    var hallmarkList = HallmarkService.GetALL().Result;
        //    Dictionary<long, string> goldTypes = new Dictionary<long, string>();
        //    Dictionary<long, string> hallmarks = new Dictionary<long, string>();

        //    foreach (var item in goldTypeList)
        //    {
        //        goldTypes[item.Id] = item.Name;
        //    }
        //    foreach (var item in hallmarkList)
        //    {
        //        hallmarks[item.Id] = item.TypeName;
        //    }

        //    ViewData["GoldTypes"] = new SelectList(goldTypes, "Key", "Value");
        //    ViewData["HallMarks"] = new SelectList(hallmarks, "Key", "Value");

        //    #endregion

        //    return View(gold);
        //}



        #endregion



        public IActionResult Cancel(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index");
        }

        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

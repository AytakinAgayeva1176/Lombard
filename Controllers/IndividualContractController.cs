using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Lombard.Domain.ViewModels;
using Lombard.Helpers;
using Lombard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class IndividualContractController : Controller, IDisposable
    {

        #region This Ctor
        private readonly ILogger<IndividualContractController> _logger;
        readonly IServiceFactory _serviceFactory;

        public IndividualContractController(IServiceFactory serviceFactory, ILogger<IndividualContractController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces

        IIndividualContractService IndividualContractService { get; set; }
        IActService ActService { get; set; }
        ILegalDebtorService LegalDebtorService { get; set; }
        IIndividualDebtorService IndividualDebtorService { get; set; }
        IActIndividualContractService ActIndividualContractService { get; set; }
        IGuaranterIndividualContractService GuaranterIndividualContractService { get; set; }
        IProductService ProductService { get; set; }
        #endregion



        public async Task<IActionResult> Index()
        {
            IndividualContractService = _serviceFactory.IndividualContractService;
            var contractList = await IndividualContractService.GetALL();
            return View(contractList);
        }

        public IActionResult Create()
        {
            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            ProductService = _serviceFactory.ProductService;
            #endregion


            #region ViewData
            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var productList = ProductService.GetALL().Result;
            Dictionary<string, string> Debtors = new Dictionary<string, string>();
            Dictionary<long, string> products = new Dictionary<long, string>();

            foreach (var item in IndividualDebtorList)
            {
                Debtors["P" + item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                Debtors["L" + item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in productList)
            {
                products[item.Id] = item.Name;
            }

            ViewData["Debtors"] = new SelectList(Debtors, "Key", "Value");
            ViewData["ProductPurpose"] = new SelectList(EnumHelper.GetDescriptions<ProductPurpose>(), "Key", "Value");
            ViewData["LoanAssignment"] = new SelectList(EnumHelper.GetDescriptions<LoanAssignment>(), "Key", "Value");
            ViewData["Products"] = new SelectList(products, "Key", "Value");
            #endregion

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(IndividualContractVM contractVM)
        {

            //if (ModelState.IsValid)
            //{
            IndividualContractService = _serviceFactory.IndividualContractService;
            ActIndividualContractService = _serviceFactory.ActIndividualContractService;
            GuaranterIndividualContractService = _serviceFactory.GuaranterIndividualContractService;

            #region Create IndividualContract
            IndividualContract contract = new IndividualContract()
            {
                // DebtorI = contractVM.IndividualContract.DebtorFullname,
                AccountNumber = contractVM.IndividualContract.AccountNumber,
                Comission = contractVM.IndividualContract.Comission,
                ContractNo = contractVM.IndividualContract.ContractNo,
                CreditAmount = contractVM.IndividualContract.CreditAmount,
                Currency = contractVM.IndividualContract.Currency,
                DiscountedMonths = contractVM.IndividualContract.DiscountedMonths,
                FIFD = contractVM.IndividualContract.FIFD,
                GivingTime = contractVM.IndividualContract.GivingTime,
                PaymentDate = contractVM.IndividualContract.PaymentDate,
                Percentage = contractVM.IndividualContract.Percentage,
                Period = contractVM.IndividualContract.Period,
                ProductId = contractVM.IndividualContract.ProductId,
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                //Status = ContractStatuses.NewAdded
            };

            if (contractVM.DebtorId.Substring(0, 1) == "L")
            {
                contract.LegalDebtorId = Convert.ToInt64(contractVM.DebtorId.Substring(1));
            }
            else if (contractVM.DebtorId.Substring(0, 1) == "P")
            {
                contract.IndividualDebtorId = Convert.ToInt64(contractVM.DebtorId.Substring(1));
            }

            var newContract = await IndividualContractService.AddNew(contract);
            #endregion


            // Create Relation Contract with Acts
            foreach (var item in contractVM.Acts)
            {
                ActIndividualContract actIndividual = new ActIndividualContract()
                {
                    IndividualContract = newContract,
                    ActId = item
                };

                await ActIndividualContractService.AddNew(actIndividual);
            }

            // Add Guaranter 
            if (contractVM.GuaranterId != null)
            {
                GuaranterContract guaranterContract = new GuaranterContract()
                {
                    IndividualContract = newContract,
                    LegalDebtorId = null,
                    IndividualDebtorId = null,
                    CreateAt = DateTime.Now
                };

                if (contractVM.GuaranterId.Substring(0, 1) == "L")
                {
                    guaranterContract.LegalDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                }
                else if (contractVM.GuaranterId.Substring(0, 1) == "P")
                {
                    guaranterContract.IndividualDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                }
                await GuaranterIndividualContractService.AddNew(guaranterContract);

            }



            var saved = await _serviceFactory.SaveAsync() > 0;
            if (saved) { return RedirectToAction("Index"); }

            else
            {
                #region Services
                IndividualDebtorService = _serviceFactory.IndividualDebtorService;
                LegalDebtorService = _serviceFactory.LegalDebtorService;
                ProductService = _serviceFactory.ProductService;
                #endregion


                #region ViewData
                var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
                var LegalDebtorList = LegalDebtorService.GetALL().Result;
                var productList = ProductService.GetALL().Result;
                Dictionary<string, string> Debtors = new Dictionary<string, string>();
                Dictionary<long, string> products = new Dictionary<long, string>();

                foreach (var item in IndividualDebtorList)
                {
                    Debtors["P" + item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
                }
                foreach (var item in LegalDebtorList)
                {
                    Debtors["L" + item.Id] = item.Name + " - " + item.VOEN;
                }
                foreach (var item in productList)
                {
                    products[item.Id] = item.Name;
                }

                ViewData["Debtors"] = new SelectList(Debtors, "Key", "Value");
                ViewData["ProductPurpose"] = new SelectList(EnumHelper.GetDescriptions<ProductPurpose>(), "Key", "Value");
                ViewData["LoanAssignment"] = new SelectList(EnumHelper.GetDescriptions<LoanAssignment>(), "Key", "Value");
                ViewData["Products"] = new SelectList(products, "Key", "Value");
                #endregion

                return View(contractVM);
            }
        }



        public async Task<IActionResult> Details(long Id)
        {

            #region Services

            IndividualContractService = _serviceFactory.IndividualContractService;
            ActService = _serviceFactory.ActService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            #endregion
            var contract = await IndividualContractService.GetById(Id);


            IndividualDetailsVM detailsVM = new IndividualDetailsVM();
            detailsVM.IndividualContract = contract;

            //Acts
            List<Act> actlist = new List<Act>();
            Dictionary<long, string> acts = new Dictionary<long, string>();

            foreach (var item in contract.ActIndividualContracts)
            {
                var act = ActService.GetById(item.ActId).Result;
                detailsVM.Acts.Add(act);
            }


            // Debtor
            foreach (var item in contract.GuaranterIndividualContract)
            {
                if (item.IndividualDebtorId != null)
                {
                    var Debtor = IndividualDebtorService.GetById(item.IndividualDebtorId.Value).Result;
                    ViewData["guarantorId"] = "P" + Debtor.Id;
                    ViewData["Guarantor"] = Debtor.Name + " " + Debtor.Surname + " " + Debtor.Patronymic;
                }
                else if (item.LegalDebtorId != null)
                {
                    var Debtor = LegalDebtorService.GetById(item.LegalDebtorId.Value).Result;
                    ViewData["guarantorId"] = "L" + Debtor.Id;
                    ViewData["Guarantor"] = Debtor.Name;
                }

            }

            return View(detailsVM);
        }


        public async Task<IActionResult> Delete(long Id)
        {
            IndividualContractService = _serviceFactory.IndividualContractService;
            var IndividualContract = await IndividualContractService.GetById(Id);
            await IndividualContractService.Remove(IndividualContract);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(long id, string returnUrl)
        {

            IndividualContractService = _serviceFactory.IndividualContractService;
            var contract = await IndividualContractService.GetById(id);

            if (contract == null)
            {
                return NotFound();
            }

            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            ActService = _serviceFactory.ActService;
            ProductService = _serviceFactory.ProductService;
            #endregion


            #region ViewData
            var actList = ActService.GetALL().Result;
            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var productList = ProductService.GetALL().Result;
            Dictionary<long, string> acts = new Dictionary<long, string>();
            Dictionary<string, string> Debtors = new Dictionary<string, string>();
            Dictionary<long, string> products = new Dictionary<long, string>();

            foreach (var item in actList)
            {
                acts[item.Id] = item.Name;
            }

            foreach (var item in IndividualDebtorList)
            {
                Debtors["P" + item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                Debtors["L" + item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in productList)
            {
                products[item.Id] = item.Name;
            }

            ViewData["Acts"] = new SelectList(acts, "Key", "Value");
            ViewData["Debtors"] = new SelectList(Debtors, "Key", "Value");
            ViewData["ProductPurpose"] = new SelectList(EnumHelper.GetDescriptions<ProductPurpose>(), "Key", "Value");
            ViewData["LoanAssignment"] = new SelectList(EnumHelper.GetDescriptions<LoanAssignment>(), "Key", "Value");
            ViewData["Products"] = new SelectList(products, "Key", "Value");

            ViewData["returnUrl"] = returnUrl;
            #endregion

            IndividualContractVM contractVM = new IndividualContractVM() { };
            contractVM.IndividualContract = contract;
            foreach (var item in contract.ActIndividualContracts)
            {
                if (item.IndividualContractId == contract.Id)
                {
                    contractVM.Acts.Add(item.ActId);
                }

            }
            foreach (var item in contract.GuaranterIndividualContract)
            {
                if (item.IndividualDebtorId != null)
                {
                    contractVM.GuaranterId = "P" + item.IndividualDebtorId;
                }
                else if (item.LegalDebtorId != null)
                {
                    contractVM.GuaranterId = "L" + item.LegalDebtorId;
                }
            }

            return View(contractVM);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IndividualContractVM contractVM, string returnUrl)
        {
            IndividualContractService = _serviceFactory.IndividualContractService;
            ActIndividualContractService = _serviceFactory.ActIndividualContractService;
            GuaranterIndividualContractService = _serviceFactory.GuaranterIndividualContractService;
            if (ModelState.IsValid)
            {
                var contract = IndividualContractService.GetById(contractVM.Id).Result;

                var acts = contract.ActIndividualContracts;

                //Remove Previous Relations With Acts And Guaranter
                await ActIndividualContractService.RemoveRange(contract.ActIndividualContracts);
                await GuaranterIndividualContractService.RemoveRange(contract.GuaranterIndividualContract);
                await _serviceFactory.SaveAsync();

                #region Update IndividualContract
                contract.DiscountedMonths = contractVM.IndividualContract.DiscountedMonths;
                contract.GivingTime = contractVM.IndividualContract.GivingTime;
                contract.PaymentDate = contractVM.IndividualContract.PaymentDate;
                contract.Percentage = contractVM.IndividualContract.Percentage;
                contract.Period = contractVM.IndividualContract.Period;
                contract.ProductId = contractVM.IndividualContract.ProductId;
                contract.CreditAmount = contractVM.IndividualContract.CreditAmount;
                contract.Currency = contractVM.IndividualContract.Currency;
                contract.Comission = contractVM.IndividualContract.Comission;
                contract.ContractNo = contractVM.IndividualContract.ContractNo;
                contract.FIFD = contractVM.IndividualContract.FIFD;
                //contract.DebtorFullname = contractVM.IndividualContract.DebtorFullname;
                contract.AccountNumber = contractVM.IndividualContract.AccountNumber;

                await IndividualContractService.Update(contract);

                #endregion

                // Create Relation Contract with Acts
                foreach (var item in contractVM.Acts)
                {
                    ActIndividualContract actIndividual = new ActIndividualContract()
                    {
                        IndividualContract = contract,
                        ActId = item
                    };

                    await ActIndividualContractService.AddNew(actIndividual);
                }

                // Add Guaranter 
                GuaranterContract DebtorIndividualContract = new GuaranterContract()
                {
                    IndividualContract = contract,
                    LegalDebtorId = null,
                    IndividualDebtorId = null,
                    CreateAt = DateTime.Now
                };

                if (contractVM.GuaranterId.Substring(0, 1) == "L")
                {
                    DebtorIndividualContract.LegalDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                }
                else if (contractVM.GuaranterId.Substring(0, 1) == "P")
                {
                    DebtorIndividualContract.IndividualDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                }
                await GuaranterIndividualContractService.AddNew(DebtorIndividualContract);




                await _serviceFactory.SaveAsync();
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            ActService = _serviceFactory.ActService;
            ProductService = _serviceFactory.ProductService;
            #endregion


            #region ViewData
            var actList = ActService.GetALL().Result;
            var IndividualDebtorList = IndividualDebtorService.GetALL().Result;
            var LegalDebtorList = LegalDebtorService.GetALL().Result;
            var productList = ProductService.GetALL().Result;
            Dictionary<long, string> actDictionary = new Dictionary<long, string>();
            Dictionary<string, string> Debtors = new Dictionary<string, string>();
            Dictionary<long, string> products = new Dictionary<long, string>();

            foreach (var item in actList)
            {
                actDictionary[item.Id] = item.Name;
            }

            foreach (var item in IndividualDebtorList)
            {
                Debtors["P" + item.Id] = item.Name + " " + item.Surname + " " + item.Patronymic + " - " + item.IDFincode;
            }
            foreach (var item in LegalDebtorList)
            {
                Debtors["L" + item.Id] = item.Name + " - " + item.VOEN;
            }
            foreach (var item in productList)
            {
                products[item.Id] = item.Name;
            }

            ViewData["Acts"] = new SelectList(actDictionary, "Key", "Value");
            ViewData["Debtors"] = new SelectList(Debtors, "Key", "Value");
            ViewData["ProductPurpose"] = new SelectList(EnumHelper.GetDescriptions<ProductPurpose>(), "Key", "Value");
            ViewData["LoanAssignment"] = new SelectList(EnumHelper.GetDescriptions<LoanAssignment>(), "Key", "Value");
            ViewData["Products"] = new SelectList(products, "Key", "Value");

            ViewData["returnUrl"] = returnUrl;
            #endregion
            return View(contractVM);
        }



        public IActionResult GuarantorDetails(string Id)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;

            if (Id.Substring(0, 1) == "L")
            {
                var legalId = Convert.ToInt64(Id.Substring(1));
                return RedirectToAction("Details", "LegalDebtor", new { id = legalId });
            }
            else if (Id.Substring(0, 1) == "P")
            {
                var physicalId = Convert.ToInt64(Id.Substring(1));
                return RedirectToAction("Details", "IndividualDebtor", new { id = physicalId });
            }


            return RedirectToAction("Details");
        }





        public JsonResult GetDebtorFullnameAndAccountNumber(string DebtorId)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            var Id = Convert.ToInt64(DebtorId.Substring(1));
            var accountNumber = "";
            if (DebtorId.Substring(0, 1) == "L")
            {
                var legal = LegalDebtorService.GetById(Id).Result;
                foreach (var item in legal.BankAccounts)
                {
                    if (item.IsMainAccount)
                    {
                        accountNumber = item.AccountNumber;
                    }
                }
                return Json(new
                {
                    FullName = legal.Name,
                    AccounNumber = accountNumber
                });
            }
            else if (DebtorId.Substring(0, 1) == "P")
            {
                var physical = IndividualDebtorService.GetById(Id).Result;
                foreach (var item in physical.BankAccounts)
                {
                    if (item.IsMainAccount)
                    {
                        accountNumber = item.AccountNumber;
                    }
                }
                return Json(new
                {
                    FullName = physical.Name + " " +
                    physical.Surname + " " +
                    physical.Patronymic,
                    AccounNumber = accountNumber
                });
            }


            return Json(new
            {
                Fullname = "",
                AccountNumber = ""
            });

        }




        public async Task<SelectList> GetAllSubActsByMainActId(long actId)
        {
            ActService = _serviceFactory.ActService;
            var act = await ActService.GetActWithAllInfo(actId);
            Dictionary<long, string> acts = new Dictionary<long, string>();

            foreach (var item in act.Acts)
            {
                if (item.ActType != ActTypes.Final)
                {
                    acts[item.Id] = item.Name;
                }
            }

            var subActs = new SelectList(acts, "Key", "Value");

            return subActs;


        }


        public SelectList GetAllMainActsByDebtorId(string DebtorId)
        {
            #region Services
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            ActService = _serviceFactory.ActService;
            #endregion

            ICollection<Act> actList = new List<Act>();

            if (DebtorId.Substring(0, 1) == "L")
            {
                actList = LegalDebtorService.GetById(Convert.ToInt64(DebtorId.Substring(1))).Result.Acts;
            }
            else if (DebtorId.Substring(0, 1) == "P")
            {
                actList = IndividualDebtorService.GetById(Convert.ToInt64(DebtorId.Substring(1))).Result.Acts;
            }

            Dictionary<long, string> acts = new Dictionary<long, string>();

            foreach (var item in actList)
            {
                if (item.ActType == ActTypes.Main)
                {
                    acts[item.Id] = item.Name + "%" + item.Date;
                }
            }


            var mainActs = new SelectList(acts, "Key", "Value");


            return mainActs;

        }


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

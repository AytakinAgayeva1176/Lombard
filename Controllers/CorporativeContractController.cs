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
    public class CorporativeContractController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<CorporativeContractController> _logger;
        readonly IServiceFactory _serviceFactory;

        public CorporativeContractController(IServiceFactory serviceFactory, ILogger<CorporativeContractController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        ICorporativeContractService CorporativeContractService { get; set; }
        ILegalDebtorService LegalDebtorService { get; set; }
        IIndividualDebtorService IndividualDebtorService { get; set; }
        IGuaranterContractService GuaranterContractService { get; set; }
        IProductService ProductService { get; set; }
        #endregion

        public async Task<IActionResult> Index()
        {
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            var CorporativeContractList = await CorporativeContractService.GetALL();
            return View(CorporativeContractList);
        }

        public IActionResult Create(long? parentId, string returnUrl)
        {

            ViewData["returnUrl"] = returnUrl;
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


            if (parentId != null)
            {
                ViewData["parentId"] = parentId;

                CorporativeContractService = _serviceFactory.CorporativeContractService;
                var parentContract = CorporativeContractService.GetCorporativeContractWithAllInfo(parentId.Value).Result;
                ViewData["ContractNo"] = parentContract.ContractNo + "-" + (parentContract.AdditionalContracts.Count + 1);

                if (parentContract.LegalDebtor != null)
                {
                    ViewData["Debtor"] = parentContract.LegalDebtor.Name;
                    foreach (var item in parentContract.LegalDebtor.BankAccounts)
                    {
                        if (item.IsMainAccount)
                        {
                            ViewData["AccountNumber"] = item.AccountNumber;
                        }
                    }
                }
                if (parentContract.IndividualDebtor != null)
                {
                    ViewData["Debtor"] = parentContract.IndividualDebtor.Name
                        + " " + parentContract.IndividualDebtor.Surname
                        + " " + parentContract.IndividualDebtor.Patronymic;
                    foreach (var item in parentContract.IndividualDebtor.BankAccounts)
                    {
                        if (item.IsMainAccount)
                        {
                            ViewData["AccountNumber"] = item.AccountNumber;
                        }
                    }
                }

            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CorporativeContractVM contractVM, long? parentId)
        {
            //if (ModelState.IsValid)
            //{
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            GuaranterContractService = _serviceFactory.GuaranterContractService;

            #region Create CorporativeContract
            CorporativeContract contract = new CorporativeContract()
            {
                AccountNumber = contractVM.CorporativeContract.AccountNumber,
                Comission = contractVM.CorporativeContract.Comission,
                ContractNo = contractVM.CorporativeContract.ContractNo,
                CreditAmount = contractVM.CorporativeContract.CreditAmount,
                Currency = contractVM.CorporativeContract.Currency,
                DiscountedMonths = contractVM.CorporativeContract.DiscountedMonths,
                FIFD = contractVM.CorporativeContract.FIFD,
                GivingTime = contractVM.CorporativeContract.GivingTime,
                PaymentDate = contractVM.CorporativeContract.PaymentDate,
                Percentage = contractVM.CorporativeContract.Percentage,
                Period = contractVM.CorporativeContract.Period,
                ProductId = contractVM.CorporativeContract.ProductId,
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                //Status = ContractStatuses.NewAdded,
                ParentId = contractVM.CorporativeContract.ParentId,
                CreditLimit = contractVM.CorporativeContract.CreditLimit,
            };


            if (contractVM.DebtorId != null)
            {
                if (contractVM.DebtorId.Substring(0, 1) == "L")
                {
                    contract.LegalDebtorId = Convert.ToInt64(contractVM.DebtorId.Substring(1));
                }
                else if (contractVM.DebtorId.Substring(0, 1) == "P")
                {
                    contract.IndividualDebtorId = Convert.ToInt64(contractVM.DebtorId.Substring(1));
                }

            }
            if (parentId != null)
            {
                var mainContract = CorporativeContractService.GetCorporativeContractWithAllInfo(parentId.Value).Result;
                contract.LegalDebtor = mainContract.LegalDebtor;
                contract.IndividualDebtor = mainContract.IndividualDebtor;
                contract.ProductId = mainContract.ProductId;
                contract.Currency = mainContract.Currency;
            }

            var newContract = await CorporativeContractService.AddNew(contract);
            #endregion


            // Add Guaranter 

            if (parentId == null)
            {

                GuaranterContract guaranterContract = new GuaranterContract()
                {
                    CorporativeContract = newContract,
                    LegalDebtorId = null,
                    IndividualDebtorId = null,
                    CreateAt = DateTime.Now
                };

                if (contractVM.GuaranterId != null)
                {
                    if (contractVM.GuaranterId.Substring(0, 1) == "L")
                    {
                        guaranterContract.LegalDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                    }
                    else if (contractVM.GuaranterId.Substring(0, 1) == "P")
                    {
                        guaranterContract.IndividualDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                    }
                }


                await GuaranterContractService.AddNew(guaranterContract);


            }

            await _serviceFactory.SaveAsync();
            if (parentId != null)
            {
                return RedirectToAction("Details", new { id = parentId });
            }
            return RedirectToAction("Index");

            //}
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

        public async Task<IActionResult> Details(long Id)
        {
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            var contract = await CorporativeContractService.GetCorporativeContractWithAllInfo(Id);
            if (contract.ParentId == null)
            {
                foreach (var item in contract.GuaranterContract)
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
            }
            else
            {
                var mainContract = await CorporativeContractService.GetCorporativeContractWithAllInfo(contract.ParentId.Value);
                foreach (var item in mainContract.GuaranterContract)
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
            }
            return View(contract);
        }

        public IActionResult GuarantorDetails(string Id, long? corporativContractId)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;

            if (Id != null && Id.Substring(0, 1) == "L")
            {
                var legalId = Convert.ToInt64(Id.Substring(1));
                return RedirectToAction("Details", "LegalDebtor", new { id = legalId });
            }
            else if (Id != null && Id.Substring(0, 1) == "P")
            {
                var physicalId = Convert.ToInt64(Id.Substring(1));
                return RedirectToAction("Details", "IndividualDebtor", new { id = physicalId });
            }


            return RedirectToAction("Details", new { id = corporativContractId });
        }

        public async Task<IActionResult> Edit(long Id, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            var contract = await CorporativeContractService.GetById(Id);
            CorporativeContractVM contractVM = new CorporativeContractVM() { };
            contractVM.CorporativeContract = contract;

            foreach (var item in contract.GuaranterContract)
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

            if (contract == null)
            {
                return NotFound();
            }
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

        // POST: LegalDebtor/Edit/5

        [HttpPost()]

        public async Task<IActionResult> Edit(CorporativeContractVM contractVM)
        {
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            GuaranterContractService = _serviceFactory.GuaranterContractService;
            if (ModelState.IsValid)
            {
                var contract = CorporativeContractService.GetById(contractVM.CorporativeContract.Id).Result;

                if (contract.ParentId == null)
                {
                    await GuaranterContractService.RemoveRange(contract.GuaranterContract);
                    await _serviceFactory.SaveAsync();
                }

                #region Update Corporative Contract
                contract.DiscountedMonths = contractVM.CorporativeContract.DiscountedMonths;
                contract.GivingTime = contractVM.CorporativeContract.GivingTime;
                contract.PaymentDate = contractVM.CorporativeContract.PaymentDate;
                contract.Percentage = contractVM.CorporativeContract.Percentage;
                contract.Period = contractVM.CorporativeContract.Period;
                contract.ProductId = contractVM.CorporativeContract.ProductId;
                contract.CreditAmount = contractVM.CorporativeContract.CreditAmount;
                contract.Currency = contractVM.CorporativeContract.Currency;
                contract.Comission = contractVM.CorporativeContract.Comission;
                contract.ContractNo = contractVM.CorporativeContract.ContractNo;
                contract.FIFD = contractVM.CorporativeContract.FIFD;
                contract.CreditLimit = contractVM.CorporativeContract.CreditLimit;
                contract.AccountNumber = contractVM.CorporativeContract.AccountNumber;
                contract.ParentId = contractVM.CorporativeContract.ParentId;
                contract.Status = contractVM.CorporativeContract.Status;
                await CorporativeContractService.Update(contract);

                #endregion

                #region Edit Guaranter
                if (contract.ParentId == null && contractVM.GuaranterId != null)
                {
                    GuaranterContract GuaranterContract = new GuaranterContract()
                    {
                        CorporativeContract = contract,
                        LegalDebtorId = null,
                        IndividualDebtorId = null,
                        CreateAt = DateTime.Now
                    };

                    if (contractVM.GuaranterId.Substring(0, 1) == "L")
                    {
                        GuaranterContract.LegalDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                    }
                    else if (contractVM.GuaranterId.Substring(0, 1) == "P")
                    {
                        GuaranterContract.IndividualDebtorId = Convert.ToInt64(contractVM.GuaranterId.Substring(1));
                    }
                    await GuaranterContractService.AddNew(GuaranterContract);


                }
                #endregion


                await _serviceFactory.SaveAsync();
                if (contract.ParentId != null)
                {
                    return RedirectToAction("Details", new { id = contract.ParentId });
                }
                return RedirectToAction("Index");
            }

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



        public async Task<IActionResult> Delete(long Id)
        {
            CorporativeContractService = _serviceFactory.CorporativeContractService;
            var CorporativeContract = await CorporativeContractService.GetById(Id);
            await CorporativeContractService.Remove(CorporativeContract);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
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

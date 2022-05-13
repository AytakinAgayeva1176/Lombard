using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Lombard.Domain.ViewModels;
using Lombard.Helpers;
using Lombard.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class LegalDebtorController : Controller
    {
        #region This Ctor

        private readonly ILogger<LegalDebtorController> _logger;
        readonly IServiceFactory _serviceFactory;
        public LegalDebtorController(IServiceFactory serviceFactory, ILogger<LegalDebtorController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion


        #region Interfaces
        ILegalDebtorService LegalDebtorService { get; set; }
        IBankAccountService BankAccountService { get; set; }
        IBankService BankService { get; set; }
        IFamilyMembersService FamilyMembersService { get; set; }
        IBusinessAreaService BusinessAreaService { get; set; }
        IRelationTypeService RelationTypeService { get; set; }
        #endregion


        #region LegalDebtor

        /// <summary>
        /// Get All LegalDebtor 
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<IActionResult> Index()
        {

            LegalDebtorService = _serviceFactory.LegalDebtorService;
            IEnumerable<LegalDebtor> list = await LegalDebtorService.GetALL();
            return View(list);
        }

        /// <summary>
        /// Get LegalDebtor's Details
        /// </summary>
        /// <param name="Id">LegalDebtor Id</param> 
        /// <returns></returns>
        /// 



        public async Task<IActionResult> Details(long id)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            var LegalDebtor = await LegalDebtorService.GetById(id);
            if (LegalDebtor == null)
            {
                return NotFound();
            }

            return View(LegalDebtor);
        }


        /// <summary>
        /// Create LegalDebtor 
        /// </summary>
        /// <returns></returns>

        // GET: LegalDebtor/Create
        public IActionResult Create()
        {
            #region ViewData
            BankService = _serviceFactory.BankService;
            BusinessAreaService = _serviceFactory.BusinessAreaService;
            var bankList = BankService.GetALL().Result;
            var businessAreaList = BusinessAreaService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            Dictionary<long, string> businessAreas = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            foreach (var item in businessAreaList)
            {
                businessAreas[item.Id] = item.Area;
            }

            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            ViewData["BusinessAreas"] = new SelectList(businessAreas, "Key", "Value");
            ViewData["Cities"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<Cities>(), "Key", "Value");
            ViewData["RegistrationStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<RegistrationStatuses>(), "Key", "Value");
            ViewData["ResidentStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<ResidentStatus>(), "Key", "Value");

            #endregion

            return View();
        }

        // POST: LegalDebtor/Create
        [HttpPost]


        public async Task<IActionResult> Create(IFormCollection form)
        {
            DateTime result;
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            List<BankAccount> bankAccounts = new List<BankAccount>();
            List<FamilyMember> familyMembers = new List<FamilyMember>();
            if (form["bankCount"] != "")
            {
                for (int i = 0; i <= int.Parse(form["bankCount"]); i++)
                {
                    if (form.ContainsKey("Code_" + i) && form.ContainsKey("BankId_" + i))
                    {
                        BankAccount bankAccount = new BankAccount()
                        {
                            AccountNumber = form["AccountNumber_" + i],
                            BankId = int.Parse(form["BankId_" + i]),
                            Code = form["Code_" + i]
                        };

                        if (form.ContainsKey("ExpireDate_" + i))
                        {
                            bankAccount.ExpireDate = DateTime.Parse(form["ExpireDate_" + i]);
                        }
                        bankAccounts.Add(bankAccount);
                    }
                }
            }

            if (form["memberCount"] != "")
            {
                for (int i = 0; i <= int.Parse(form["memberCount"]); i++)
                {
                    double doubleRes = 0;
                    if (form.ContainsKey("RelationType_" + i))
                    {
                        FamilyMember member = new FamilyMember()
                        {
                            Fullname = form["Fullname_" + i],
                            BirthDate = DateTime.TryParse(form["BirthDate_" + i].ToString(), out result) ? (DateTime?)result : null,
                            BirthPlace = form["BirthPlace_" + i],
                            Email = form["Email_" + i],
                            WorkPlace = form["WorkPlace_" + i],
                            WorkPosition = form["WorkPosition_" + i],
                            Salary = Double.TryParse(form["Salary_" + i].ToString(), out doubleRes) ? (Double?)doubleRes : null,
                            MobileNumber = form["MobileNumber_" + i],
                            MobileAdditional = form["MobileAdditional_" + i],
                            RelationTypeId = int.Parse(form["RelationType_" + i])
                        };

                        familyMembers.Add(member);
                    }
                }
            }


            LegalDebtor Debtor = new LegalDebtor()
            {
                FamilyMembers = familyMembers,
                BankAccounts = bankAccounts,
                Name = form["Name"],
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive,
                Email = form["Email"],
                MobileNumber = form["MobileNumber"],
                MobileAdditional1 = form["MobileAdditional1"],
                MobileAdditional2 = form["MobileAdditional2"],
                Phone = form["Phone"],
                PhoneAdditional = form["PhoneAdditional"],
                RegisteredAddress = form["RegisteredAddress"],
                CurrentLivingAddress = form["CurrentLivingAddress"],
                BusinessArea = form["BusinessAreaId"],
                VOEN = form["VOEN"],
                DirectorName = form["VOEN"],
                Description = form["VOEN"],
                HeadAccountant = form["VOEN"],
                City = form["City"],
                RegistrationStatus= form["RegistrationStatuse"],
                IsResident= form["ResidentStatus"]

            };

            await LegalDebtorService.AddNew(Debtor);
            await _serviceFactory.SaveAsync();
            return RedirectToAction("Index");

        }



        // GET: LegalDebtor/Edit/5
        public async Task<IActionResult> Edit(long id, string returnUrl)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            var LegalDebtor = await LegalDebtorService.GetById(id);
            if (LegalDebtor == null)
            {
                return NotFound();
            }
            #region ViewData
            BusinessAreaService = _serviceFactory.BusinessAreaService;
            var businessAreaList = BusinessAreaService.GetALL().Result;
            Dictionary<long, string> businessAreas = new Dictionary<long, string>();
            foreach (var item in businessAreaList)
            {
                businessAreas[item.Id] = item.Area;
            }

            ViewData["BusinessAreas"] = new SelectList(businessAreas, "Key", "Value");
           
            ViewData["RegistrationStatuses"] = new SelectList(EnumHelper.GetDescriptions<RegistrationStatuses>(), "Key", "Value");

            ViewData["Cities"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<Cities>(), "Key", "Value");

            ViewData["returnUrl"] = returnUrl;

            #endregion
            return View(LegalDebtor);
        }

        // POST: LegalDebtor/Edit/5

        [HttpPost()]

        public async Task<IActionResult> Edit(LegalDebtor Debtor, string returnUrl)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;

            if (ModelState.IsValid)
            {
                await LegalDebtorService.Update(Debtor);
                await _serviceFactory.SaveAsync();
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }

            #region ViewData
            BusinessAreaService = _serviceFactory.BusinessAreaService;
            var businessAreaList = BusinessAreaService.GetALL().Result;
            Dictionary<long, string> businessAreas = new Dictionary<long, string>();
            foreach (var item in businessAreaList)
            {
                businessAreas[item.Id] = item.Area;
            }

            ViewData["BusinessAreas"] = new SelectList(businessAreas, "Key", "Value");

            ViewData["Cities"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<Cities>(), "Key", "Value");
            ViewData["RegistrationStatuses"] = new SelectList(EnumHelper.GetDescriptions<RegistrationStatuses>(), "Key", "Value");
            ViewData["returnUrl"] = returnUrl;

            #endregion
            return View(Debtor);
        }


        public async Task<IActionResult> Delete(long id)
        {
            LegalDebtorService = _serviceFactory.LegalDebtorService;
            var LegalDebtor = await LegalDebtorService.GetById(id);
            await LegalDebtorService.Remove(LegalDebtor);
            await _serviceFactory.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion


        #region BankAccount

        public IActionResult CreateBankAccount(string returnUrl)
        {
            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;
            #endregion

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBankAccount(long id, string returnUrl, BankAccountVM bankAccountVM)
        {
            if (ModelState.IsValid)
            {
                BankAccountService = _serviceFactory.BankAccountService;
                LegalDebtorService = _serviceFactory.LegalDebtorService;
                var Debtor = await LegalDebtorService.GetById(id);

                BankAccount bankAccount = new BankAccount()
                {
                    Code = bankAccountVM.Code,
                    AccountNumber = bankAccountVM.AccountNumber,
                    ExpireDate = bankAccountVM.ExpireDate,
                    IsMainAccount = bankAccountVM.IsMainAccount,
                    BankId = bankAccountVM.BankId,
                    CreateAt = DateTime.Now,
                    RecordStatus = RecordStatus.IsActive
                };

                if (Debtor != null) { bankAccount.LegalDebtor = Debtor; }

                if (bankAccountVM.IsMainAccount)
                {
                    foreach (var item in Debtor.BankAccounts)
                    { item.IsMainAccount = false; }

                    await BankAccountService.UpdateRange(Debtor.BankAccounts);
                    bankAccount.IsMainAccount = true;
                }

                await BankAccountService.AddNew(bankAccount);
                await _serviceFactory.SaveAsync();

                return RedirectToAction("Details", new { id });

            }


            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;
            #endregion

            return View(bankAccountVM);
            // return PartialView("_BankCreate", newAccount);
        }


        public async Task<IActionResult> BankAccountDetails(long Id)
        {
            BankAccountService = _serviceFactory.BankAccountService;
            var bank = await BankAccountService.GetById(Id);
            return View(bank);
        }


        public async Task<IActionResult> DeleteBankAccount(long Id)
        {
            BankAccountService = _serviceFactory.BankAccountService;
            var bank = await BankAccountService.GetById(Id);
            await BankAccountService.Remove(bank);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Details", new { id = bank.LegalDebtorId });
        }



        public async Task<IActionResult> EditBankAccount(long Id, string returnUrl)
        {
            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");

            ViewData["returnUrl"] = returnUrl;

            #endregion

            BankAccountService = _serviceFactory.BankAccountService;
            var bankAccount = await BankAccountService.GetById(Id);
            return View(bankAccount);
        }

        [HttpPost]
        public async Task<IActionResult> EditBankAccount(BankAccount bankAccount, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                BankAccountService = _serviceFactory.BankAccountService;
                await BankAccountService.Update(bankAccount);
                LegalDebtorService = _serviceFactory.LegalDebtorService;
                var Debtor = await LegalDebtorService.GetById(bankAccount.LegalDebtorId.Value);
                if (bankAccount.IsMainAccount)
                {
                    foreach (var item in Debtor.BankAccounts)
                    {
                        if (item.Id != bankAccount.Id)
                        {
                            item.IsMainAccount = false;
                            await BankAccountService.Update(item);
                        }
                    }
                }
                await _serviceFactory.SaveAsync();
                ViewData["returnUrl"] = returnUrl;
                return RedirectToAction("Details", new { id = bankAccount.LegalDebtorId });
                
            }

            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            #endregion

            return View(bankAccount);
        }

        #endregion


        #region FamilyMembers

        public IActionResult AddFamilyMember(string returnUrl)
        {
            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFamilyMember(long id, string returnUrl, FamilyMembersVM familyMembersVM)
        {
            if (ModelState.IsValid)
            {
                FamilyMembersService = _serviceFactory.FamilyMembersService;

                FamilyMember member = new FamilyMember()
                {
                    Fullname = familyMembersVM.Fullname,
                    Salary = familyMembersVM.Salary,
                    MobileAdditional = familyMembersVM.MobileAdditional,
                    BirthDate = familyMembersVM.BirthDate,
                    BirthPlace = familyMembersVM.BirthPlace,
                    Email = familyMembersVM.Email,
                    MobileNumber = familyMembersVM.MobileNumber,
                    RelationTypeId = familyMembersVM.RelationTypeId,
                    WorkPlace = familyMembersVM.WorkPlace,
                    WorkPosition = familyMembersVM.WorkPosition,
                    LegalDebtorId = id,
                    CreateAt = DateTime.Now,
                    RecordStatus = RecordStatus.IsActive
                };


                await FamilyMembersService.AddNew(member);
                await _serviceFactory.SaveAsync();

                return RedirectToAction("Details", new { id });

            }

            #region ViewData
            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;

            #endregion

            return View(familyMembersVM);
            // return PartialView("_BankCreate", newAccount);
        }


        public async Task<IActionResult> FamilyMemberDetails(long Id)
        {
            FamilyMembersService = _serviceFactory.FamilyMembersService;
            var member = await FamilyMembersService.GetById(Id);
            return View(member);
        }


        public async Task<IActionResult> DeleteFamilyMember(long Id)
        {
            FamilyMembersService = _serviceFactory.FamilyMembersService;
            var member = await FamilyMembersService.GetById(Id);
            await FamilyMembersService.Remove(member);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Details", new { id = member.LegalDebtorId });
        }



        public async Task<IActionResult> EditFamilyMember(long Id, string returnUrl)
        {
            #region ViewData
            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;

            #endregion
            FamilyMembersService = _serviceFactory.FamilyMembersService;
            var member = await FamilyMembersService.GetById(Id);
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> EditFamilyMember(FamilyMember member, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FamilyMembersService = _serviceFactory.FamilyMembersService;
                await FamilyMembersService.Update(member);
                await _serviceFactory.SaveAsync();
                return RedirectToAction("Details", new { id = member.LegalDebtorId });
            }

            #region ViewData
            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["returnUrl"] = returnUrl;

            #endregion

            return View(member);
        }


        #endregion


        public IActionResult Cancel(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index");
        }



        public ActionResult DisplayAddFamilyMember(int iCnt)
        {
            #region ViewData
            int newCount = iCnt;
            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["newCount"] = newCount;

            #endregion

            return PartialView("_AddFamilyMember");
        }

        public ActionResult DisplayAddBankAccount(int jCnt)
        {
            int newCount = jCnt;
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");

            ViewData["newCount"] = newCount;
            return PartialView("_AddBankAccount");
        }

        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

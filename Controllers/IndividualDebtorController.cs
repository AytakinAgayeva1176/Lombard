using System;
using System.Collections.Generic;
using Lombard.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lombard.Domain.Contexts;
using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Microsoft.Extensions.Logging;
using Lombard.Domain.Contracts.Services;
using Lombard.Services;
using Lombard.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Lombard.Controllers
{
    public class IndividualDebtorController : Controller, IDisposable
    {
        #region This Ctor
        private readonly ILogger<IndividualDebtorController> _logger;
        readonly IServiceFactory _serviceFactory;

        public IndividualDebtorController(IServiceFactory serviceFactory, ILogger<IndividualDebtorController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;

        }
        #endregion

        #region Interfaces
        IIndividualDebtorService IndividualDebtorService { get; set; }
        IFamilyMembersService FamilyMembersService { get; set; }
        IBankAccountService BankAccountService { get; set; }
        IBankService BankService { get; set; }
        IRelationTypeService RelationTypeService { get; set; }
        IJobService JobService { get; set; }
        IActService ActService { get; set; }
        

        #endregion

        #region IndividualDebtor

        /// <summary>
        /// Get All IndividualDebtor 
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<IActionResult> Index()
        {

            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            IEnumerable<IndividualDebtor> list = await IndividualDebtorService.GetALL();
            return View(list);
        }

        /// <summary>
        /// Get IndividualDebtor's Details
        /// </summary>
        /// <param name="Id">IndividualDebtor Id</param> 
        /// <returns></returns>
        /// 



        public async Task<IActionResult> Details(long id)
        {
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            ActService = _serviceFactory.ActService;
            var individualDebtor = await IndividualDebtorService.GetById(id);

            IndividualDebtorWithALLInfo individualDebtorWithALLInfo = new IndividualDebtorWithALLInfo()
            {
                Acts = individualDebtor.Acts,
                Jobs = individualDebtor.Jobs,
                FamilyMembers = individualDebtor.FamilyMembers,
                BankAccounts = individualDebtor.BankAccounts,
                CorporativeContracts = individualDebtor.CorporativeContracts,


                Id = individualDebtor.Id,
                UserId = individualDebtor.UserId,
                Name = individualDebtor.Name,
                Surname = individualDebtor.Surname,
                Patronymic = individualDebtor.Patronymic,
                BirthDate = individualDebtor.BirthDate,
                BirthPlace = individualDebtor.BirthPlace,
                RegisteredAddress = individualDebtor.RegisteredAddress,
                IDFincode = individualDebtor.IDFincode,
                Citizenship = individualDebtor.Citizenship,
                City = individualDebtor.City,
                Description = individualDebtor.Description,
                IDExpireDate = individualDebtor.IDExpireDate,
                IDGiveDate = individualDebtor.IDGiveDate,
                IDOrganizationName = individualDebtor.IDOrganizationName,
                IDSerialNumber = individualDebtor.IDSerialNumber,
                Education = individualDebtor.Education,
                Email = individualDebtor.Email,
                VOEN = individualDebtor.VOEN,
                CurrentLivingAddress = individualDebtor.CurrentLivingAddress,
                Gender = individualDebtor.Gender,
                MarialStatuses = individualDebtor.MarialStatuses,
                IsOwner = individualDebtor.IsOwner,
                IsResident = individualDebtor.IsResident,
                MobileAdditional1 = individualDebtor.MobileAdditional1,
                MobileAdditional2 = individualDebtor.MobileAdditional2,
                MobileAdditional3 = individualDebtor.MobileAdditional3,
                MobileAdditional4 = individualDebtor.MobileAdditional4,
                MobileNumber = individualDebtor.MobileNumber,
                Phone = individualDebtor.Phone,
                PhoneAdditional = individualDebtor.PhoneAdditional,
                RegistrationStatus = individualDebtor.RegistrationStatus
            };
            if (individualDebtor.Acts.Count != 0)
            {

                Act final = new Act()
                {
                    ActType = ActTypes.Final,
                    TotalGoldLikvidPrice = 0,
                    TotalGoldMarketPrice = 0,
                    TotalGoldNetWeight = 0,
                    TotalGoldWeight = 0,
                    TotalJewelsWeight = 0,
                    TotalGoldCount = 0
                };

                foreach (var act in individualDebtor.Acts)
                {
                    if (act.ActType == ActTypes.Main)
                    {
                        var subActs = ActService.GetActWithAllInfo(act.Id);



                        final.TotalGoldLikvidPrice += act.TotalGoldLikvidPrice;
                        final.TotalGoldMarketPrice += act.TotalGoldMarketPrice;
                        final.TotalGoldNetWeight += act.TotalGoldNetWeight;
                        final.TotalGoldWeight += act.TotalGoldWeight;
                        final.TotalJewelsWeight += act.TotalJewelsWeight;
                        final.TotalGoldCount += act.TotalGoldCount;

                        foreach (var sub in subActs.Result.Acts)
                        {
                            if (sub.ActType == ActTypes.Addition)
                            {
                                final.TotalGoldLikvidPrice += sub.TotalGoldLikvidPrice;
                                final.TotalGoldMarketPrice += sub.TotalGoldMarketPrice;
                                final.TotalGoldNetWeight += sub.TotalGoldNetWeight;
                                final.TotalGoldWeight += sub.TotalGoldWeight;
                                final.TotalJewelsWeight += sub.TotalJewelsWeight;
                                final.TotalGoldCount += sub.TotalGoldCount;
                            }
                            else if (sub.ActType == ActTypes.Extractions)
                            {
                                final.TotalGoldLikvidPrice -= sub.TotalGoldLikvidPrice;
                                final.TotalGoldMarketPrice -= sub.TotalGoldMarketPrice;
                                final.TotalGoldNetWeight -= sub.TotalGoldNetWeight;
                                final.TotalGoldWeight -= sub.TotalGoldWeight;
                                final.TotalJewelsWeight -= sub.TotalJewelsWeight;
                                final.TotalGoldCount -= sub.TotalGoldCount;
                            }
                        }

                    }

                }


                individualDebtorWithALLInfo.Act = final;
            }


            if (individualDebtorWithALLInfo == null)
            {
                return NotFound();
            }

            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }

            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            ViewData["MarialStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<MarialStatuses>(), "Key", "Value");
            ViewData["GenderTypes"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<GenderTypes>(), "Key", "Value");


            #endregion
            return View(individualDebtorWithALLInfo);
        }


        /// <summary>
        /// Create IndividualDebtor 
        /// </summary>
        /// <returns></returns>

        // GET: IndividualDebtor/Create _AddJob
        public IActionResult Create()
        {
            #region ViewData
            BankService = _serviceFactory.BankService;
            var bankList = BankService.GetALL().Result;
            Dictionary<long, string> banks = new Dictionary<long, string>();
            foreach (var item in bankList)
            {
                banks[item.Id] = item.Name;
            }

            RelationTypeService = _serviceFactory.RelationTypeService;
            var relationTypeList = RelationTypeService.GetALL().Result;
            Dictionary<long, string> relationTypes = new Dictionary<long, string>();
            foreach (var item in relationTypeList)
            {
                relationTypes[item.Id] = item.Type;
            }

            ViewData["RelationTypes"] = new SelectList(relationTypes, "Key", "Value");
            ViewData["Banks"] = new SelectList(banks, "Key", "Value");
            ViewData["MarialStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<MarialStatuses>(), "Key", "Value");
            ViewData["GenderTypes"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<GenderTypes>(), "Key", "Value");
            ViewData["EducationTypes"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<EducationTypes>(), "Key", "Value");
            ViewData["ResidentStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<ResidentStatus>(), "Key", "Value");
            #endregion

            return View();
        }



        /// <summary>
        /// Create IndividualDebtor 
        /// </summary>
        /// <param name="createIndividualDebtorVM">IndividualDebtor viewModel</param> 
        /// <returns></returns>

        // POST: IndividualDebtor/Create
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            DateTime result;
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            List<Job> jobs = new List<Job>();
            List<BankAccount> bankAccounts = new List<BankAccount>();
            List<FamilyMember> familyMembers = new List<FamilyMember>();
            if (form["jobCount"] != "")
            {
                for (int i = 0; i <= int.Parse(form["jobCount"]); i++)
                {
                    decimal decimalRes;
                    double doubleRes;
                    if (form.ContainsKey("CompanyName_" + i) )
                    {
                        Job job = new Job()
                        {
                            CompanyName = form["CompanyName_" + i],
                            Experience = double.TryParse(form["Experience_" + i].ToString(), out doubleRes) ? (double?)doubleRes : null,
                            Adress = form["Adress_" + i],
                            Position = form["Position_" + i],
                            Salary = decimal.TryParse(form["DebtorSalary_" + i].ToString(), out decimalRes) ? (decimal?)decimalRes : null,
                            IsCurrent = true,
                            CreateAt = DateTime.Now,
                            RecordStatus = RecordStatus.IsActive,
                            VOEN = form["VOEN_" + i],
                            Phone = form["Phone_" + i],
                        };

                        jobs.Add(job);
                    }

                }
            }

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

            IndividualDebtor Debtor = new IndividualDebtor()
            {
                FamilyMembers = familyMembers,
                BankAccounts = bankAccounts,
                Jobs = jobs,


                Name = form["Name"],
                Surname = form["Surname"],
                Patronymic = form["Patronymic"],
                BirthDate = DateTime.Parse(form["BirthDate"]),
                BirthPlace = form["BirthPlace"],
                IDFincode = form["IDFincode"],
                IDSerialNumber = form["IDSerialNumber"],
                IDOrganizationName = form["IDOrganizationName"],
                Citizenship = form["Citizenship"],
                IDExpireDate = DateTime.Parse(form["IDExpireDate"]),
                IDGiveDate = DateTime.TryParse(form["IDGiveDate"].ToString(), out result) ? (DateTime?)result : null,
                MarialStatuses = form["MarialStatuses"],
                Gender = form["Gender"],
                Email = form["Email"],
                MobileNumber = form["MobileNumber"],
                MobileAdditional1 = form["MobileAdditional1"],
                MobileAdditional2 = form["MobileAdditional2"],
                Phone = form["Phone"],
                PhoneAdditional = form["PhoneAdditional"],
                RegisteredAddress = form["RegisteredAddress"],
                CurrentLivingAddress = form["CurrentLivingAddress"],
                Education = form["Education"],
                VOEN = form["VOEN"],
                Description = form["Description"],
                City = form["City"],
                RegistrationStatus = form["RegistrationStatus"],
                MobileAdditional3 = form["MobileAdditional3"],
                MobileAdditional4 = form["MobileAdditional4"],
                IsResident = form["ResidentStatus"],
                CreateAt = DateTime.Now,
                RecordStatus = RecordStatus.IsActive
            };


            //Voen isowner
            if (form["IsOwner"] == "true")
            {
                Debtor.IsOwner = true;
                Debtor.VOEN = form["VOEN"];
            }
            await IndividualDebtorService.AddNew(Debtor);
            await _serviceFactory.SaveAsync();
            return RedirectToAction("Index");

        }

        // GET: IndividualDebtor/Edit/5
        public async Task<IActionResult> Edit(long id, string returnUrl)
        {
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            var IndividualDebtor = await IndividualDebtorService.GetById(id);
            if (IndividualDebtor == null)
            {
                return NotFound();
            }
            #region ViewData
            ViewData["MarialStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<MarialStatuses>(), "Key", "Value");
            ViewData["GenderTypes"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<GenderTypes>(), "Key", "Value");

            ViewData["returnUrl"] = returnUrl;
            #endregion
            return View(IndividualDebtor);
        }

        // POST: IndividualDebtor/Edit/5

        [HttpPost()]

        public async Task<IActionResult> Edit(IndividualDebtor Debtor, string returnUrl)
        {
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;

            if (ModelState.IsValid)
            {
                await IndividualDebtorService.Update(Debtor);
                await _serviceFactory.SaveAsync();
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }

            #region ViewData
            ViewData["MarialStatuses"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<MarialStatuses>(), "Key", "Value");
            ViewData["GenderTypes"] = new SelectList(EnumHelper.GetDescriptionsSrtingKeyValue<GenderTypes>(), "Key", "Value");
            ViewData["returnUrl"] = returnUrl;
            #endregion
            return View(Debtor);
        }


        public async Task<IActionResult> Delete(long id)
        {
            IndividualDebtorService = _serviceFactory.IndividualDebtorService;
            var individualDebtor = await IndividualDebtorService.GetById(id);
            await IndividualDebtorService.Remove(individualDebtor);
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
                IndividualDebtorService = _serviceFactory.IndividualDebtorService;
                var Debtor = await IndividualDebtorService.GetById(id);

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

                if (Debtor != null) { bankAccount.IndividualDebtor = Debtor; }

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

            return RedirectToAction("Details", new { id = bank.IndividualDebtorId });
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

                IndividualDebtorService = _serviceFactory.IndividualDebtorService;
                var Debtor = await IndividualDebtorService.GetById(bankAccount.IndividualDebtorId.Value);
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
                return RedirectToAction("Details", new { id = bankAccount.IndividualDebtorId });
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

            return View(bankAccount);
        }

        #endregion

        #region FamilyMembers

        public IActionResult AddFamilyMember(string returnUrl)
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
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFamilyMember(long id, FamilyMembersVM familyMembersVM, string returnUrl)
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
                    IndividualDebtorId = id,
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

            return RedirectToAction("Details", new { id = member.IndividualDebtorId });
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
                return RedirectToAction("Details", new { id = member.IndividualDebtorId });
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

        #region Job

        public IActionResult AddJob(string returnUrl)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddJob(long id, Job job, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                JobService = _serviceFactory.JobService;
                IndividualDebtorService = _serviceFactory.IndividualDebtorService;
                var Debtor = await IndividualDebtorService.GetById(id);

                Job newjob = new Job()
                {
                    Salary = job.Salary,
                    Adress = job.Adress,
                    CompanyName = job.CompanyName,
                    Experience = job.Experience,
                    Phone = job.Phone,
                    Position = job.Position,
                    VOEN = job.VOEN,
                    IndividualDebtorId = id,
                    CreateAt = DateTime.Now,
                    RecordStatus = RecordStatus.IsActive
                };


                if (Debtor != null) { newjob.IndividualDebtor = Debtor; }

                if (job.IsCurrent)
                {
                    foreach (var item in Debtor.Jobs)
                    { item.IsCurrent = false; }

                    await JobService.UpdateRange(Debtor.Jobs);
                    newjob.IsCurrent = true;
                }

                await JobService.AddNew(newjob);
                await _serviceFactory.SaveAsync();

                return RedirectToAction("Details", new { id });

            }

            return View(job);
        }


        public async Task<IActionResult> JobDetails(long Id)
        {
            JobService = _serviceFactory.JobService;
            var job = await JobService.GetById(Id);
            return View(job);
        }


        public async Task<IActionResult> DeleteJob(long Id)
        {
            JobService = _serviceFactory.JobService;
            var job = await JobService.GetById(Id);
            await JobService.Remove(job);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Details", new { id = job.IndividualDebtorId });
        }



        public async Task<IActionResult> EditJob(long Id, string returnUrl)
        {
            JobService = _serviceFactory.JobService;
            var job = await JobService.GetById(Id);
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(Job job, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                JobService = _serviceFactory.JobService;
                await JobService.Update(job);

                IndividualDebtorService = _serviceFactory.IndividualDebtorService;
                var Debtor = await IndividualDebtorService.GetById(job.IndividualDebtorId.Value);
                if (job.IsCurrent)
                {
                    foreach (var item in Debtor.Jobs)
                    {
                        if (item.Id != job.Id)
                        {
                            item.IsCurrent = false;
                            await JobService.Update(item);
                        }
                    }
                }

                await _serviceFactory.SaveAsync();
                return RedirectToAction("Details", new { id = job.IndividualDebtorId });
            }
            return View(job);
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
            int newCount = iCnt;
            #region ViewData
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

        public ActionResult AddButtonForJob(int JobCnt)
        {
            int newCount = JobCnt;
            ViewData["newCount"] = newCount;

            return PartialView("_AddJob");
        }
        public void Dispose()
        {
            ((IDisposable)_serviceFactory).Dispose();
        }
    }
}

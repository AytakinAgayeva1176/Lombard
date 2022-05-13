using Lombard.Domain.Contexts;
using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Lombard.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.Repositories
{
    public class EfRepository : IRepository
    {
        #region Ctor
        private readonly LombardContext _dbContext;
        private readonly ILogger<EfRepository> _logger;
        public EfRepository(LombardContext dbContext, ILogger<EfRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public EfRepository(LombardContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        public async Task<T> GetById<T>(long id) where T : BaseEntity
        {
            #region Get IndividualDebtor
            if (typeof(T) == typeof(IndividualDebtor))
            {
                var item = await _dbContext.IndividualDebtors
                    .Include(x => x.Acts)
                    .Include(x => x.FamilyMembers)
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Jobs)
                    .Include(x => x.CorporativeContracts)
                    .Include(x => x.IndividualContracts)
                    .SingleOrDefaultAsync(c => c.Id == id);



                var res = item as T;
                return res;
            }
            #endregion



            #region Get LegalDebtor
            if (typeof(T) == typeof(LegalDebtor))
            {
                var item = await _dbContext.LegalDebtors
                    .Include(x => x.Acts)
                    .Include(x => x.FamilyMembers)
                    .Include(x => x.BankAccounts)
                    .SingleOrDefaultAsync(c => c.Id == id);

                foreach (var account in item.BankAccounts)
                {
                    var bank = await _dbContext.Bank.SingleOrDefaultAsync(x => x.Id == account.BankId);
                    account.Bank = bank;
                }


                List<Act> finalList = new List<Act>();
                foreach (var act in item.Acts)
                {
                    if (act.ActType == ActTypes.Main)
                    {
                        var subActs = _dbContext.Acts.Where(i => i.ParentId == act.Id).ToList();

                        Act final = new Act()
                        {
                            ActType = ActTypes.Final,
                            TotalGoldLikvidPrice = act.TotalGoldLikvidPrice,
                            TotalGoldMarketPrice = act.TotalGoldMarketPrice,
                            TotalGoldNetWeight = act.TotalGoldNetWeight,
                            TotalGoldWeight = act.TotalGoldWeight,
                            TotalJewelsWeight = act.TotalJewelsWeight,
                            TotalGoldCount = act.TotalGoldCount
                        };

                        foreach (var sub in subActs)
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

                        finalList.Add(final);
                    }

                }
                Act Total = new Act()
                {
                    ActType = ActTypes.Final,
                    TotalGoldLikvidPrice = 0,
                    TotalGoldMarketPrice = 0,
                    TotalGoldNetWeight = 0,
                    TotalGoldWeight = 0,
                    TotalJewelsWeight = 0,
                    TotalGoldCount = 0
                };

                foreach (var final in finalList)
                {
                    Total.ActType = ActTypes.Final;
                    Total.TotalGoldLikvidPrice = final.TotalGoldLikvidPrice;
                    Total.TotalGoldMarketPrice = final.TotalGoldMarketPrice;
                    Total.TotalGoldNetWeight = final.TotalGoldNetWeight;
                    Total.TotalGoldWeight = final.TotalGoldWeight;
                    Total.TotalJewelsWeight = final.TotalJewelsWeight;
                    Total.TotalGoldCount = final.TotalGoldCount;
                }

                item.Acts.Add(Total);

                var res = item as T;
                return res;
            }
            #endregion

            #region Get Act
            if (typeof(T) == typeof(Act))
            {
                var item = await _dbContext.Acts
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.LegalDebtor)
                    .SingleOrDefaultAsync(c => c.Id == id);
                var res = item as T;
                return res;
            }
            #endregion

            #region Get Act With All Details
            if (typeof(T) == typeof(ActDetailsVM))
            {
                var item = await _dbContext.Acts.Include(x => x.Golds).Include(x => x.IndividualDebtor).Include(x => x.LegalDebtor).SingleOrDefaultAsync(c => c.Id == id);
                var subActs = _dbContext.Acts.Where(i => i.ParentId == item.Id).ToList();
                Act final = new Act()
                {
                    ActType = ActTypes.Final,
                    TotalGoldLikvidPrice = item.TotalGoldLikvidPrice,
                    TotalGoldMarketPrice = item.TotalGoldMarketPrice,
                    TotalGoldNetWeight = item.TotalGoldNetWeight,
                    TotalGoldWeight = item.TotalGoldWeight,
                    TotalJewelsWeight = item.TotalJewelsWeight,
                    TotalGoldCount = item.TotalGoldCount,
                    Golds = item.Golds
                };

                foreach (var act in subActs)
                {
                    if (act.ActType == ActTypes.Addition)
                    {
                        final.TotalGoldLikvidPrice += act.TotalGoldLikvidPrice;
                        final.TotalGoldMarketPrice += act.TotalGoldMarketPrice;
                        final.TotalGoldNetWeight += act.TotalGoldNetWeight;
                        final.TotalGoldWeight += act.TotalGoldWeight;
                        final.TotalJewelsWeight += act.TotalJewelsWeight;
                        final.TotalGoldCount += act.TotalGoldCount;
                    }
                    else if (act.ActType == ActTypes.Extractions)
                    {
                        final.TotalGoldLikvidPrice -= act.TotalGoldLikvidPrice;
                        final.TotalGoldMarketPrice -= act.TotalGoldMarketPrice;
                        final.TotalGoldNetWeight -= act.TotalGoldNetWeight;
                        final.TotalGoldWeight -= act.TotalGoldWeight;
                        final.TotalJewelsWeight -= act.TotalJewelsWeight;
                        final.TotalGoldCount -= act.TotalGoldCount;
                    }
                }

                subActs.Add(final);
                ActDetailsVM actDetails = new ActDetailsVM()
                {
                    Id = item.Id,
                    ActType = item.ActType,
                    Date = item.Date,
                    Golds = item.Golds,
                    FileName = item.FileName,
                    LegalDebtor = item.LegalDebtor,
                    IndividualDebtor = item.IndividualDebtor,
                    Name = item.Name,
                    TotalGoldLikvidPrice = item.TotalGoldLikvidPrice,
                    TotalGoldMarketPrice = item.TotalGoldMarketPrice,
                    TotalGoldNetWeight = item.TotalGoldNetWeight,
                    TotalGoldWeight = item.TotalGoldWeight,
                    TotalJewelsWeight = item.TotalJewelsWeight,
                    TotalGoldCount = item.TotalGoldCount,
                    UserId = item.UserId,
                    ActIndividualContracts = item.ActIndividualContracts,
                    Acts = subActs,
                    CreateAt = item.CreateAt,
                    ParentId = item.ParentId,
                    LegalDebtorId = item.LegalDebtorId,
                    IndividualDebtorId = item.IndividualDebtorId
                };


                foreach (var gold in item.Golds)
                {
                    var goldType = await _dbContext.GoldTypes.SingleOrDefaultAsync(x => x.Id == gold.GoldTypeId);
                    gold.GoldType = goldType;
                    var hallmark = await _dbContext.Hallmarks.SingleOrDefaultAsync(x => x.Id == gold.HallmarkId);
                    gold.Hallmark = hallmark;
                }

                if (item.IndividualDebtor != null)
                {
                    actDetails.IndividualDebtor.BankAccounts = _dbContext.BankAccount.Where(x => x.IndividualDebtorId == item.IndividualDebtorId).ToList();

                }
                else if (actDetails.LegalDebtor != null)
                {
                    item.LegalDebtor.BankAccounts = _dbContext.BankAccount.Where(x => x.LegalDebtorId == item.LegalDebtorId).ToList();

                }

                var res = actDetails as T;
                return res;
            }
            #endregion

            #region Get Gold
            if (typeof(T) == typeof(Gold))
            {
                var item = await _dbContext.Golds.SingleOrDefaultAsync(c => c.Id == id);

                var goldType = await _dbContext.GoldTypes.SingleOrDefaultAsync(x => x.Id == item.GoldTypeId);
                item.GoldType = goldType;
                var hallmark = await _dbContext.Hallmarks.SingleOrDefaultAsync(x => x.Id == item.HallmarkId);
                item.Hallmark = hallmark;

                var res = item as T;
                return res;
            }
            #endregion

            #region Get IndividualContract
            if (typeof(T) == typeof(IndividualContract))
            {
                var item = await _dbContext.IndividualContracts
                    .Include(x => x.LegalDebtor)
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.ActIndividualContracts)
                    .Include(x => x.GuaranterIndividualContract)
                    .SingleOrDefaultAsync(c => c.Id == id);

                var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == item.ProductId);
                item.Product = product;

                var res = item as T;
                return res;
            }

            #endregion

            #region Get Corporative Contract
            if (typeof(T) == typeof(CorporativeContract))
            {
                var item = await _dbContext.CorporativeContracts
                    .Include(x => x.LegalDebtor)
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.GuaranterContract)
                    .Include(x => x.PledgeContracts)
                    .SingleOrDefaultAsync(c => c.Id == id);

                var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == item.ProductId);
                item.Product = product;

                var res = item as T;
                return res;
            }

            #endregion

            #region Get Corporative Contract With All Details
            if (typeof(T) == typeof(CorporativeContractDetailsVM))
            {
                var item = await _dbContext.CorporativeContracts
                    .Include(x => x.PledgeContracts)
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.LegalDebtor)
                    .Include(x => x.Product)
                    .Include(x => x.Parent)
                    .Include(x => x.GuaranterContract)
                    .SingleOrDefaultAsync(c => c.Id == id);

                var additionalContracts = _dbContext.CorporativeContracts.Where(i => i.ParentId == item.Id).ToList();

                CorporativeContractDetailsVM contractDetails = new CorporativeContractDetailsVM()
                {
                    AdditionalContracts = additionalContracts,
                    Id = item.Id,
                    ParentId = item.ParentId,
                    DiscountedMonths = item.DiscountedMonths,
                    FIFD = item.FIFD,
                    PaymentDate = item.PaymentDate,
                    AccountNumber = item.AccountNumber,
                    Status = item.Status,
                    RecordStatus = item.RecordStatus,
                    Comission = item.Comission,
                    ContractNo = item.ContractNo,
                    CreditAmount = item.CreditAmount,
                    CreditLimit = item.CreditLimit,
                    Currency = item.Currency,
                    GivingTime = item.GivingTime,
                    GuaranterContract = item.GuaranterContract,
                    Percentage = item.Percentage,
                    Period = item.Period,
                    ProductId = item.ProductId,
                    CreateAt = item.CreateAt,
                    LegalDebtorId = item.LegalDebtorId,
                    IndividualDebtorId = item.IndividualDebtorId,
                    Product = item.Product,
                    LegalDebtor = item.LegalDebtor,
                    IndividualDebtor = item.IndividualDebtor,
                    UserId = item.UserId,
                    Parent = item.Parent

                };
                List<Pledge> pledges = new List<Pledge>();
                if (item.PledgeContracts.Count != 0)
                {
                    Act final = new Act();
                    foreach (var pledgeContract in item.PledgeContracts)
                    {
                        var contract = await _dbContext.PledgeContracts.Include(x => x.PledgeList).SingleOrDefaultAsync(x => x.Id == pledgeContract.Id);

                       
                        if (contract.ActType == ActTypes.Addition)
                        {
                            contractDetails.AddedPledgesContract.Add(contract);
                            foreach (var pledge in contract.PledgeList)
                            {

                                if (pledge.ExtractionContractId==0)
                                {
                                    pledges.Add(pledge);
                                }

                            }


                            final.TotalGoldCount += pledges.Count;
                        }
                        else if (contract.ActType == ActTypes.Extractions)
                        {
                            contractDetails.ExtractedPledgesContract.Add(contract);
                        }


                    }

                    ///////////////////////Final//////////////////////////////
                    
                   // final.PledgeList = pledges;
                    foreach (var addedPledge in contractDetails.AddedPledgesContract)
                    {
                        final.TotalGoldLikvidPrice += addedPledge.TotalGoldLikvidPrice;
                        final.TotalGoldMarketPrice += addedPledge.TotalGoldMarketPrice;
                        final.TotalGoldNetWeight += addedPledge.TotalGoldNetWeight;
                        final.TotalGoldWeight += addedPledge.TotalGoldWeight;
                    }
                    foreach (var extractedPledge in contractDetails.ExtractedPledgesContract)
                    {
                        final.TotalGoldLikvidPrice -= extractedPledge.TotalGoldLikvidPrice;
                        final.TotalGoldMarketPrice -= extractedPledge.TotalGoldMarketPrice;
                        final.TotalGoldNetWeight -= extractedPledge.TotalGoldNetWeight;
                        final.TotalGoldWeight -= extractedPledge.TotalGoldWeight;
                    }

                    contractDetails.FinalPledges = final;

                }


                if (item.IndividualDebtor != null)
                {
                    contractDetails.IndividualDebtor.BankAccounts =
                        _dbContext.BankAccount.Where(x => x.IndividualDebtorId == item.IndividualDebtorId).ToList();

                }
                else if (contractDetails.LegalDebtor != null)
                {
                    item.LegalDebtor.BankAccounts =
                        _dbContext.BankAccount.Where(x => x.LegalDebtorId == item.LegalDebtorId).ToList();

                }

                var res = contractDetails as T;
                return res;
            }
            #endregion

            #region Get Pledge Contract
            if (typeof(T) == typeof(PledgeContract))
            {

                var item = await _dbContext.PledgeContracts
                    .SingleOrDefaultAsync(c => c.Id == id);

                PledgeContract contract = new PledgeContract();
                if (item.ActType == ActTypes.Addition)
                {
                    item = await _dbContext.PledgeContracts
                  .Include(x => x.PledgeList)
                  .SingleOrDefaultAsync(c => c.Id == id);
                    contract = item;
                }

                contract = item;
                if (item.ActType == ActTypes.Extractions)
                {
                    var corporativeContract = await _dbContext.CorporativeContracts
                        .Include(x => x.PledgeContracts)
                        .SingleOrDefaultAsync(x => x.Id == item.CorporativeContractId);


                    List<Pledge> pledges = new List<Pledge>();

                    foreach (var pledgeContract in corporativeContract.PledgeContracts)
                    {
                        if (pledgeContract.ActType == ActTypes.Addition)
                        {
                            var pledgeContract1 = _dbContext.PledgeContracts.Include(x => x.PledgeList).SingleOrDefault(x => x.Id == pledgeContract.Id);
                            foreach (var pledge in pledgeContract1.PledgeList)
                            {

                                if (pledge.ExtractionContractId == contract.Id)
                                {
                                    pledges.Add(pledge);
                                }
                            }
                           // contract = item;
                            contract.PledgeList = pledges;

                        }
                    }

                }
                var res = contract as T;
                return res;
            }
            #endregion

            else
            {
                return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            }
        }




        public async Task<T> GetById<T>(long id, string include) where T : BaseEntity
        {

            return await _dbContext.Set<T>().Include(include).SingleOrDefaultAsync(e => e.Id == id);
        }



        public async Task<List<T>> List<T>(ISpecification<T> spec = null) where T : BaseEntity
        {

            #region GetAll IndividualDebtor
            if (typeof(T) == typeof(IndividualDebtor))
            {
                var items = _dbContext.IndividualDebtors.Include(x => x.FamilyMembers).Include(x => x.BankAccounts);
                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            #region GetAll LegalDebtor
            if (typeof(T) == typeof(LegalDebtor))
            {
                var items = _dbContext.LegalDebtors
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Acts)
                   .Include(x => x.FamilyMembers);

                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            #region GetAll Main Act
            if (typeof(T) == typeof(Act))
            {
                var items = _dbContext.Acts.Where(c => c.ActType == ActTypes.Main).Include(x => x.LegalDebtor).Include(x => x.IndividualDebtor);
                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            #region GetAll IndividualContracts
            if (typeof(T) == typeof(IndividualContract))
            {
                var items = _dbContext.IndividualContracts
                    .Include(x => x.Product)
                    .Include(x => x.LegalDebtor)
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.GuaranterIndividualContract)
                   .Include(x => x.ActIndividualContracts);

                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            #region GetAll Corporative Contracts
            if (typeof(T) == typeof(CorporativeContract))
            {
                var items = _dbContext.CorporativeContracts
                    .Include(x => x.Product)
                    .Include(x => x.LegalDebtor)
                    .Include(x => x.IndividualDebtor)
                    .Include(x => x.GuaranterContract)
                    .Where(x => x.ParentId == null);

                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            #region GetAll Pledge Contracts
            if (typeof(T) == typeof(PledgeContract))
            {
                var items = _dbContext.PledgeContracts
                    .Include(x => x.CorporativeContract)
                    .Include(x => x.PledgeList);

                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            #endregion

            var query = _dbContext.Set<T>().AsQueryable();
            if (spec != null)
            {
                query = query.Where(spec.Criteria);
            }
            return await query.ToListAsync();
        }


        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            #region Add Act
            if (typeof(T) == typeof(Act))
            {
                var item = entity as Act;

                await _dbContext.Acts.AddAsync(item);
                await _dbContext.SaveChangesAsync();

                if (item.Golds != null)
                {
                    foreach (var gold in item.Golds)
                    {
                        gold.CreateAt = DateTime.Now;
                        gold.RecordStatus = RecordStatus.IsActive;
                        gold.UserId = item.UserId;
                        gold.Act = item;
                    }
                    _dbContext.Golds.UpdateRange(item.Golds);
                }

                return entity;
            }
            #endregion

            #region Add IndividualDebtor
            if (typeof(T) == typeof(IndividualDebtor))
            {
                var item = entity as IndividualDebtor;
                _dbContext.IndividualDebtors.Add(item);

                #region Creating FamilyMembers

                if (item.FamilyMembers != null)
                {
                    foreach (var familyMember in item.FamilyMembers)
                    {
                        familyMember.IndividualDebtor = item;
                        familyMember.CreateAt = DateTime.Now;
                        familyMember.RecordStatus = RecordStatus.IsActive;
                        familyMember.UserId = item.UserId;
                    }

                    _dbContext.FamilyMembers.AddRange(item.FamilyMembers);
                }

                #endregion

                #region Creating Bank Account

                if (item.BankAccounts != null)
                {
                    foreach (var bankAccount in item.BankAccounts)
                    {
                        bankAccount.IndividualDebtor = item;
                        bankAccount.CreateAt = DateTime.Now;
                        bankAccount.RecordStatus = RecordStatus.IsActive;
                        bankAccount.UserId = item.UserId;
                        if (item.BankAccounts.Count == 1)
                        {
                            bankAccount.IsMainAccount = true;
                        }
                    }

                    _dbContext.BankAccount.AddRange(item.BankAccounts);
                }

                #endregion

                return entity;
            }
            #endregion

            #region Add LegalDebtor
            if (typeof(T) == typeof(LegalDebtor))
            {
                var item = entity as LegalDebtor;
                _dbContext.LegalDebtors.Add(item);

                #region Creating FamilyMembers

                if (item.FamilyMembers != null)
                {
                    foreach (var familyMember in item.FamilyMembers)
                    {
                        familyMember.LegalDebtor = item;
                        familyMember.CreateAt = DateTime.Now;
                        familyMember.RecordStatus = RecordStatus.IsActive;
                        familyMember.UserId = item.UserId;
                    }

                    _dbContext.FamilyMembers.AddRange(item.FamilyMembers);
                }

                #endregion
                #region Creating Bank Account

                if (item.BankAccounts != null)
                {
                    foreach (var bankAccount in item.BankAccounts)
                    {
                        bankAccount.LegalDebtor = item;
                        bankAccount.CreateAt = DateTime.Now;
                        bankAccount.RecordStatus = RecordStatus.IsActive;
                        bankAccount.UserId = item.UserId;
                        if (item.BankAccounts.Count == 1)
                        {
                            bankAccount.IsMainAccount = true;
                        }
                    }

                    _dbContext.BankAccount.AddRange(item.BankAccounts);
                }

                #endregion

                return entity;
            }
            #endregion

            #region Add Gold
            if (typeof(T) == typeof(Gold))
            {
                var item = entity as Gold;
                var act = await _dbContext.Acts.SingleOrDefaultAsync(x => x.Id == item.ActId);

                act.TotalGoldLikvidPrice += item.LikvidPrice;
                act.TotalGoldMarketPrice += item.MarketPrice;
                act.TotalGoldNetWeight += item.NetWeight;
                act.TotalGoldWeight += item.TotalWeight;
                act.TotalJewelsWeight += item.JewelWeight;

                item.Act = act;

                _dbContext.Set<T>().Add(entity);
                _dbContext.Acts.Update(act);
                return entity;
            }
            #endregion

            #region Add PledgeContract
            if (typeof(T) == typeof(PledgeContract))
            {
                var item = entity as PledgeContract;
                await _dbContext.PledgeContracts.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                var contract = item;
                if (item.ActType == ActTypes.Addition)
                {
                    foreach (var pledge in item.PledgeList)
                    {
                        pledge.CreateAt = DateTime.Now;
                        pledge.RecordStatus = RecordStatus.IsActive;
                        pledge.UserId = item.UserId;
                        pledge.PledgeContract = item;
                    }
                }

                _dbContext.Pledges.UpdateRange(item.PledgeList);



                return entity;
            }
            #endregion

            else
            {
                _dbContext.Set<T>().Add(entity);
                return entity;
            }

        }

        public async Task Delete<T>(T entity) where T : BaseEntity
        {
            #region Delete Act
            if (typeof(T) == typeof(Act))
            {
                var item = entity as Act;
                var contracts = _dbContext.ActIndividualContracts.Where(x => x.ActId == item.Id).ToList();
                if (contracts.Count == 0)
                {
                    var golds = _dbContext.Golds.Where(i => i.ActId == item.Id).ToList();
                    _dbContext.Golds.RemoveRange(golds);

                    if (item.ActType == ActTypes.Main)
                    {
                        var subActs = _dbContext.Acts.Where(i => i.ParentId == item.Id).ToList();
                        _dbContext.Acts.RemoveRange(subActs);
                    }

                    _dbContext.Acts.Remove(item);
                }

            }
            #endregion

            #region Delete IndividualDebtor
            if (typeof(T) == typeof(IndividualDebtor))
            {
                var item = entity as IndividualDebtor;
                var guaranters = _dbContext.GuaranterContracts.Where(x => x.IndividualDebtorId == item.Id).ToList();


                if (item.Acts.Count == 0
                    && guaranters.Count == 0
                    && item.CorporativeContracts.Count == 0)
                {
                    _dbContext.Jobs.RemoveRange(item.Jobs);
                    _dbContext.FamilyMembers.RemoveRange(item.FamilyMembers);
                    _dbContext.BankAccount.RemoveRange(item.BankAccounts);
                    _dbContext.IndividualDebtors.Remove(item);
                }
                else
                {
                    return;
                }

            }
            #endregion

            #region Delete LegalDebtor
            if (typeof(T) == typeof(LegalDebtor))
            {
                var item = entity as LegalDebtor;
                var guaranters = _dbContext.GuaranterContracts.Where(x => x.LegalDebtorId == item.Id).ToList();

                if (item.Acts.Count == 0
                    && guaranters.Count == 0
                    && item.CorporativeContracts.Count == 0)
                {
                    _dbContext.FamilyMembers.RemoveRange(item.FamilyMembers);
                    _dbContext.BankAccount.RemoveRange(item.BankAccounts);
                    _dbContext.LegalDebtors.Remove(item);
                }

                else return;
            }
            #endregion

            #region Delete Gold
            if (typeof(T) == typeof(Gold))
            {
                var item = entity as Gold;
                var act = await _dbContext.Acts.SingleOrDefaultAsync(x => x.Id == item.ActId);

                act.TotalGoldLikvidPrice = item.LikvidPrice;
                act.TotalGoldMarketPrice -= item.MarketPrice;
                act.TotalGoldNetWeight -= item.NetWeight;
                act.TotalGoldWeight -= item.TotalWeight;
                act.TotalJewelsWeight -= item.JewelWeight;

                item.Act = act;

                _dbContext.Set<T>().Remove(entity);
                _dbContext.Acts.Update(act);
            }
            #endregion

            #region Delete Individual Contract
            if (typeof(T) == typeof(IndividualContract))
            {
                var item = entity as IndividualContract;
                var actContracts = _dbContext.ActIndividualContracts.Where(x => x.IndividualContractId == item.Id).ToList();
                var guaranters =
                    _dbContext.GuaranterContracts.Where(x => x.IndividualContractId == item.Id).ToList();

                if (actContracts.Count != 0)
                {
                    _dbContext.ActIndividualContracts.RemoveRange(actContracts);
                }
                if (guaranters.Count != 0)
                {
                    _dbContext.GuaranterContracts.RemoveRange(guaranters);
                }

                _dbContext.IndividualContracts.Remove(item);
            }
            #endregion

            #region Delete Corporative Contract
            if (typeof(T) == typeof(CorporativeContract))
            {
                var item = entity as CorporativeContract;
                if (item.PledgeContracts.Count == 0)
                {
                    var guaranters =
                    _dbContext.GuaranterContracts.Where(x => x.CorporativeContractId == item.Id).ToList();

                    if (guaranters.Count == 0)
                    {
                        _dbContext.GuaranterContracts.RemoveRange(guaranters);
                    }

                    _dbContext.CorporativeContracts.Remove(item);
                }

                else return;
            }
            #endregion

            #region Delete PledgeContract
            if (typeof(T) == typeof(PledgeContract))
            {
                var item = entity as PledgeContract;
                var contracts = _dbContext.Pledges.Where(x => x.PledgeContractId == item.Id).ToList();

                _dbContext.Pledges.RemoveRange(item.PledgeList);
                _dbContext.PledgeContracts.Remove(item);

            }
            #endregion

            else
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public async Task Update<T>(T entity) where T : BaseEntity
        {
           

            //#region Update Pledge
            //if (typeof(T) == typeof(Pledge))
            //{
            //    var item = entity as Pledge;
            //    _dbContext.Set<T>().Update(entity);

            //    var pledgeContract = await _dbContext.PledgeContracts.Include(x => x.PledgeList).SingleOrDefaultAsync(x => x.Id == item.PledgeContractId);
            //    decimal totalWeight = 0;
            //    decimal likvidPrice = 0;
            //    decimal storePrice = 0;
            //    decimal netWeight = 0;
            //    foreach (var pledge in pledgeContract.PledgeList)
            //    {
            //        totalWeight += pledge.TotalWeight.Value;
            //        netWeight += pledge.NetWeight.Value;
            //        likvidPrice += pledge.LikvidPrice.Value;
            //        storePrice += pledge.StorePrice.Value;
            //    }

            //    pledgeContract.TotalGoldLikvidPrice = likvidPrice;
            //    pledgeContract.TotalGoldMarketPrice = storePrice;
            //    pledgeContract.TotalGoldNetWeight = netWeight;
            //    pledgeContract.TotalGoldWeight = totalWeight;

            //    _dbContext.PledgeContracts.Update(pledgeContract);
            //}
            //#endregion

             _dbContext.Entry(entity).State = EntityState.Modified;

           
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
        }

        public async Task DeleteRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            foreach (var item in entities)
            {
                _dbContext.Set<T>().Remove(item);
            }

        }

        public async Task SaveChange()
        {
            await _dbContext.SaveChangesAsync();
        }


        public async Task AddRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            foreach (var item in entities)
            {
                await _dbContext.Set<T>().AddAsync(item);
            }
        }


    }
}

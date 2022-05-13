using Lombard.Repositories;
using System;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public interface IServiceFactory
    {
        public IndividualDebtorService IndividualDebtorService { get; }
        public LegalDebtorService LegalDebtorService { get; }
        public FamilyMembersService FamilyMembersService { get; }
        public BankAccountService BankAccountService { get; }
        public BusinessAreaService BusinessAreaService { get; }
        public BankService BankService { get; }
        public GoldService GoldService { get; }
        public GoldTypeService GoldTypeService { get; }
        public ActService ActService { get; }
        public HallmarkService HallmarkService { get; }
        public IndividualContractService IndividualContractService { get; }
        public ActIndividualContractService ActIndividualContractService { get; }
        public GuaranterIndividualContractService GuaranterIndividualContractService { get; }
        public ProductService ProductService { get; }
        public CorporativeContractService CorporativeContractService { get; }
        public GuaranterContractService GuaranterContractService { get; }
        public PledgeContractService PledgeContractService { get; }
        public PledgeService PledgeService { get; }
        public RelationTypeService RelationTypeService { get; }
        public JobService JobService { get; }
        Task<int> SaveAsync();
    }

    public class ServiceFactory : IServiceFactory, IDisposable
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ServiceFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }


        private IndividualDebtorService _IndividualDebtorService;
        public IndividualDebtorService IndividualDebtorService
        {
            get
            {
                this._IndividualDebtorService ??= new IndividualDebtorService(_repositoryFactory);
                return _IndividualDebtorService;
            }
        }


        private LegalDebtorService _LegalDebtorService;
        public LegalDebtorService LegalDebtorService
        {
            get
            {
                this._LegalDebtorService ??= new LegalDebtorService(_repositoryFactory);
                return _LegalDebtorService;
            }
        }


        private FamilyMembersService _familyMembersService;
        public FamilyMembersService FamilyMembersService
        {
            get
            {
                this._familyMembersService ??= new FamilyMembersService(_repositoryFactory);
                return _familyMembersService;
            }
        }


        private BankAccountService _bankAccountService;
        public BankAccountService BankAccountService
        {
            get
            {
                this._bankAccountService ??= new BankAccountService(_repositoryFactory);
                return _bankAccountService;
            }
        }


        private BusinessAreaService _businessAreaService;
        public BusinessAreaService BusinessAreaService
        {
            get
            {
                this._businessAreaService ??= new BusinessAreaService(_repositoryFactory);
                return _businessAreaService;
            }
        }


        private BankService _bankService;
        public BankService BankService
        {
            get
            {
                this._bankService ??= new BankService(_repositoryFactory);
                return _bankService;
            }
        }


        private HallmarkService _hallmarkService;
        public HallmarkService HallmarkService
        {
            get
            {
                this._hallmarkService ??= new HallmarkService(_repositoryFactory);
                return _hallmarkService;
            }
        }


        private ActService _actService;
        public ActService ActService
        {
            get
            {
                this._actService ??= new ActService(_repositoryFactory);
                return _actService;
            }
        }


        private GoldService _goldService;
        public GoldService GoldService
        {
            get
            {
                this._goldService ??= new GoldService(_repositoryFactory);
                return _goldService;
            }
        }


        private GoldTypeService _goldTypeService;
        public GoldTypeService GoldTypeService
        {
            get
            {
                this._goldTypeService ??= new GoldTypeService(_repositoryFactory);
                return _goldTypeService;
            }
        }


        private IndividualContractService _individualContractService;
        public IndividualContractService IndividualContractService
        {
            get
            {
                this._individualContractService ??= new IndividualContractService(_repositoryFactory);
                return _individualContractService;
            }
        }


        private ActIndividualContractService _actindividualContractService;
        public ActIndividualContractService ActIndividualContractService
        {
            get
            {
                this._actindividualContractService ??= new ActIndividualContractService(_repositoryFactory);
                return _actindividualContractService;
            }
        }


        private GuaranterContractService _GuaranterContractService;
        public GuaranterContractService GuaranterContractService
        {
            get
            {
                this._GuaranterContractService ??= new GuaranterContractService(_repositoryFactory);
                return _GuaranterContractService;
            }
        }


        private GuaranterIndividualContractService _guaranterIndividualContractService;
        public GuaranterIndividualContractService GuaranterIndividualContractService
        {
            get
            {
                this._guaranterIndividualContractService ??= new GuaranterIndividualContractService(_repositoryFactory);
                return _guaranterIndividualContractService;
            }
        }


        private ProductService _productService;
        public ProductService ProductService
        {
            get
            {
                this._productService ??= new ProductService(_repositoryFactory);
                return _productService;
            }
        }


        private CorporativeContractService _corporativeContractService;
        public CorporativeContractService CorporativeContractService
        {
            get
            {
                this._corporativeContractService ??= new CorporativeContractService(_repositoryFactory);
                return _corporativeContractService;
            }
        }


        private PledgeContractService _pledgeContractService;
        public PledgeContractService PledgeContractService
        {
            get
            {
                this._pledgeContractService ??= new PledgeContractService(_repositoryFactory);
                return _pledgeContractService;
            }
        }


        private PledgeService _pledgeService;
        public PledgeService PledgeService
        {
            get
            {
                this._pledgeService ??= new PledgeService(_repositoryFactory);
                return _pledgeService;
            }
        }



        private RelationTypeService _relationTypeService;
        public RelationTypeService RelationTypeService
        {
            get
            {
                this._relationTypeService ??= new RelationTypeService(_repositoryFactory);
                return _relationTypeService;
            }
        }



        private JobService _jobService;
        public JobService JobService
        {
            get
            {
                this._jobService ??= new JobService(_repositoryFactory);
                return _jobService;
            }
        }




        #region SaveChange
        public async Task<int> SaveAsync()
        {
            return await _repositoryFactory.SaveAsync();
        }
        #endregion

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ((IDisposable)_repositoryFactory).Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

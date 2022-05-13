using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lombard.Domain.Contexts
{
    public class LombardContext : DbContext
    {
        public LombardContext()
        {

        }
        public LombardContext(DbContextOptions<LombardContext> options) : base(options)
        {
    //        modelBuilder.Query<SpecialProductResult>()
    //.Property(o => o.GoldPercent)
    //.HasColumnType("decimal(18,4)");
        }


        public virtual DbSet<RelationType> RelationTypes { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BusinessArea> BusinessArea { get; set; }
        public virtual DbSet<IndividualDebtor> IndividualDebtors { get; set; }
        public virtual DbSet<LegalDebtor> LegalDebtors { get; set; }
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Hallmark> Hallmarks { get; set; }
        public virtual DbSet<GoldType> GoldTypes { get; set; }
        public virtual DbSet<Act> Acts { get; set; }
        public virtual DbSet<Gold> Golds { get; set; }
        public virtual DbSet<IndividualContract> IndividualContracts { get; set; }
        public virtual DbSet<ActIndividualContract> ActIndividualContracts { get; set; }
        public virtual DbSet<ProductGroup> Groups { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<CorporativeContract> CorporativeContracts { get; set; }
        public virtual DbSet<PledgeContract> PledgeContracts { get; set; }
        public virtual DbSet<Pledge> Pledges { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<RefusalReason> RefusalReasons { get; set; }
        public virtual DbSet<CreditStatus> CreditStatuses { get; set; }
        public virtual DbSet<PaymentSchedule> PaymentSchedule { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Bail> Bails { get; set; }

        public virtual DbSet<GuaranterContract> GuaranterContracts { get; set; }

    }
    
}

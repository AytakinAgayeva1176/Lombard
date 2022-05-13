using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lombard.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    ForPrint = table.Column<string>(nullable: true),
                    VOEN = table.Column<string>(nullable: true),
                    CorrespondentAccount = table.Column<string>(nullable: true),
                    SWIFT = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    OtherDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessArea",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Area = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoldTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsFiveWorkdaysSchedule = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hallmarks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    TypeName = table.Column<string>(nullable: true),
                    LikvidPriceOfOneGram = table.Column<decimal>(nullable: false),
                    MarketPriceOfOneGram = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hallmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualDebtors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsResident = table.Column<string>(nullable: true),
                    RegistrationStatus = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CurrentLivingAddress = table.Column<string>(nullable: true),
                    RegisteredAddress = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    MobileAdditional1 = table.Column<string>(nullable: true),
                    MobileAdditional2 = table.Column<string>(nullable: true),
                    MobileAdditional3 = table.Column<string>(nullable: true),
                    MobileAdditional4 = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneAdditional = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    VOEN = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsOwner = table.Column<bool>(nullable: false),
                    Surname = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BirthPlace = table.Column<string>(nullable: true),
                    IDFincode = table.Column<string>(nullable: true),
                    IDSerialNumber = table.Column<string>(nullable: true),
                    IDOrganizationName = table.Column<string>(nullable: true),
                    Citizenship = table.Column<string>(nullable: true),
                    IDExpireDate = table.Column<DateTime>(nullable: false),
                    IDGiveDate = table.Column<DateTime>(nullable: true),
                    MarialStatuses = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualDebtors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalDebtors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsResident = table.Column<string>(nullable: true),
                    RegistrationStatus = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CurrentLivingAddress = table.Column<string>(nullable: true),
                    RegisteredAddress = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    MobileAdditional1 = table.Column<string>(nullable: true),
                    MobileAdditional2 = table.Column<string>(nullable: true),
                    MobileAdditional3 = table.Column<string>(nullable: true),
                    MobileAdditional4 = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneAdditional = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    VOEN = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HeadAccountant = table.Column<string>(nullable: true),
                    DirectorName = table.Column<string>(nullable: true),
                    BusinessArea = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalDebtors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefusalReasons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefusalReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    GroupId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LocalCardAvailability = table.Column<bool>(nullable: false),
                    PenaltyPercentage = table.Column<decimal>(nullable: true),
                    ProductPurpose = table.Column<string>(nullable: true),
                    LoanAssignment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    VOEN = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: true),
                    Experience = table.Column<decimal>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false),
                    IndividualDebtorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    ActType = table.Column<int>(nullable: false),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    TotalGoldWeight = table.Column<decimal>(nullable: false),
                    TotalJewelsWeight = table.Column<decimal>(nullable: false),
                    TotalGoldNetWeight = table.Column<decimal>(nullable: false),
                    TotalGoldLikvidPrice = table.Column<decimal>(nullable: false),
                    TotalGoldMarketPrice = table.Column<decimal>(nullable: false),
                    TotalGoldCount = table.Column<decimal>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acts_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acts_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acts_Acts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Acts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    IsMainAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccount_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true),
                    WorkPosition = table.Column<string>(nullable: true),
                    Salary = table.Column<double>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    MobileAdditional = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    RelationTypeId = table.Column<long>(nullable: false),
                    RelationType = table.Column<string>(nullable: true),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CorporativeContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Guarantors = table.Column<string>(nullable: true),
                    SignedBy = table.Column<string>(nullable: true),
                    BackOfficeApprover = table.Column<string>(nullable: true),
                    BackOfficeConfirmedDate = table.Column<DateTime>(nullable: false),
                    DecisionMaker = table.Column<string>(nullable: true),
                    DecisionDate = table.Column<DateTime>(nullable: false),
                    ContractNo = table.Column<string>(nullable: true),
                    RefusalReason = table.Column<string>(nullable: true),
                    Expert = table.Column<string>(nullable: true),
                    CalculationType = table.Column<string>(nullable: true),
                    SourceId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ContractTime = table.Column<DateTime>(nullable: false),
                    LoanPeriod = table.Column<int>(nullable: false),
                    LoanExpireDate = table.Column<DateTime>(nullable: false),
                    LoanEffectiveDate = table.Column<DateTime>(nullable: false),
                    IsComissionFull = table.Column<bool>(nullable: false),
                    Comission = table.Column<decimal>(nullable: false),
                    ComissionAmount = table.Column<decimal>(nullable: false),
                    AnnuityAmount = table.Column<decimal>(nullable: false),
                    AnnualPercentage = table.Column<decimal>(nullable: false),
                    DiscountedMonths = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Percentage = table.Column<decimal>(nullable: true),
                    CreditAmount = table.Column<decimal>(nullable: false),
                    GivingTime = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    FIFD = table.Column<decimal>(nullable: false),
                    CreditLimit = table.Column<double>(nullable: false),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporativeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorporativeContracts_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorporativeContracts_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorporativeContracts_CorporativeContracts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CorporativeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorporativeContracts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporativeContracts_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Guarantors = table.Column<string>(nullable: true),
                    SignedBy = table.Column<string>(nullable: true),
                    BackOfficeApprover = table.Column<string>(nullable: true),
                    BackOfficeConfirmedDate = table.Column<DateTime>(nullable: false),
                    DecisionMaker = table.Column<string>(nullable: true),
                    DecisionDate = table.Column<DateTime>(nullable: false),
                    ContractNo = table.Column<string>(nullable: true),
                    RefusalReason = table.Column<string>(nullable: true),
                    Expert = table.Column<string>(nullable: true),
                    CalculationType = table.Column<string>(nullable: true),
                    SourceId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ContractTime = table.Column<DateTime>(nullable: false),
                    LoanPeriod = table.Column<int>(nullable: false),
                    LoanExpireDate = table.Column<DateTime>(nullable: false),
                    LoanEffectiveDate = table.Column<DateTime>(nullable: false),
                    IsComissionFull = table.Column<bool>(nullable: false),
                    Comission = table.Column<decimal>(nullable: false),
                    ComissionAmount = table.Column<decimal>(nullable: false),
                    AnnuityAmount = table.Column<decimal>(nullable: false),
                    AnnualPercentage = table.Column<decimal>(nullable: false),
                    DiscountedMonths = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Percentage = table.Column<decimal>(nullable: true),
                    CreditAmount = table.Column<decimal>(nullable: false),
                    GivingTime = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    FIFD = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualContracts_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualContracts_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualContracts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualContracts_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Golds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    ActId = table.Column<long>(nullable: false),
                    GoldTypeId = table.Column<long>(nullable: false),
                    HallmarkId = table.Column<long>(nullable: false),
                    OneGramLikvidPrice = table.Column<decimal>(nullable: false),
                    OneGramStorePrice = table.Column<decimal>(nullable: false),
                    ItemsCount = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    JewelWeight = table.Column<decimal>(nullable: false),
                    NetWeight = table.Column<decimal>(nullable: false),
                    LikvidPrice = table.Column<decimal>(nullable: false),
                    MarketPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Golds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Golds_Acts_ActId",
                        column: x => x.ActId,
                        principalTable: "Acts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Golds_GoldTypes_GoldTypeId",
                        column: x => x.GoldTypeId,
                        principalTable: "GoldTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Golds_Hallmarks_HallmarkId",
                        column: x => x.HallmarkId,
                        principalTable: "Hallmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PledgeContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    ActType = table.Column<int>(nullable: false),
                    CorporativeContractId = table.Column<long>(nullable: false),
                    ContractNumber = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TotalGoldWeight = table.Column<decimal>(nullable: false),
                    TotalGoldNetWeight = table.Column<decimal>(nullable: false),
                    TotalGoldLikvidPrice = table.Column<decimal>(nullable: false),
                    TotalGoldMarketPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PledgeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PledgeContracts_CorporativeContracts_CorporativeContractId",
                        column: x => x.CorporativeContractId,
                        principalTable: "CorporativeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActIndividualContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    ActId = table.Column<long>(nullable: false),
                    IndividualContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActIndividualContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActIndividualContracts_Acts_ActId",
                        column: x => x.ActId,
                        principalTable: "Acts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActIndividualContracts_IndividualContracts_IndividualContractId",
                        column: x => x.IndividualContractId,
                        principalTable: "IndividualContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CorporativeContractID = table.Column<long>(nullable: true),
                    IndividualContractID = table.Column<long>(nullable: true),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EvaluationDate = table.Column<DateTime>(nullable: false),
                    Evaluator = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bails_CorporativeContracts_CorporativeContractID",
                        column: x => x.CorporativeContractID,
                        principalTable: "CorporativeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bails_IndividualContracts_IndividualContractID",
                        column: x => x.IndividualContractID,
                        principalTable: "IndividualContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bails_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bails_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuaranterContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    IndividualDebtorId = table.Column<long>(nullable: true),
                    LegalDebtorId = table.Column<long>(nullable: true),
                    IndividualContractId = table.Column<long>(nullable: true),
                    CorporativeContractId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranterContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuaranterContracts_CorporativeContracts_CorporativeContractId",
                        column: x => x.CorporativeContractId,
                        principalTable: "CorporativeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuaranterContracts_IndividualContracts_IndividualContractId",
                        column: x => x.IndividualContractId,
                        principalTable: "IndividualContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuaranterContracts_IndividualDebtors_IndividualDebtorId",
                        column: x => x.IndividualDebtorId,
                        principalTable: "IndividualDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuaranterContracts_LegalDebtors_LegalDebtorId",
                        column: x => x.LegalDebtorId,
                        principalTable: "LegalDebtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CorporativeContractID = table.Column<long>(nullable: true),
                    IndividualContractID = table.Column<long>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    TotalPayment = table.Column<decimal>(nullable: false),
                    InitialRemainder = table.Column<decimal>(nullable: false),
                    LastRemainder = table.Column<decimal>(nullable: false),
                    MainDebtPayment = table.Column<decimal>(nullable: false),
                    PercentageDebtPayment = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSchedule_CorporativeContracts_CorporativeContractID",
                        column: x => x.CorporativeContractID,
                        principalTable: "CorporativeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentSchedule_IndividualContracts_IndividualContractID",
                        column: x => x.IndividualContractID,
                        principalTable: "IndividualContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pledges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    ClientFullName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    PackageNumber = table.Column<string>(nullable: true),
                    TotalWeight_375 = table.Column<decimal>(nullable: true),
                    NetWeight_375 = table.Column<decimal>(nullable: true),
                    TotalWeight_500 = table.Column<decimal>(nullable: true),
                    NetWeight_500 = table.Column<decimal>(nullable: true),
                    TotalWeight583_585 = table.Column<decimal>(nullable: true),
                    NetWeight583_585 = table.Column<decimal>(nullable: true),
                    TotalWeight_750 = table.Column<decimal>(nullable: true),
                    NetWeight_750 = table.Column<decimal>(nullable: true),
                    TotalWeight_875 = table.Column<decimal>(nullable: true),
                    NetWeight_875 = table.Column<decimal>(nullable: true),
                    TotalWeight_916 = table.Column<decimal>(nullable: true),
                    NetWeight_916 = table.Column<decimal>(nullable: true),
                    TotalWeight_999 = table.Column<decimal>(nullable: true),
                    NetWeight_999 = table.Column<decimal>(nullable: true),
                    TotalWeight = table.Column<decimal>(nullable: true),
                    NetWeight = table.Column<decimal>(nullable: true),
                    LikvidPrice = table.Column<decimal>(nullable: true),
                    StorePrice = table.Column<decimal>(nullable: true),
                    AdditionalPledgeContractNumber = table.Column<string>(nullable: true),
                    PledgeContractId = table.Column<long>(nullable: false),
                    ExtractionContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pledges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pledges_PledgeContracts_PledgeContractId",
                        column: x => x.PledgeContractId,
                        principalTable: "PledgeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActIndividualContracts_ActId",
                table: "ActIndividualContracts",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_ActIndividualContracts_IndividualContractId",
                table: "ActIndividualContracts",
                column: "IndividualContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Acts_IndividualDebtorId",
                table: "Acts",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Acts_LegalDebtorId",
                table: "Acts",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Acts_ParentId",
                table: "Acts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bails_CorporativeContractID",
                table: "Bails",
                column: "CorporativeContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Bails_IndividualContractID",
                table: "Bails",
                column: "IndividualContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Bails_IndividualDebtorId",
                table: "Bails",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bails_LegalDebtorId",
                table: "Bails",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankId",
                table: "BankAccount",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_IndividualDebtorId",
                table: "BankAccount",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_LegalDebtorId",
                table: "BankAccount",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporativeContracts_IndividualDebtorId",
                table: "CorporativeContracts",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporativeContracts_LegalDebtorId",
                table: "CorporativeContracts",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporativeContracts_ParentId",
                table: "CorporativeContracts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporativeContracts_ProductId",
                table: "CorporativeContracts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporativeContracts_SourceId",
                table: "CorporativeContracts",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_IndividualDebtorId",
                table: "FamilyMembers",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_LegalDebtorId",
                table: "FamilyMembers",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Golds_ActId",
                table: "Golds",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_Golds_GoldTypeId",
                table: "Golds",
                column: "GoldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Golds_HallmarkId",
                table: "Golds",
                column: "HallmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_GuaranterContracts_CorporativeContractId",
                table: "GuaranterContracts",
                column: "CorporativeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_GuaranterContracts_IndividualContractId",
                table: "GuaranterContracts",
                column: "IndividualContractId");

            migrationBuilder.CreateIndex(
                name: "IX_GuaranterContracts_IndividualDebtorId",
                table: "GuaranterContracts",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_GuaranterContracts_LegalDebtorId",
                table: "GuaranterContracts",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualContracts_IndividualDebtorId",
                table: "IndividualContracts",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualContracts_LegalDebtorId",
                table: "IndividualContracts",
                column: "LegalDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualContracts_ProductId",
                table: "IndividualContracts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualContracts_SourceId",
                table: "IndividualContracts",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IndividualDebtorId",
                table: "Jobs",
                column: "IndividualDebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_CorporativeContractID",
                table: "PaymentSchedule",
                column: "CorporativeContractID",
                unique: true,
                filter: "[CorporativeContractID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_IndividualContractID",
                table: "PaymentSchedule",
                column: "IndividualContractID",
                unique: true,
                filter: "[IndividualContractID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PledgeContracts_CorporativeContractId",
                table: "PledgeContracts",
                column: "CorporativeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_PledgeContractId",
                table: "Pledges",
                column: "PledgeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GroupId",
                table: "Products",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActIndividualContracts");

            migrationBuilder.DropTable(
                name: "Bails");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BusinessArea");

            migrationBuilder.DropTable(
                name: "CreditStatuses");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "Golds");

            migrationBuilder.DropTable(
                name: "GuaranterContracts");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "PaymentSchedule");

            migrationBuilder.DropTable(
                name: "Pledges");

            migrationBuilder.DropTable(
                name: "RefusalReasons");

            migrationBuilder.DropTable(
                name: "RelationTypes");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Acts");

            migrationBuilder.DropTable(
                name: "GoldTypes");

            migrationBuilder.DropTable(
                name: "Hallmarks");

            migrationBuilder.DropTable(
                name: "IndividualContracts");

            migrationBuilder.DropTable(
                name: "PledgeContracts");

            migrationBuilder.DropTable(
                name: "CorporativeContracts");

            migrationBuilder.DropTable(
                name: "IndividualDebtors");

            migrationBuilder.DropTable(
                name: "LegalDebtors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}

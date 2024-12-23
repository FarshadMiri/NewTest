using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data
{
    public class TestWithValueDbContext : IdentityDbContext
    {
        public TestWithValueDbContext(DbContextOptions<TestWithValueDbContext> options)
            : base(options)
        {

        }
        public DbSet<Tbl_Answer> tbl_Answers { get; set; }
        public DbSet<Tbl_Option> tbl_Options { get; set; }

        public DbSet<Tbl_Question> tbl_Questions { get; set; }
        public DbSet<Tbl_Test> tbl_Tests { get; set; }
        public DbSet<Tbl_Topic> tbl_Topics { get; set; }
        public DbSet<Tbl_User> tbl_Users { get; set; }
        public DbSet<Tbl_CartItem> tbl_CartItems { get; set; }
        public DbSet<Tbl_TicketMessage> tbl_TicketMessages { get; set; }
        public DbSet<Tbl_TaskMessage>  tbl_TaskMessages { get; set; }

        public DbSet<Tbl_Ticket>  tbl_Tickets  { get; set; }
        public DbSet<Tbl_TicketStatus> tbl_TicketStatus { get; set; }
		public DbSet<Tbl_Province> tbl_Provinces { get; set; }
        public DbSet<Tbl_City> tbl_Cities { get; set; }
        public DbSet<Tbl_UserInfo> tbl_UserInfos { get; set; }
        public DbSet<Tbl_Organization> tbl_Organizations { get; set; }
        public DbSet<Tbl_ReportInfo>  tbl_ReportInfos { get; set; }
        public DbSet<Tbl_Request> tbl_Requests { get; set; }
        public DbSet<Tbl_Task> tbl_Tasks { get; set; }
        public DbSet<Tbl_Case>  tbl_Cases { get; set; }
        public DbSet<Tbl_Location> tbl_Locations { get; set; }
        public DbSet<Tbl_SuggestedCase> tbl_SuggestedCases { get; set; }
        public DbSet<Tbl_Contract> tbl_Contracts { get; set; }
        public DbSet<Tbl_PartyContract>  tbl_PartyContracts { get; set; }
        public DbSet<Tbl_ContractClause>  tbl_ContractClauses { get; set; }
        public DbSet<Tbl_ContractClauseMapping> tbl_ContractClauseMappings { get; set; }
        public DbSet<Tbl_ContractTitle>  tbl_ContractTitles { get; set; }













        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_Test>().HasKey(t => t.TestId);
            modelBuilder.Entity<Tbl_Location>().HasData(
new Tbl_Location { LocationId = 1, Name = "شعبه دادگاه تهران" },
new Tbl_Location { LocationId = 2, Name = "شعبه دادگاه مشهد" },
new Tbl_Location { LocationId = 3, Name = "شعبه دادگاه اصفهان" },
new Tbl_Location { LocationId = 4, Name = "شعبه دادگاه شیراز" },
new Tbl_Location { LocationId = 5, Name = "شعبه دادگاه تبریز" },
new Tbl_Location { LocationId = 6, Name = "شعبه دادگاه کرج" },
new Tbl_Location { LocationId = 7, Name = "شعبه دادگاه اهواز" },
new Tbl_Location { LocationId = 8, Name = "شعبه دادگاه قم" },
new Tbl_Location { LocationId = 9, Name = "شعبه دادگاه رشت" },
new Tbl_Location { LocationId = 10, Name = "شعبه دادگاه یزد" }
);
            modelBuilder.Entity<Tbl_ContractClause>().HasData(
       new Tbl_ContractClause { ClauseId = 1, ClauseText = "بند 1: این قرارداد تحت قوانین جمهوری اسلامی ایران تنظیم شده است." },
       new Tbl_ContractClause { ClauseId = 2, ClauseText = "بند 2: طرفین موظف به حفظ اطلاعات محرمانه هستند." },
       new Tbl_ContractClause { ClauseId = 3, ClauseText = "بند 3: مدت قرارداد از تاریخ امضا به مدت یک سال معتبر است." },
       new Tbl_ContractClause { ClauseId = 4, ClauseText = "بند 4: کلیه اختلافات از طریق داوری حل می‌شود." },
       new Tbl_ContractClause { ClauseId = 5, ClauseText = "بند 5: تمامی پرداخت‌ها به صورت ریالی انجام خواهد شد." },
       new Tbl_ContractClause { ClauseId = 6, ClauseText = "بند 6: در صورت بروز اختلاف، حکمیت به وکیل تعیین شده واگذار می‌شود." },
       new Tbl_ContractClause { ClauseId = 7, ClauseText = "بند 7: هر گونه تخلف منجر به فسخ قرارداد خواهد شد." },
       new Tbl_ContractClause { ClauseId = 8, ClauseText = "بند 8: طرفین توافق دارند از امکانات سامانه الکترونیکی استفاده کنند." },
       new Tbl_ContractClause { ClauseId = 9, ClauseText = "بند 9: حق تغییر در بندهای قرارداد با توافق طرفین امکان‌پذیر است." },
       new Tbl_ContractClause { ClauseId = 10, ClauseText = "بند 10: جریمه تخلف معادل 10 درصد ارزش قرارداد خواهد بود." },
       new Tbl_ContractClause { ClauseId = 11, ClauseText = "بند 11: قرارداد فقط با امضای هر دو طرف معتبر است." },
       new Tbl_ContractClause { ClauseId = 12, ClauseText = "بند 12: خدمات پشتیبانی شامل هزینه جداگانه خواهد بود." },
       new Tbl_ContractClause { ClauseId = 13, ClauseText = "بند 13: قرارداد باید در بازه زمانی توافق شده اجرا شود." },
       new Tbl_ContractClause { ClauseId = 14, ClauseText = "بند 14: اطلاعات طرفین باید به صورت دقیق در سامانه وارد شود." },
       new Tbl_ContractClause { ClauseId = 15, ClauseText = "بند 15: تعهدات مالی باید ظرف مدت 30 روز تسویه شود." },
       new Tbl_ContractClause { ClauseId = 16, ClauseText = "بند 16: طرفین متعهد به رعایت الزامات محیط زیستی هستند." },
       new Tbl_ContractClause { ClauseId = 17, ClauseText = "بند 17: تغییرات در قرارداد باید مکتوب و امضا شود." },
       new Tbl_ContractClause { ClauseId = 18, ClauseText = "بند 18: قرارداد شامل مالیات بر ارزش افزوده می‌باشد." },
       new Tbl_ContractClause { ClauseId = 19, ClauseText = "بند 19: در صورت بروز قوه قهریه، قرارداد به تعلیق در می‌آید." },
       new Tbl_ContractClause { ClauseId = 20, ClauseText = "بند 20: هر گونه استفاده تجاری بدون مجوز ممنوع است." }
   );
            modelBuilder.Entity<Tbl_ContractTitle>().HasData(
    new Tbl_ContractTitle { TitleId = 1, TitleName = "قرارداد خدمات" },
    new Tbl_ContractTitle { TitleId = 2, TitleName = "قرارداد عدم افشاء (NDA)" },
    new Tbl_ContractTitle { TitleId = 3, TitleName = "قرارداد محرمانگی" },
    new Tbl_ContractTitle { TitleId = 4, TitleName = "قرارداد مشارکت" },
    new Tbl_ContractTitle { TitleId = 5, TitleName = "قرارداد استخدام" },
    new Tbl_ContractTitle { TitleId = 6, TitleName = "قرارداد اجاره" },
    new Tbl_ContractTitle { TitleId = 7, TitleName = "قرارداد فروش" },
    new Tbl_ContractTitle { TitleId = 8, TitleName = "قرارداد وام" },
    new Tbl_ContractTitle { TitleId = 9, TitleName = "قرارداد تسویه" },
    new Tbl_ContractTitle { TitleId = 10, TitleName = "قرارداد مجوز" }
);

            modelBuilder.Entity<Tbl_Answer>()
                .HasKey(a => a.AnswerId);

            modelBuilder.Entity<Tbl_Answer>()
             .HasOne(a => a.Question)
           .WithOne(o => o.Answer)
            .HasForeignKey<Tbl_Answer>(a => a.QuestionId)  // کلید خارجی به Tbl_Answer اشاره می‌کند
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_ContractClauseMapping>()
       .HasKey(mapping => new { mapping.ContractId, mapping.ClauseId });

            // رابطه بین Tbl_PartyContract و Tbl_ContractClauseMapping
            modelBuilder.Entity<Tbl_ContractClauseMapping>()
                .HasOne(mapping => mapping.Contract)
                .WithMany(contract => contract.ContractClauseMappings)
                .HasForeignKey(mapping => mapping.ContractId);

            // رابطه بین Tbl_ContractClause و Tbl_ContractClauseMapping
            modelBuilder.Entity<Tbl_ContractClauseMapping>()
                .HasOne(mapping => mapping.Clause)
                .WithMany(clause => clause.ContractClauseMappings)
                .HasForeignKey(mapping => mapping.ClauseId);

            modelBuilder.Entity<Tbl_Option>()
        .HasOne(o => o.Test)
        .WithMany(t => t.Options)
        .HasForeignKey(o => o.TestId)
        .OnDelete(DeleteBehavior.Restrict);  // یا DeleteBehavior.NoAction
          
            modelBuilder.Entity<Tbl_Answer>()
        .HasOne(o => o.Test)
        .WithMany(t => t.Answers)
        .HasForeignKey(o => o.TestId)
        .OnDelete(DeleteBehavior.Restrict);
          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestWithValueDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Tbl_TicketStatus>().HasData(
               new Tbl_TicketStatus { Id = 1, Name = "Open" },
               new Tbl_TicketStatus { Id = 2, Name = "InProgress" },
               new Tbl_TicketStatus { Id = 3, Name = "Closed" },
               new Tbl_TicketStatus { Id = 4, Name = "Resolved" }
           );
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Tbl_User>())
            {
                entry.Entity.UpdatedDateTime = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDateTime = DateTime.Now;
                }
            }


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Tbl_User>())
            {
                entry.Entity.UpdatedDateTime = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();


        }
    }
}

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
        public DbSet<Tbl_Location>  tbl_Locations { get; set; }









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


            modelBuilder.Entity<Tbl_Answer>()
                .HasKey(a => a.AnswerId);
                     
            modelBuilder.Entity<Tbl_Answer>()
             .HasOne(a => a.Question)
           .WithOne(o => o.Answer)
            .HasForeignKey<Tbl_Answer>(a => a.QuestionId)  // کلید خارجی به Tbl_Answer اشاره می‌کند
             .OnDelete(DeleteBehavior.Restrict);
           
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

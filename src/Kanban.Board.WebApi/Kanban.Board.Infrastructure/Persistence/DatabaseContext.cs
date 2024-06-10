using Kanban.Board.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kanban.Board.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<CandidateJobRelation> CandidateJobRelations { get; set; }
        public DbSet<CandidateInterviewerRelation> CandidateInterviewerRelations { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dic = Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CandidateJobRelation>()
            .HasOne(n => n.Candidate)
            .WithMany(n => n.CandidateJobRelations)
            .HasForeignKey(n => n.CandidateId);

            modelBuilder.Entity<CandidateJobRelation>()
               .HasOne(n => n.Job)
               .WithMany(n => n.CandidateJobRelations)
               .HasForeignKey(n => n.JobId);

            modelBuilder.Entity<Candidate>().
                Property(e => e.CandidateStatus)
                .HasDefaultValue(CandidateStatus.Applied)
                .HasConversion<string>();

            modelBuilder.Entity<Candidate>()
                .Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            modelBuilder.Entity<CandidateInterviewerRelation>()
                .HasOne(n => n.Interviewer)
                .WithMany(n => n.CandidateInterviewerRelations);

            modelBuilder.Entity<CandidateInterviewerRelation>()
                .HasOne(n => n.Candidate)
                .WithMany(n => n.CandidateInterviewerRelations);
        }
    }
}
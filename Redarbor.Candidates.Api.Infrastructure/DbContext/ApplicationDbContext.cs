using Microsoft.EntityFrameworkCore;
using Redarbor.Candidates.Api.Domain.Entities;

namespace Redarbor.Candidates.Api.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Candidate?> Candidates { get; set; }
    public DbSet<CandidateExperience> Experiences { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>().ToTable("candidates");
        modelBuilder.Entity<CandidateExperience>().ToTable("candidateexperiences");


        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.CandidateExperiences)
            .WithOne(e => e.Candidate)
            .HasForeignKey(e => e.IdCandidate)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<CandidateExperience>()
            .HasKey(e => e.IdCandidateExperience);

        modelBuilder.Entity<Candidate>()
            .HasKey(c => c.IdCandidate);
    }
}
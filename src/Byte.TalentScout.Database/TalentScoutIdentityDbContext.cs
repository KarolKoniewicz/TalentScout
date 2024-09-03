using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Byte.TalentScout.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Byte.TalentScout.Database;

public class TalentScoutIdentityDbContext : IdentityDbContext<Candidate, IdentityRole<long>, long>
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Degree> Degrees { get; set; }

    public TalentScoutIdentityDbContext(DbContextOptions<TalentScoutIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Degree>()
                    .HasOne(d => d.Candidate)
                    .WithMany(c => c.Degrees)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}
using LendingPlatform.Domain;
using Microsoft.EntityFrameworkCore;

namespace LendingPlatform.Data;

public sealed class LendingDbContext(DbContextOptions<LendingDbContext> options) : DbContext(options)
{
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var application = modelBuilder.Entity<LoanApplication>();
        application.Property(item => item.LoanAmount).HasPrecision(18, 2);
        application.Property(item => item.AssetValue).HasPrecision(18, 2);
        application.Property(item => item.LoanToValue).HasPrecision(9, 4);
        application.Property(item => item.Decision).HasConversion<string>();
        application.Property(item => item.DecisionReason).HasMaxLength(500);
    }
}

using Microsoft.EntityFrameworkCore;
using PiApi.Domain;
using WalletTS.Database;

namespace PiApi.Database;

public class PiApiDbContext : DbContext
{
    public DbSet<Pi> PiDb { get; set; }
    public DbSet<Palindromic> Palindromics { get; set; }

    public PiApiDbContext(DbContextOptions<PiApiDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("ppp");

        builder.ApplyConfiguration(new PiConfig());
        builder.ApplyConfiguration(new PalindromicConfig());
    }  
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiApi.Domain;

namespace WalletTS.Database;

public class PiConfig : IEntityTypeConfiguration<Pi>
{
    public void Configure(EntityTypeBuilder<Pi> pi)
    {
        pi.ToTable("pi");

        pi.HasKey(p => p.Million);
        pi.Property(p => p.Million).ValueGeneratedNever();

        pi.Property(p => p.Digits);

        pi.HasCheckConstraint("digits_length", "length(digits) = 1000000");
    }
}

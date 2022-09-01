using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiApi.Domain;

namespace WalletTS.Database;

public class PalindromicConfig : IEntityTypeConfiguration<Palindromic>
{
    public void Configure(EntityTypeBuilder<Palindromic> palindromic)
    {
        palindromic.ToTable("palindromics");

        palindromic.HasKey(p => p.Index);
        palindromic.Property(p => p.Index)
            .HasColumnType("bigint")
            .ValueGeneratedNever();

        palindromic.Property(p => p.Length);
        palindromic.Property(p => p.Digits);
        palindromic.Property(p => p.IsPrime);

        palindromic.HasCheckConstraint("digits_length", "length(digits) = length");
    }
}

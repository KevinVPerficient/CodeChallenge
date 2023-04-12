using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Data.Models.ModelsConfiguration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(5);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.SellerCode) 
                .IsRequired()
                .HasMaxLength(5);
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(12);
            builder.Property(x => x.CellPhoneNumber)
                .HasMaxLength(15);     
        }
    }
}

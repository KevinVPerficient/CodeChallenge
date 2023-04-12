using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Data.Models.ModelsConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.ClientType)
                .IsRequired();
            builder.Property(x => x.DocType) 
                .IsRequired();
            builder.Property(x => x.DocNumber)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(12);
            builder.Property(x => x.CellPhoneNumber)
                .HasMaxLength(15);
        }                                             
    }
}

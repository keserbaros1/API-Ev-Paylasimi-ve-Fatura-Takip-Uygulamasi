using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Configurations
{
    public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Status).HasDefaultValue(true);
            builder.HasQueryFilter(e => e.Status);

            // Payment -> House (Many-to-One)
            builder.HasOne(p => p.House)
                .WithMany(h => h.Payments)
                .HasForeignKey(p => p.HouseId)
                .OnDelete(DeleteBehavior.Cascade);  // House silindiğinde Payment'ler de silinsin

            // Payment -> User (Many-to-One)
            builder.HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // User silindiğinde Payment'ler silinmesin
        }
    }
}

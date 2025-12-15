using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Status).HasDefaultValue(true);

            builder.HasQueryFilter(e => e.Status);


            // Expense -> House (Many-to-One)
            builder.HasOne(e => e.House)
                .WithMany(h => h.Expense)
                .HasForeignKey(e => e.HouseId)
                .OnDelete(DeleteBehavior.Cascade);  // House silindiğinde Expense'ler de silinsin

            // Expense -> User (Many-to-One)
            builder.HasOne(e => e.User)
                .WithMany(u => u.Expense)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); // User silindiğinde Expense'ler silinmesin
        }
    }
}

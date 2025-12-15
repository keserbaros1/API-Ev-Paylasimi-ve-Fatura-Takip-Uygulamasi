using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Configurations
{
    public class ExpenseShareConfiguration : IEntityTypeConfiguration<ExpenseShare>
    {
        public void Configure(EntityTypeBuilder<ExpenseShare> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Status).HasDefaultValue(true);

            builder.HasQueryFilter(e => e.Status);

            // ExpenseShare -> Expense (Many-to-One)
            builder.HasOne(es => es.Expense)
                .WithMany(e => e.ExpenseShares)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade); // Harcama silinirse paylaşımlar da silinsin


            // ExpenseShare -> User (Many-to-One)
            builder.HasOne(es => es.User)
                .WithMany(u => u.ExpenseShares)
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse paylaşımlar silinmesin
        }
    }
}

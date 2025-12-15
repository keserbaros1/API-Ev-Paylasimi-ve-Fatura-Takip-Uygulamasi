using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Configurations
{
    public class HouseMemberConfiguration : IEntityTypeConfiguration<HouseMember>
    {
        public void Configure(EntityTypeBuilder<HouseMember> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Status).HasDefaultValue(true);
            builder.HasQueryFilter(e => e.Status);

            // HouseMember -> House (Many-to-One)
            builder.HasOne(hm => hm.House)
                .WithMany(h => h.HouseMembers)
                .HasForeignKey(hm => hm.HouseID)
                .OnDelete(DeleteBehavior.Cascade);  // House silindiğinde HouseMember da silinsin

            // HouseMember -> User (Many-to-One)
            builder.HasOne(hm => hm.User)
                .WithMany(u => u.HouseMembers)
                .HasForeignKey(hm => hm.UserID)
                .OnDelete(DeleteBehavior.Restrict); // User silindiğinde HouseMember silinmesin
        }
    }
}

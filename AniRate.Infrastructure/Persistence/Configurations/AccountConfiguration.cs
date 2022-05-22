using AniRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(account => account.Id);
            builder.HasIndex(account => account.Id).IsUnique();
            builder.Property(account => account.Name).HasMaxLength(200).IsRequired();
            builder.Property(account => account.EmailAddress).HasMaxLength(200).IsRequired();
            builder.Property(account => account.Password).HasMaxLength(200).IsRequired();
        }
    }
}

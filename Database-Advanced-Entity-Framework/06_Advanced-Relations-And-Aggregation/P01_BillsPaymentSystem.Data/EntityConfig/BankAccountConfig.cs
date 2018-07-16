﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.BankAccountId);

            builder.Property(x => x.BankName).IsRequired().IsUnicode().HasMaxLength(50);

            builder.Property(x => x.SwiftCode).IsRequired().IsUnicode(false).HasMaxLength(20);

            builder.Ignore(x => x.PaymentMethodId);
        }
    }
}

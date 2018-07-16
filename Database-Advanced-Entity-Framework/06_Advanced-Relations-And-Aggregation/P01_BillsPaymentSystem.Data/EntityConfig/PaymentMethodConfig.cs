using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.CreditCardId, x.BankAccount }).IsUnique();

            builder.HasOne(x => x.User).WithMany(x => x.PaymentMethods).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.BankAccount).WithOne(e => e.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.BankAccountId);

            builder.HasOne(x => x.CreditCard).WithOne(x => x.PaymentMethod)
                .HasForeignKey<PaymentMethod>(x => x.CreditCardId);
        }
    }
}

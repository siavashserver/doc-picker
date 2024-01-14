using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Accounts.Core.DataAccess.Entities;

namespace Services.Accounts.Core.DataAccess.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(account => account.Email).IsUnique();
    }
}
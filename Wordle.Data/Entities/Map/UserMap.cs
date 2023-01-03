using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wordle.Data.Entities.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Matches).WithOne(x => x.User)
                .HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
        }
    }
}

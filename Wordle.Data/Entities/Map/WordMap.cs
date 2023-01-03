using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wordle.Data.Entities.Map
{
    public class WordMap : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).UseIdentityColumn();

            builder.Property(w => w.Text).IsRequired().HasMaxLength(30);
        }
    }
}

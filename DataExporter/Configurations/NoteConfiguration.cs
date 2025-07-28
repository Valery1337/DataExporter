using DataExporter.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataExporter.Configurations;
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasOne(e => e.Policy)
                .WithMany(e => e.Notes)
                .HasForeignKey(e => e.PolicyId)
                .IsRequired();

        builder.HasData(new Note() { Id = 1, Text = "The first note is for policy with id = 1", PolicyId = 1 },
                new Note { Id = 2, Text = "The second note is for policy with id = 1", PolicyId = 1 },
                new Note { Id = 3, Text = "The first note is for policy with id = 2", PolicyId = 2 });
    }
}

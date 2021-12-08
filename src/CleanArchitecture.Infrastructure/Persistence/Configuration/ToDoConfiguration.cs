using CleanArchitecture.Core.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable("ToDoItems");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(200);

        builder.Property(t => t.IsDone);

        builder.Ignore(t => t.Events);
    }
}

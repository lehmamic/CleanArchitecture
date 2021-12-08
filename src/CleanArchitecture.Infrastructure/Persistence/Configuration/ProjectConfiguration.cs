using CleanArchitecture.Core.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Ignore(t => t.Status);
        builder.Ignore(t => t.Events);
    }
}

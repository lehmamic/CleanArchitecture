using System.Reflection;
using CleanArchitecture.Core.Projects;
using CleanArchitecture.SharedKernel.Events;
using CleanArchitecture.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Threading;

namespace CleanArchitecture.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IEventBus? _eventBus;

    public AppDbContext(DbContextOptions options, IEventBus? eventBus)
        : base(options)
    {
        _eventBus = eventBus;
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    public DbSet<Project> Projects => Set<Project>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_eventBus == null)
        {
            return result;
        }

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<Guid>>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();

            foreach (var domainEvent in events)
            {
                await _eventBus.PublishAsync(domainEvent, cancellationToken).ConfigureAwait(false);
            }
        }

        return result;
    }

    public override int SaveChanges()
    {
        using var context = new JoinableTaskContext();
        var jtf = new JoinableTaskFactory(context);
        return jtf.Run(() => SaveChangesAsync());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

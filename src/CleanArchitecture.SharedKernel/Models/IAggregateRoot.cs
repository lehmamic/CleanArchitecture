using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.SharedKernel.Models;

// Apply this marker interface only to aggregate root entities
// Repositories will only work with aggregate roots, not their children
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "This is a marker interface")]
public interface IAggregateRoot
{
}

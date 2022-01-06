using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.SharedKernel.Events;

[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "This is a marker interface")]
public interface IExternalEvent : IEvent
{
}

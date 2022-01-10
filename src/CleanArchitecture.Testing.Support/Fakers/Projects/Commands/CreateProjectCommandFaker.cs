using AutoBogus;
using CleanArchitecture.Application.Projects.Commands.CreateProject;

namespace CleanArchitecture.Testing.Support.Fakers.Projects.Commands;

public sealed class CreateProjectCommandFaker : AutoFaker<CreateProjectCommand>
{
    public CreateProjectCommandFaker()
    {
        RuleFor(c => c.Name, f => f.Random.String(1, 10));
    }
}

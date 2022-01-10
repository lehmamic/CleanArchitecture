using AutoBogus;
using CleanArchitecture.Core.Projects;

namespace CleanArchitecture.Testing.Support.Fakers.Projects.Entities;

public sealed class ProjectFaker : AutoFaker<Project>
{
    public ProjectFaker()
    {
        CustomInstantiator(f => new Project(f.Random.String(1, 10)));
    }
}

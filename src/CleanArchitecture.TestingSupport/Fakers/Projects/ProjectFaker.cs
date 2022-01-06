using AutoBogus;
using CleanArchitecture.Core.Projects;

namespace CleanArchitecture.TestingSupport.Fakers.Projects;

public sealed class ProjectFaker : AutoFaker<Project>
{
    public ProjectFaker()
    {
        CustomInstantiator(f => new Project(f.Random.String(1, 10)));
    }
}

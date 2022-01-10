using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Testing.Support.Fakers.Projects;
using CleanArchitecture.Testing.Support.Fakers.Projects.Entities;
using Xunit;

namespace CleanArchitecture.Application.Tests;

public class MappingTest
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTest()
    {
        _configuration = new MapperConfiguration(config =>
        {
            config.AddMaps(typeof(DependencyInjection).Assembly);
        });

        _mapper = _configuration.CreateMapper();
    }

    public static IEnumerable<object[]> MappingTestData =>
        new List<object[]>
        {
            new object[] { new ProjectFaker().Generate(), typeof(ProjectDto) },
            new object[] { new ToDoItemFaker().Generate(), typeof(ToDoItemDto) },
        };

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [MemberData(nameof(MappingTestData))]
    public void ShouldSupportMappingFromSourceToDestination(object source, Type destinationType)
    {
        _mapper.Map(source, source.GetType(), destinationType);
    }
}

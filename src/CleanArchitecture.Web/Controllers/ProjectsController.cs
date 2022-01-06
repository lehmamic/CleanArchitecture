using System.Collections.Immutable;
using CleanArchitecture.Application.Projects.Commands.CreateProject;
using CleanArchitecture.Application.Projects.Commands.DeleteProject;
using CleanArchitecture.Application.Projects.Commands.UpdateProject;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Application.Projects.Queries.GetProjectById;
using CleanArchitecture.Application.Projects.Queries.GetProjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IImmutableList<ProjectDto>>> GetProjectsAsync()
    {
        var result = await _mediator.Send(new GetProjectsQuery());
        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetProjectAsync))]
    public async Task<ActionResult<ProjectDto>> GetProjectAsync([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProjectAsync([FromBody] CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtRoute(nameof(GetProjectAsync), () => new { id = result.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProjectAsync([FromRoute] Guid id, [FromBody] UpdateProjectCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDto>> DeleteProjectAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteProjectCommand { Id = id });
        return NoContent();
    }
}

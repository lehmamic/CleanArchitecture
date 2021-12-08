using System.Collections.Immutable;
using CleanArchitecture.Application.Projects.Commands.CreateProject;
using CleanArchitecture.Application.Projects.Commands.DeleteProject;
using CleanArchitecture.Application.Projects.Commands.UpdateProject;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Application.Projects.Queries.GetProjectById;
using CleanArchitecture.Application.Projects.Queries.GetProjects;
using CleanArchitecture.Web.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IImmutableList<ProjectDto>>> GetProjects()
    {
        var result = await _mediator.Send(new GetProjectsQuery());
        return result.ToActionResult();
    }

    [HttpGet("{id}", Name = nameof(GetProject))]
    public async Task<ActionResult<ProjectDto>> GetProject([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToCreatedActionResult(nameof(GetProject), () => new { id = result.Value.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject([FromRoute] Guid id, [FromBody] UpdateProjectCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return result.ToNoContentActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDto>> DeleteProject([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteProjectCommand { Id = id });
        return NoContent();
    }
}

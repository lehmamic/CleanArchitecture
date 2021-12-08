using System.Collections.Immutable;
using CleanArchitecture.Application.Projects.Commands.CompleteToDoItem;
using CleanArchitecture.Application.Projects.Commands.CreateToDoItem;
using CleanArchitecture.Application.Projects.Commands.DeleteToDoItem;
using CleanArchitecture.Application.Projects.Commands.UpdateToDoItem;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Application.Projects.Queries.GetDoDoItemById;
using CleanArchitecture.Application.Projects.Queries.GetToDoItems;
using CleanArchitecture.Web.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;

[ApiController]
[Route("projects/{projectId}/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IImmutableList<ToDoItemDto>>> GetToDoItems([FromRoute] Guid projectId)
    {
        var result = await _mediator.Send(new GetToDoItemsQuery { ProjectId = projectId });
        return result.ToActionResult();
    }

    [HttpGet("/projects/{projectId}/incomplete-items")]
    public async Task<ActionResult<IImmutableList<ToDoItemDto>>> GetIncompleteToDoItems([FromRoute] Guid projectId)
    {
        var result = await _mediator.Send(new GetToDoItemsQuery { ProjectId = projectId, Completed = false});
        return result.ToActionResult();
    }

    [HttpGet("{id}", Name = nameof(GetToDoItem))]
    public async Task<ActionResult<ToDoItemDto>> GetToDoItem([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetToDoItemByIdQuery { ProjectId = projectId, Id = id });
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemDto>> CreateToDoItem([FromRoute] Guid projectId, [FromBody] CreateToDoItemCommand command)
    {
        if (projectId != command.ProjectId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return result.ToCreatedActionResult(nameof(GetToDoItem), () => new { projectId = command.ProjectId , id = result.Value.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ToDoItemDto>> UpdateToDoItem([FromRoute] Guid projectId, [FromRoute] Guid id, [FromBody] UpdateToDoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        if (projectId != command.ProjectId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return result.ToNoContentActionResult();
    }
    
    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<ToDoItemDto>> CompleteToDoItem([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
        var result = await _mediator.Send(new CompleteToDoItemCommand { ProjectId = projectId, Id = id});
        return result.ToNoContentActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDto>> DeleteToDoItem([FromRoute] Guid projectId, [FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteToDoItemCommand { ProjectId = projectId, Id = id });
        return NoContent();
    }
}

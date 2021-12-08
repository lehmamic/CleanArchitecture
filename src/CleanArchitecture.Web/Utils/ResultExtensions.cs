using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Utils;

public static class ResultExtensions
{
    public static ActionResult<T> ToCreatedActionResult<T>(this Result<T> result, string routeName, Func<object?> routeValues)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new CreatedAtRouteResult(routeName, routeValues(), result.Value),
            _ => result.ToActionResult(),
        };
    }
    
    public static ActionResult<T> ToNoContentActionResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new NoContentResult(),
            _ => result.ToActionResult(),
        };
    }

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new OkObjectResult(result.GetValue()),
            ResultStatus.Error=> new UnprocessableEntityObjectResult(result),
            ResultStatus.Invalid => new BadRequestObjectResult(result),
            ResultStatus.NotFound=> new NotFoundResult(),
            ResultStatus.Forbidden => new ForbidResult(),
            ResultStatus.Unauthorized=> new UnauthorizedResult(),
            _ => throw new NotImplementedException(
                $"The conversion of {result.Status} to ActionResult has not been implemented yet.")
        };
    }
}

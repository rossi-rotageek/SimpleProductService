using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace SimpleService.Core;

public class BaseApiController : ControllerBase
{
    protected IActionResult ResponseFromError(Error error)
    {
        return ErrorResponseWithSpecificHttpStatusCode(error.ErrorType, error);
    }

    private IActionResult ErrorResponseWithSpecificHttpStatusCode(ErrorType errorType, Error error)
    {
        switch (errorType)
        {
            case ErrorType.AccessDenied:
            case ErrorType.PermissionMissing:
            case ErrorType.NotAllowed:
                return Unauthorized(error);
            case ErrorType.ValidationFailed:
                return BadRequest(error);
            case ErrorType.ValueNotFound:
                return NotFound(error);
            default:
                throw new IndexOutOfRangeException(nameof(errorType));
        }
    }
}
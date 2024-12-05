using AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]         //swaggerda gözükmesin diye
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return NoContent();
            //return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };   //response body ve status code dinamik hale getirdik
        }
        if (result.StatusCode == HttpStatusCode.Created)
        {
            return Created(result.UrlAsCreated, result);
        }
        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };   //response body ve status code dinamik hale getirdik
        }
        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }
}



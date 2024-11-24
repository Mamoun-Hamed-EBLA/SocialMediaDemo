using API.Filters;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExceptionFilter]
public abstract class ApiBaseController : ControllerBase
{
	private ISender _mediator = null!;

	private IMemoryCache _memoryCache = null!;
	protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

	protected IMemoryCache Cache => _memoryCache ??= HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
	protected OkObjectResult OkObject(object result)
	{
		return Ok(new SuccessObject(result));
	}


}
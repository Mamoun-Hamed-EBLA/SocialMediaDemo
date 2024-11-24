using Application.Models;
using Application.Services.Comment.Commands.Delete;
using Application.Services.Comment.Commands.Update;
using Application.Services.Comment.DTOs;
using Application.Services.Comment.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;
[Authorize]
public class CommentController : ApiBaseController
{

	[HttpGet()]
	[ProducesResponseType(typeof(PaginatedList<CommentDto>), 200)]
	public async Task<IActionResult> GetAll([FromQuery] GetCommentsQuery request, CancellationToken cancellationToken = default)
	{
		if (Cache.TryGetValue(request, out var value))
		{
			return OkObject(value!);
		}
		var rersult = await Mediator.Send(request, cancellationToken);
		Cache.Set(request, rersult, DateTimeOffset.UtcNow.AddMinutes(5));
		return OkObject(rersult);
	}


	[HttpPut()]
	[ProducesResponseType(204)]
	public async Task<IActionResult> Put(UpdateCommentCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}
	[HttpDelete()]
	[ProducesResponseType(204)]
	public async Task<IActionResult> Delete(DeleteCommentCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}

}
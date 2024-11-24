using Application.Models;
using Application.Services.Comment.Commands.Create;
using Application.Services.Post.Commands.Create;
using Application.Services.Post.Commands.Like;
using Application.Services.Post.Commands.Unlike;
using Application.Services.Post.Commands.Update;
using Application.Services.Post.DTOs;
using Application.Services.Post.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;

[Authorize]
public class PostController : ApiBaseController
{
	[HttpGet()]
	[ProducesResponseType(typeof(PaginatedList<PostDto>), 200)]
	public async Task<IActionResult> GetAll([FromQuery] GetPostsQuery request, CancellationToken cancellationToken = default)
	{
		if (Cache.TryGetValue(request, out var value))
		{
			return OkObject(value!);
		}
		var rersult = await Mediator.Send(request, cancellationToken);
		Cache.Set(request, rersult, DateTimeOffset.UtcNow.AddMinutes(5));
		return OkObject(rersult);
	}

	[HttpPost()]
	[ProducesResponseType(204)]
	public async Task<IActionResult> Post(CreatePostCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}

	[HttpPut()]
	[ProducesResponseType(204)]
	public async Task<IActionResult> Put(UpdatePostCommand request, CancellationToken cancellationToken = default)
	{

		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}

	[HttpPost("comment")]
	[ProducesResponseType(204)]
	public async Task<IActionResult> AddComment(CreateCommentCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}
	[HttpPut("like")]
	[ProducesResponseType(204)]
	public async Task<IActionResult> LikePost(LikePostCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}

	[HttpPut("unlike")]
	[ProducesResponseType(204)]
	public async Task<IActionResult> UnLike(UnlikePostCommand request, CancellationToken cancellationToken = default)
	{
		await Mediator.Send(request, cancellationToken);
		return NoContent();
	}
}

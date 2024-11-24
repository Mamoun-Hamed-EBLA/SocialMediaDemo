using FluentValidation;

namespace Application.Services.Post.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
	public CreatePostCommandValidator()
	{
		RuleFor(e => e.Post)
			.NotEmpty()
			.MaximumLength(512);
	}
}
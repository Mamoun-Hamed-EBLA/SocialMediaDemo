using FluentValidation;

namespace Application.Services.Comment.Commands.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
	public CreateCommentCommandValidator()
	{
		RuleFor(e => e.PostId).NotEmpty();
		RuleFor(e => e.Comment)
			.NotEmpty()
			.MaximumLength(512);
	}
}
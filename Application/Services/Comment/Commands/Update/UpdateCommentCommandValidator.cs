using FluentValidation;

namespace Application.Services.Comment.Commands.Update;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
	public UpdateCommentCommandValidator()
	{
		RuleFor(e => e.Id).NotEmpty();
		RuleFor(e => e.Comment)
			.NotEmpty()
			.MaximumLength(512);
	}
}
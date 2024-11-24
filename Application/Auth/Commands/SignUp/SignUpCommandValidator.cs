using FluentValidation;

namespace Application.Auth.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
	public SignUpCommandValidator()
	{
		RuleFor(e => e.UserName).NotEmpty()
			.MaximumLength(64);

		RuleFor(e => e.Password).NotEmpty()
			.MaximumLength(64);
	}
}
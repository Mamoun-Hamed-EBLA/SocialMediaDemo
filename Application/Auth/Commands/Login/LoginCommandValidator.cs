using FluentValidation;

namespace Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
	public LoginCommandValidator()
	{
		RuleFor(e => e.UserName).NotEmpty()
			.MaximumLength(64);

		RuleFor(e => e.Password).NotEmpty()
			.MaximumLength(64);
	}
}
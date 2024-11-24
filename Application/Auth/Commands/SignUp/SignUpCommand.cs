namespace Application.Auth.Commands.SignUp;

public record SignUpCommand(string UserName, string Password) : IRequest;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand>
{
	private readonly IIdentityService _identityService;

	public SignUpCommandHandler(IIdentityService identityService)
	{
		_identityService = identityService;
	}

	public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
	{
		var result = await _identityService.CreateUserAsync(UserEntity.Create(request.UserName), request.Password);

		if (!result.Succeeded)
		{
			throw new ApplicationException(string.Join(',', result.Errors.Select(x => x.Code).ToList()));
		}
	}
}

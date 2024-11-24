namespace Application.Auth.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<TokenInfo>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenInfo>
{
	private readonly IIdentityService _identityService;

	public LoginCommandHandler(IIdentityService identityService)
	{
		_identityService = identityService;
	}

	public async Task<TokenInfo> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		return await _identityService.SignInAsync(request.UserName, request.Password);
	}
}

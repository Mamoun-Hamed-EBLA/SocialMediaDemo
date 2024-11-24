using Application.Auth.Commands.Login;
using Application.Auth.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : ApiBaseController
{


	[HttpPost("signUp")]
	public async Task<IActionResult> SignUp(SignUpCommand request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpPost("logIn")]
	public async Task<IActionResult> Login(LoginCommand request)
	{
		return OkObject(await Mediator.Send(request));
	}
}

using Application.Interfaces;

namespace Infrastructure.Utility;
public class CurrentDate : IDateTime
{
	public DateTime Now => DateTime.UtcNow;
}

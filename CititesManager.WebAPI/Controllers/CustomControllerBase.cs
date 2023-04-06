using Microsoft.AspNetCore.Mvc;

namespace CititesManager.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomControllerBase : ControllerBase //the attributes get transfered to the child object
	{
	}
}
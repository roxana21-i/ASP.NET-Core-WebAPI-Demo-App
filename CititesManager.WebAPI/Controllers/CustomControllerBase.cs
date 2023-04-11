using Microsoft.AspNetCore.Mvc;

namespace CititesManager.WebAPI.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	//[Route("api/[controller]")]
	[ApiController]
	public class CustomControllerBase : ControllerBase //the attributes get transfered to the child object
	{
	}
}
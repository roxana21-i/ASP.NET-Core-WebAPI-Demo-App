using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CititesManager.WebAPI.Controllers
{
	public class TestController : CustomControllerBase
	{
		[HttpGet]
		public string Method()
		{
			return "Hello World";
		}
	}
}

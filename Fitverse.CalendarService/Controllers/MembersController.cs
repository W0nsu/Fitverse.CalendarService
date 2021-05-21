using System.Threading.Tasks;
using Fitverse.CalendarService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitverse.CalendarService.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/cs/members")]
	public class MembersController : Controller
	{
		private readonly IMediator _mediator;

		public MembersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllMembers()
		{
			var query = new GetAllMembersQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}
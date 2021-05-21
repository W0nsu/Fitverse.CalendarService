using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitverse.CalendarService.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/cs/timetable")]
	public class TimetablesController : Controller
	{
		private readonly IMediator _mediator;

		public TimetablesController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllTimetables()
		{
			var query = new GetAllTimetablesQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet]
		[Route("{timetableId}")]
		public async Task<IActionResult> GetTimetableById(int timetableId)
		{
			var query = new GetTimetableByIdQuery(timetableId);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddTimetable([FromBody] TimetableDto timetableDto)
		{
			var command = new AddTimetableCommand(timetableDto);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete]
		[Route("{timetableId}")]
		public async Task<IActionResult> DeleteTimetable([FromRoute]int timetableId)
		{
			var command = new DeleteTimetableByIdCommand(timetableId);
			var result = await _mediator.Send(command);
			return Ok($"Timetable [timetableId: {result.TimetableId}] has been deleted");
		}
	}
}
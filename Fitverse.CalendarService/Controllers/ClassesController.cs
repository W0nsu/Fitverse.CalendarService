using System;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Helpers;
using Fitverse.CalendarService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Fitverse.CalendarService.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/cs/classes")]
	public class ClassesController : Controller
	{
		private readonly IMediator _mediator;

		public ClassesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetClassByDate([FromQuery] DateRange dateRange)
		{
			var query = new GetClassByDateQuery(dateRange);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet]
		[Route("{classId}")]
		public async Task<IActionResult> GetClassById([FromRoute] int classId)
		{
			var query = new GetClassByIdQuery(classId);
			var result = await _mediator.Send(query);
			return Ok(result);
		}
		
		[HttpDelete]
		[Route("{classId}")]
		public async Task<IActionResult> DeleteClassById([FromRoute] int classId)
		{
			var command = new DeleteClassByIdCommand(classId);
			var result = await _mediator.Send(command);
			return Ok($"Class [ClassId: {result.ClassId}] has been deleted");
		}
		
		[HttpGet]
		[Route("{classId}/reservation")]
		public async Task<IActionResult> GetReservationsByClassId([FromRoute] int classId)
		{
			var command = new GetReservationsByClassIdCommand(classId);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPost]
		[Route("reservation")]
		public async Task<IActionResult> SignUpForClass([FromBody] ReservationDtoSetter reservation)
		{
			var command = new SignUpForClassCommand(reservation);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete]
		[Route("reservation/{reservationId}")]
		public async Task<IActionResult> SignOutOfClass([FromRoute] int reservationId)
		{
			var command = new SignOutOfClassCommand(reservationId);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Models;
using Fitverse.CalendarService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Fitverse.CalendarService.Controllers
{
	[Authorize]
	[Route("/api/cs/calendar/settings")]
	[ApiController]
	public class SettingsController : Controller
	{
		private readonly IMediator _mediator;

		public SettingsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllClassTypes()
		{
			var query = new GetAllClassTypesQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}
		
		[HttpPost]
		public async Task<IActionResult> AddClassType([FromBody] ClassTypeDto membershipDto)
		{
			var command = new AddClassTypeCommand(membershipDto);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		
		[HttpGet("{classTypeId}")]
		public async Task<IActionResult> GetClassTypeById([FromRoute] int classTypeId)
		{
			var query = new GetClassTypeByIdQuery(classTypeId);
			var result = await _mediator.Send(query);
			return Ok(result);
		}
		
		[HttpPatch("{classTypeId}")]
		public async Task<IActionResult> EditClassType([FromRoute] int classTypeId,
			[FromBody] JsonPatchDocument<ClassType> classTypeEntity)
		{
			var command = new EditClassTypeCommand(classTypeId, classTypeEntity);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		
		[HttpDelete("{classTypeId}")]
		public async Task<IActionResult> DeleteClassType([FromRoute] int classTypeId)
		{
			var command = new DeleteClassTypeCommand(classTypeId);
			var result = await _mediator.Send(command);
			return Ok($"Class type [ClassTypeId: {result.ClassTypeId}] has been deleted");
		}
	}
}
using System.Collections.Generic;
using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetAllTimetablesQuery : IRequest<List<TimetableDto>>
	{
	}
}
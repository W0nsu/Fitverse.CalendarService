using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class AddTimetableCommand : IRequest<TimetableDto>
	{
		public AddTimetableCommand(TimetableDto timetableDto)
		{
			NewTimetableDto = timetableDto;
		}

		public TimetableDto NewTimetableDto { get; }
	}
}
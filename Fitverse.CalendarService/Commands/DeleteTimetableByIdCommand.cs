using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class DeleteTimetableByIdCommand : IRequest<TimetableDto>
	{
		public DeleteTimetableByIdCommand(int timetableId)
		{
			TimetableId = timetableId;
		}

		public int TimetableId { get; }
	}
}
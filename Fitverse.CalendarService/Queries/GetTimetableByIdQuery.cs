using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetTimetableByIdQuery : IRequest<TimetableDto>
	{
		public GetTimetableByIdQuery(int timetableId)
		{
			TimetableId = timetableId;
		}

		public int TimetableId { get; }
	}
}
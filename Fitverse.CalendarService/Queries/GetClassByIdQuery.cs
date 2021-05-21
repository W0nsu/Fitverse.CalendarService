using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetClassByIdQuery : IRequest<CalendarClassDto>
	{
		public GetClassByIdQuery(int classId)
		{
			ClassId = classId;
		}

		public int ClassId { get; }
	}
}
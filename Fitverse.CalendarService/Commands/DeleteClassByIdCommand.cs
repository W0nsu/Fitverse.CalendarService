using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class DeleteClassByIdCommand : IRequest<CalendarClassDto>
	{
		public DeleteClassByIdCommand(int classId)
		{
			ClassId = classId;
		}

		public int ClassId { get; }
	}
}
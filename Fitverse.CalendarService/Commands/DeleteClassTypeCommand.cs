using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class DeleteClassTypeCommand : IRequest<ClassTypeDto>
	{
		public DeleteClassTypeCommand(int classTypeId)
		{
			ClassTypeId = classTypeId;
		}

		public int ClassTypeId { get; }
	}
}
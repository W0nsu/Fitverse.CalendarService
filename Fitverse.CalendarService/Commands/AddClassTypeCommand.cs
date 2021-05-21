using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class AddClassTypeCommand : IRequest<ClassTypeDto>
	{
		public AddClassTypeCommand(ClassTypeDto classType)
		{
			NewClassType = classType;
		}

		public ClassTypeDto NewClassType { get; }
	}
}
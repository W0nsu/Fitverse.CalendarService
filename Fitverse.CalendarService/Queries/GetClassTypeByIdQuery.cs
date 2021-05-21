using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetClassTypeByIdQuery : IRequest<ClassTypeDto>
	{
		public GetClassTypeByIdQuery(int classTypeId)
		{
			ClassTypeId = classTypeId;
		}

		public int ClassTypeId { get; }
	}
}
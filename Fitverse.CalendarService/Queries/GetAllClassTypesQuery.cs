using System.Collections.Generic;
using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetAllClassTypesQuery : IRequest<List<ClassTypeDto>>
	{
	}
}
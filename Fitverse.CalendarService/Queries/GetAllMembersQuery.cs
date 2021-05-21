using System.Collections.Generic;
using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetAllMembersQuery : IRequest<List<MemberDto>>
	{
		
	}
}
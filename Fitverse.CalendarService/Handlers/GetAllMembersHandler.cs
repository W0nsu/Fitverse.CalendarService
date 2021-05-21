using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Queries;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, List<MemberDto>>
	{
		private readonly CalendarContext _dbContext;

		public GetAllMembersHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<List<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
		{
			var membersList = await _dbContext.Members.ToListAsync(cancellationToken);

			return membersList.Select(member => member.Adapt<MemberDto>())
				.OrderBy(x => x.SurName).ToList();
		}
	}
}
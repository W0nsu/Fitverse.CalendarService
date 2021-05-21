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
	public class GetAllClassTypesHandler : IRequestHandler<GetAllClassTypesQuery, List<ClassTypeDto>>
	{
		private readonly CalendarContext _dbContext;

		public GetAllClassTypesHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ClassTypeDto>> Handle(GetAllClassTypesQuery request, CancellationToken cancellationToken)
		{
			var classTypesList = await _dbContext
				.ClassTypes
				.Where(x => !x.IsDeleted)
				.ToListAsync(cancellationToken);

			return classTypesList.Select(classType => classType.Adapt<ClassTypeDto>()).ToList();
		}
	}
}
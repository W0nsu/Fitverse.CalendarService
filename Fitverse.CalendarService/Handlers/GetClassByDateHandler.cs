using System;
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
	public class GetClassByDateHandler : IRequestHandler<GetClassByDateQuery, List<CalendarClassDto>>
	{
		private readonly CalendarContext _dbContext;

		public GetClassByDateHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<CalendarClassDto>> Handle(GetClassByDateQuery request,
			CancellationToken cancellationToken)
		{
			var classList = await _dbContext
				.Classes
				.Where(c => c.Date.Date >= request.DateRange.DateFrom && c.Date.Date <= request.DateRange.DateTo)
				.ToListAsync(cancellationToken);

			if (classList is null)
			{
				throw new NullReferenceException(
					$"There is no classes for given period. [Date from: {request.DateRange.DateFrom}, Date to: {request.DateRange.DateTo}]");
			}

			return classList
				.Select(calendarClass => calendarClass.Adapt<CalendarClassDto>())
				.OrderBy(x => x.Date)
				.ThenBy(x => x.StartingTime.TimeOfDay)
				.ToList();
		}
	}
}
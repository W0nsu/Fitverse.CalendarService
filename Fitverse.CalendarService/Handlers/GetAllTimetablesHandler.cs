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
	public class GetAllTimetablesHandler : IRequestHandler<GetAllTimetablesQuery, List<TimetableDto>>
	{
		private readonly CalendarContext _dbContext;

		public GetAllTimetablesHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<TimetableDto>> Handle(GetAllTimetablesQuery request, CancellationToken cancellationToken)
		{
			var timetablesList = await _dbContext
				.Timetables
				.ToListAsync(cancellationToken);

			var classTypesList = await _dbContext.ClassTypes
				.ToListAsync(cancellationToken);

			var timetablesDtoList = new List<TimetableDto>();

			foreach (var timetable in timetablesList)
			{
				var timetableDto = timetable.Adapt<TimetableDto>();
				timetableDto.ClassTypeName =
					classTypesList.FirstOrDefault(x => x.ClassTypeId == timetable.ClassTypeId)?.Name;
				
				timetablesDtoList.Add(timetableDto);
			}

			return timetablesDtoList;
		}
	}
}
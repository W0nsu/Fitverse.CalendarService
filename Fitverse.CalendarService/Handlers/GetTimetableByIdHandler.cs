using System;
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
	public class GetTimetableByIdHandler : IRequestHandler<GetTimetableByIdQuery, TimetableDto>
	{
		private readonly CalendarContext _dbContext;

		public GetTimetableByIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<TimetableDto> Handle(GetTimetableByIdQuery request, CancellationToken cancellationToken)
		{
			var timetableEntity = await _dbContext
				.Timetables
				.SingleOrDefaultAsync(m => m.TimetableId == request.TimetableId, cancellationToken);

			if (timetableEntity is null)
				throw new NullReferenceException($"Timetable[TimetableId: {request.TimetableId} not found]");

			var timetableDto = timetableEntity.Adapt<TimetableDto>();
			timetableDto.ClassTypeName =  _dbContext
				.ClassTypes
				.FirstOrDefault(x => x.ClassTypeId == timetableEntity.ClassTypeId)
				?.Name;

			return timetableDto;
		}
	}
}
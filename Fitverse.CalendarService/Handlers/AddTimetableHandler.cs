using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Helpers;
using Fitverse.CalendarService.Models;
using Mapster;
using MediatR;

namespace Fitverse.CalendarService.Handlers
{
	public class AddTimetableHandler : IRequestHandler<AddTimetableCommand, TimetableDto>
	{
		private readonly CalendarContext _dbContext;

		public AddTimetableHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<TimetableDto> Handle(AddTimetableCommand request, CancellationToken cancellationToken)
		{
			var timetableEntity = request.NewTimetableDto.Adapt<Timetable>();

			_ = await _dbContext.AddAsync(timetableEntity, cancellationToken);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var newTimetable = _dbContext
				.Timetables
				.Where(m => m.ClassTypeId == request.NewTimetableDto.ClassTypeId)
				.AsEnumerable()
				.LastOrDefault();

			if (newTimetable is null)
				throw new NullReferenceException("Failed to add timetable. Try again");

			var classGenerator = new ClassGenerator(_dbContext);
			await classGenerator.AddClassesForTimetableAsync(timetableEntity, cancellationToken);

			var newTimetableDto = newTimetable.Adapt<TimetableDto>();

			return newTimetableDto;
		}
	}
}
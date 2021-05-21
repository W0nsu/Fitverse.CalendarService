using System;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Helpers;
using Fitverse.CalendarService.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class DeleteTimetableByIdHandler : IRequestHandler<DeleteTimetableByIdCommand, TimetableDto>
	{
		private readonly CalendarContext _dbContext;

		public DeleteTimetableByIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<TimetableDto> Handle(DeleteTimetableByIdCommand request, CancellationToken cancellationToken)
		{
			var timetableEntity = await _dbContext
				.Timetables
				.SingleOrDefaultAsync(m => m.TimetableId == request.TimetableId, cancellationToken);

			var classGenerator = new ClassGenerator(_dbContext);
			await classGenerator.DeleteAllClassesByTimetableIdAsync(timetableEntity, cancellationToken);

			_dbContext.Remove(timetableEntity);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var timetableDto = timetableEntity.Adapt<TimetableDto>();

			return timetableDto;
		}
	}
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Helpers;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class DeleteClassTypeHandler : IRequestHandler<DeleteClassTypeCommand, ClassTypeDto>
	{
		private readonly CalendarContext _dbContext;

		public DeleteClassTypeHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ClassTypeDto> Handle(DeleteClassTypeCommand request, CancellationToken cancellationToken)
		{
			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == request.ClassTypeId, cancellationToken);

			classTypeEntity.IsDeleted = true;
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var timetablesList = await _dbContext
				.Timetables
				.Where(x => x.ClassTypeId == request.ClassTypeId && x.EndingDate > DateTime.Today)
				.ToListAsync(cancellationToken);

			var classGenerator = new ClassGenerator(_dbContext);
			foreach (var timetable in timetablesList)
			{
				await classGenerator.DeleteAllFutureClassesByTimetableIdAsync(timetable, cancellationToken);
				timetable.EndingDate = DateTime.Today;
			}

			await _dbContext.SaveChangesAsync(cancellationToken);
			var classTypeDto = classTypeEntity.Adapt<ClassTypeDto>();

			return classTypeDto;
		}
	}
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class DeleteClassByIdHandler : IRequestHandler<DeleteClassByIdCommand, CalendarClassDto>
	{
		private readonly CalendarContext _dbContext;

		public DeleteClassByIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<CalendarClassDto> Handle(DeleteClassByIdCommand request, CancellationToken cancellationToken)
		{
			var classEntity = await _dbContext
				.Classes
				.SingleOrDefaultAsync(m => m.ClassId == request.ClassId, cancellationToken);

			_dbContext.Remove(classEntity);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);
			
			var deletedReservationsForClass = await _dbContext.Reservations
				.Where(x => x.ClassId == request.ClassId)
				.ToListAsync(cancellationToken);

			foreach (var reservation in deletedReservationsForClass)
				_ = _dbContext.Remove(reservation);

			_ = _dbContext.SaveChangesAsync(cancellationToken);

			var classDto = classEntity.Adapt<CalendarClassDto>();

			return classDto;
		}
	}
}
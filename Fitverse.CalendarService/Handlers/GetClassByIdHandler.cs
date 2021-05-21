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
	public class GetClassByIdHandler : IRequestHandler<GetClassByIdQuery, CalendarClassDto>
	{
		private readonly CalendarContext _dbContext;

		public GetClassByIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<CalendarClassDto> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
		{
			var classEntity = await _dbContext
				.Classes
				.SingleOrDefaultAsync(m => m.ClassId == request.ClassId, cancellationToken);

			if (classEntity is null)
				throw new NullReferenceException($"Class [ClassId: {request.ClassId} not found]");

			var classDto = classEntity.Adapt<CalendarClassDto>();

			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == classEntity.ClassTypeId, cancellationToken);

			if (classTypeEntity is null)
				throw new NullReferenceException($"ClassType [ClassTypeId: {classEntity.ClassTypeId} not found]");

			classDto.ClassName = classTypeEntity.Name;
			classDto.Description = classTypeEntity.Description;
			classDto.Limit = classTypeEntity.Limit;

			var reservations = await _dbContext
				.Reservations
				.Where(m => m.ClassId == classDto.ClassId)
				.ToListAsync(cancellationToken);

			classDto.Reservations = new List<int>();
			
			foreach (var reservation in reservations)
				classDto.Reservations.Add(reservation.ReservationId);

			return classDto;
		}
	}
}
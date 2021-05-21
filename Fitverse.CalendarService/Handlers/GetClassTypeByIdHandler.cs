using System;
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
	public class GetClassTypeByIdHandler : IRequestHandler<GetClassTypeByIdQuery, ClassTypeDto>
	{
		private readonly CalendarContext _dbContext;

		public GetClassTypeByIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ClassTypeDto> Handle(GetClassTypeByIdQuery request, CancellationToken cancellationToken)
		{
			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == request.ClassTypeId && !m.IsDeleted, cancellationToken);

			if (classTypeEntity is null)
				throw new NullReferenceException($"ClassType [ClassTypeId: {request.ClassTypeId} not found]");

			var classTypeDto = classTypeEntity.Adapt<ClassTypeDto>();

			return classTypeDto;
		}
	}
}
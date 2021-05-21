using System;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class AddClassTypeHandler : IRequestHandler<AddClassTypeCommand, ClassTypeDto>
	{
		private readonly CalendarContext _dbContext;

		public AddClassTypeHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ClassTypeDto> Handle(AddClassTypeCommand request, CancellationToken cancellationToken)
		{
			var name = request.NewClassType.Name;
			var classTypeEntity = request.NewClassType.Adapt<ClassType>();

			_ = await _dbContext.AddAsync(classTypeEntity, cancellationToken);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var newClassTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.Name == name, cancellationToken);
			if (newClassTypeEntity is null)
				throw new NullReferenceException("Failed to add class type. Try again");

			var newClassTypeDto = newClassTypeEntity.Adapt<ClassTypeDto>();

			return newClassTypeDto;
		}
	}
}
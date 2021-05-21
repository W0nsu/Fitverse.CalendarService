using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Validators;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class EditClassTypeHandler : ControllerBase, IRequestHandler<EditClassTypeCommand, ClassTypeDto>
	{
		private readonly CalendarContext _dbContext;

		public EditClassTypeHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ClassTypeDto> Handle(EditClassTypeCommand request, CancellationToken cancellationToken)
		{
			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == request.ClassTypeId, cancellationToken);

			if (classTypeEntity is null)
			{
				throw new NullReferenceException($"ClassType [classTypeId: {request.ClassTypeId}] not found");
			}

			var editedClassType = request.NewClassTypeEntity;
			var nameBeforeChange = classTypeEntity.Name;

			editedClassType.ApplyTo(classTypeEntity, ModelState);

			var nameAfterChange = classTypeEntity.Name;
			// var validator =
			// 	new ClassTypeValidator(_dbContext, new Tuple<string, string>(nameBeforeChange, nameAfterChange));
			// var validationResult = await validator.ValidateAsync(classTypeEntity, cancellationToken);
			//
			// if (!validationResult.IsValid)
			// 	throw new ValidationException(validationResult.Errors.ToList());

			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var patchedClassTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == request.ClassTypeId, cancellationToken);

			if (patchedClassTypeEntity is null)
			{
				throw new NullReferenceException(
					$"Failed to fetch patched class type [ClassTypeId: {request.ClassTypeId}]");
			}

			var patchedClassTypeDto = patchedClassTypeEntity.Adapt<ClassTypeDto>();

			return patchedClassTypeDto;
		}
	}
}
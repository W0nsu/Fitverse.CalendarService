using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Fitverse.CalendarService.Commands
{
	public class EditClassTypeCommand : IRequest<ClassTypeDto>
	{
		public EditClassTypeCommand(int classTypeId, JsonPatchDocument<ClassType> classTypeEntity)
		{
			ClassTypeId = classTypeId;
			NewClassTypeEntity = classTypeEntity;
		}

		public int ClassTypeId { get; }

		public JsonPatchDocument<ClassType> NewClassTypeEntity { get; }
	}
}
using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class DeleteClassByIdCommandValidator : AbstractValidator<DeleteClassByIdCommand>
	{
		public DeleteClassByIdCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.ClassId)
				.GreaterThan(0);
				
			RuleFor(x => x.ClassId)
				.Must(id => dbContext.Classes.Any(m => m.ClassId == id))
				.WithMessage(x => $"Class [ClassId: {x.ClassId}] not found.");
		}
	}
}
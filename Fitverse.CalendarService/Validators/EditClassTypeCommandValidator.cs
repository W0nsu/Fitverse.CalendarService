using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class EditClassTypeCommandValidator : AbstractValidator<EditClassTypeCommand>
	{
		public EditClassTypeCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.ClassTypeId)
				.GreaterThan(0);
				
			RuleFor(x => x.ClassTypeId)
				.Must(id => dbContext.ClassTypes.Any(m => m.ClassTypeId == id))
				.WithMessage(x => $"Class type [ClassTypeId: {x.ClassTypeId}] not found");
		}
	}
}
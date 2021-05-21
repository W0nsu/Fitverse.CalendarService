using System.Data;
using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class AddClassTypeCommandValidator : AbstractValidator<AddClassTypeCommand>
	{
		public AddClassTypeCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.NewClassType.Name)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(30);
				
			RuleFor(x => x.NewClassType.Name)
				.Must(n => !dbContext.ClassTypes.Any(c => c.Name == n))
				.WithMessage(x => $"Class type name [ClassTypeName: {x.NewClassType.Name}] already in use.");

			RuleFor(x => x.NewClassType.Description)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(255);

			RuleFor(x => x.NewClassType.Limit)
				.NotEmpty()
				.GreaterThan(0);
			
			RuleFor(x => x.NewClassType.Room)
				.MinimumLength(3)
				.MaximumLength(30);

			RuleFor(x => x.NewClassType.Duration)
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
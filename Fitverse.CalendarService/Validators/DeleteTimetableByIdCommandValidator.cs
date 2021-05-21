using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class DeleteTimetableByIdCommandValidator : AbstractValidator<DeleteTimetableByIdCommand>
	{
		public DeleteTimetableByIdCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.TimetableId)
				.GreaterThan(0);
				
			RuleFor(x => x.TimetableId)
				.Must(id => dbContext.Classes.Any(m => m.TimetableId == id))
				.WithMessage(x => $"Timetable [TimetableId: {x.TimetableId}] not found");
		}
	}
}
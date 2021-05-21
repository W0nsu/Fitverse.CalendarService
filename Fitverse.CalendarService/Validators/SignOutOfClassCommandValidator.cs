using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class SignOutOfClassCommandValidator : AbstractValidator<SignOutOfClassCommand>
	{
		public SignOutOfClassCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.ReservationId)
				.GreaterThan(0);
				
			RuleFor(x => x.ReservationId)
				.Must(id => dbContext.Reservations.Any(m => m.ReservationId == id))
				.WithMessage(x => $"Reservation [ReservationId: {x.ReservationId}] not found.");
		}
	}
}
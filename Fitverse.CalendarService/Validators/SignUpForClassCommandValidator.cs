using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class SignUpForClassCommandValidator : AbstractValidator<SignUpForClassCommand>
	{
		public SignUpForClassCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.Reservation.ClassId)
				.GreaterThan(0);

			RuleFor(x => x.Reservation.ClassId)
				.Must(id => dbContext.Classes.Any(m => m.ClassId == id))
				.WithMessage(x => $"Classes [ClassId: {x.Reservation.ClassId}] doesn't exists.");

			RuleFor(x => x.Reservation.MemberId)
				.GreaterThan(0);

			RuleFor(x => x.Reservation.MemberId)
				.Must(id => dbContext.Members.Any(m => m.MemberId == id))
				.WithMessage(x => $"Member [MemberId: {x.Reservation.MemberId}] doesn't exists.");

			RuleFor(x => x.Reservation)
				.Must(reservation =>
					!dbContext.Reservations.Any(r =>
						r.ClassId == reservation.ClassId && r.MemberId == reservation.MemberId))
				.WithMessage(x =>
					$"Member [MemberId: {x.Reservation.MemberId}] is already registered to Classes [ClassId: {x.Reservation.ClassId}]");
		}
	}
}
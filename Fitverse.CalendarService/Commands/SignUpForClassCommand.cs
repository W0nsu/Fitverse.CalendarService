using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class SignUpForClassCommand : IRequest<ReservationDtoSetter>
	{
		public SignUpForClassCommand(ReservationDtoSetter reservation)
		{
			Reservation = reservation;
		}

		public ReservationDtoSetter Reservation { get; }
	}
}
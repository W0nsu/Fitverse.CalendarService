using Fitverse.CalendarService.Dtos;
using MediatR;

namespace Fitverse.CalendarService.Commands
{
	public class SignOutOfClassCommand : IRequest<ReservationDtoSetter>
	{
		public SignOutOfClassCommand(int reservationId)
		{
			ReservationId = reservationId;
		}

		public int ReservationId { get; }
	}
}
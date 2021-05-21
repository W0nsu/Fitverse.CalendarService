using Fitverse.CalendarService.Models;

namespace Fitverse.CalendarService.Interfaces
{
	public interface ISignUpForClassSender
	{
		public void AddReservation(Reservation reservation);
	}
}
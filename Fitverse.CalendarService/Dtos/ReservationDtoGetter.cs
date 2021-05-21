namespace Fitverse.CalendarService.Dtos
{
	public class ReservationDtoGetter
	{
		public int ReservationId { get; private set; }
		public int ClassId { get; set; }
		public int MemberId { get; set; }

		public MemberDto Member { get; set; }
	}
}
using System;

namespace Fitverse.CalendarService.Dtos
{
	public class TimetableDto
	{
		public int TimetableId { get; private set; }
		public int ClassTypeId { get; set; }
		public string ClassTypeName { get; set; }
		public DateTime StartingDate { get; set; }
		public DateTime EndingDate { get; set; }
		public DateTime ClassesStartingTime { get; set; }
		public int PeriodType { get; set; }
	}
}
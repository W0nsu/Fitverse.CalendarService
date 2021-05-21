using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.CalendarService.Models
{
	public class Timetable
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TimetableId { get; set; }

		[Required] public int ClassTypeId { get; set; }

		[Required] [Column(TypeName = "Date")] public DateTime StartingDate { get; set; }

		[Required] [Column(TypeName = "Date")] public DateTime EndingDate { get; set; }

		[Required] public DateTime ClassesStartingTime { get; set; }

		[Required] public int PeriodType { get; set; }
	}
}
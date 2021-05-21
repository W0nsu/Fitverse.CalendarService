using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.CalendarService.Models
{
	public class CalendarClass
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ClassId { get; set; }

		[Required] public int ClassTypeId { get; set; }
		
		[Required] public string ClassName { get; set; }

		[Required] public DateTime Date { get; set; }

		[Required] public DateTime StartingTime { get; set; }

		[Required] public DateTime EndingTime { get; set; }

		[Required] public int TimetableId { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.CalendarService.Models
{
	public class ClassType
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ClassTypeId { get; set; }
		
		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string Name { get; set; }
		
		[Required]
		[MaxLength(255)]
		public string Description { get; set; }

		[Required]
		public int Limit { get; set; }
		
		[MinLength(3)]
		[MaxLength(30)]
		public string Room { get; set; }

		[Required]
		public int Duration { get; set; }

		[Required]
		public bool IsDeleted { get; set; }
	}
}
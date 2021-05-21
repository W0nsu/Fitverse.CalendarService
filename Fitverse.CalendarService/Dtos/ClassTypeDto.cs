namespace Fitverse.CalendarService.Dtos
{
	public class ClassTypeDto
	{
		public int ClassTypeId { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Limit { get; set; }
		public string Room { get; set; }
		public int Duration { get; set; }
	}
}
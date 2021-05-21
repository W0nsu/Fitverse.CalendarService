using System;
using System.Linq;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.Shared.Helpers;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class AddTimetableCommandValidator : AbstractValidator<AddTimetableCommand>
	{
		public AddTimetableCommandValidator(CalendarContext dbContext)
		{
			RuleFor(x => x.NewTimetableDto.ClassTypeId)
				.GreaterThan(0);
				
			RuleFor(x => x.NewTimetableDto.ClassTypeId)
				.Must(id => dbContext.ClassTypes.Any(m => m.ClassTypeId == id))
				.WithMessage(x => $"ClassType [ClassTypeId: {x.NewTimetableDto.ClassTypeId}] doesn't exists.");

			RuleFor(x => x.NewTimetableDto.StartingDate)
				.NotEmpty();
				
			RuleFor(x => x.NewTimetableDto.StartingDate)
				.Must(startingDate => startingDate >= DateTime.Now)
				.WithMessage($"Select a date later than {DateTime.Now.ToShortDateString()}");

			RuleFor(x => x.NewTimetableDto.EndingDate)
				.NotEmpty();
				
			RuleFor(x => x.NewTimetableDto.EndingDate)
				.Must(endingDate => endingDate > DateTime.Now)
				.WithMessage("Select a date later than Timetable starting date");

			RuleFor(x => x.NewTimetableDto.ClassesStartingTime)
				.NotEmpty();

			RuleFor(x => x.NewTimetableDto.PeriodType)
				.NotEmpty();
				
			RuleFor(x => x.NewTimetableDto.PeriodType)
				.Must(periodType => Enum.IsDefined(typeof(PeriodType), periodType))
				.WithMessage("Available PeriodType: " + Enum.GetNames(typeof(PeriodType))
					.Aggregate("", (current, value) => current + value + ", "));
		}
	}
}
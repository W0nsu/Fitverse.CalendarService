using System;
using System.Linq;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Models;
using FluentValidation;

namespace Fitverse.CalendarService.Validators
{
	public class ClassTypeValidator : AbstractValidator<ClassType>
	{
		// public ClassTypeValidator(CalendarContext dbContext, Tuple<string, string> nameBeforeAndAfterChange)
		// {
		// 	string nameBeforeChange, nameAfterChange;
		// 	(nameBeforeChange, nameAfterChange) = nameBeforeAndAfterChange;
		//
		// 	RuleFor(x => x.Name)
		// 		.NotEmpty()
		// 		.MinimumLength(3)
		// 		.MaximumLength(30);
		//
		// 	if (nameBeforeChange != nameAfterChange)
		// 	{
		// 		RuleFor(x => x.Name)
		// 			.Must(name => !dbContext.ClassTypes.Any(m => m.Name == name))
		// 			.WithMessage(x => $"Name [Name: {x.Name}] already in use");
		// 	}
		//
		// 	RuleFor(x => x.Description)
		// 		.MaximumLength(255);
		//
		// 	RuleFor(x => x.Limit)
		// 		.NotEmpty()
		// 		.GreaterThan(0);
		//
		// 	RuleFor(x => x.Room)
		// 		.MinimumLength(3)
		// 		.MaximumLength(30);
		//
		// 	RuleFor(x => x.Duration)
		// 		.NotEmpty()
		// 		.GreaterThan(0);
		// }
	}
}
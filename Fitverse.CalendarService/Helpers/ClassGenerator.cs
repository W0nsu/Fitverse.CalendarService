using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Models;
using Fitverse.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Helpers
{
	public class ClassGenerator
	{
		private readonly CalendarContext _dbContext;

		public ClassGenerator(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddClassesForTimetableAsync(Timetable timetable, CancellationToken cancellationToken = default)
		{
			var timetableStartingDate = timetable.StartingDate;

			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(m => m.ClassTypeId == timetable.ClassTypeId, cancellationToken);

			var classesStartingTime = timetable.ClassesStartingTime;
			var classDate = timetableStartingDate;

			var classesDto = new CalendarClassDto {Date = classDate};
			while (classDate >= timetableStartingDate && classDate <= timetable.EndingDate)
			{
				if (classDate == timetableStartingDate)
				{
					classesDto.ClassName = classTypeEntity.Name;
					classesDto.ClassTypeId = timetable.ClassTypeId;
					classesDto.StartingTime = classesStartingTime;
					classesDto.EndingTime = classesStartingTime.AddMinutes(classTypeEntity.Duration);
					classesDto.TimetableId = timetable.TimetableId;
				}
				else
					classesDto.Date = classDate;

				var classEntity = classesDto.Adapt<CalendarClass>();
				_ = await _dbContext.AddAsync(classEntity, cancellationToken);

				classDate = CalculateNextClassDate(classDate, timetable);
			}

			_ = await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAllClassesByTimetableIdAsync(Timetable timetable, CancellationToken cancellationToken = default)
		{
			var classesList = await _dbContext
				.Classes
				.Where(m => m.TimetableId == timetable.TimetableId)
				.ToListAsync(cancellationToken);

			foreach (var calendarClass in classesList)
			{
				await DeleteReservationsAsync(calendarClass, cancellationToken);
				_dbContext.Remove(calendarClass);
			}

			_ = await _dbContext.SaveChangesAsync(cancellationToken);
		}
		
		public async Task DeleteAllFutureClassesByTimetableIdAsync(Timetable timetable, CancellationToken cancellationToken = default)
		{
			var classesList = await _dbContext
				.Classes
				.Where(m => m.TimetableId == timetable.TimetableId && m.Date > DateTime.Now)
				.ToListAsync(cancellationToken);

			foreach (var calendarClass in classesList)
			{
				await DeleteReservationsAsync(calendarClass, cancellationToken);
				_dbContext.Remove(calendarClass);
			}

			_ = await _dbContext.SaveChangesAsync(cancellationToken);
		}

		private async Task DeleteReservationsAsync(CalendarClass calendarClass,
			CancellationToken cancellationToken = default)
		{
			var reservationsForClass = await _dbContext
				.Reservations
				.Where(m => m.ClassId == calendarClass.ClassId)
				.ToListAsync(cancellationToken);

			foreach (var reservation in reservationsForClass)
				_dbContext.Remove(reservation);

			_ = await _dbContext.SaveChangesAsync(cancellationToken);
		}

		private DateTime CalculateNextClassDate(DateTime classDate, Timetable timetable)
		{
			switch ((PeriodType) timetable.PeriodType)
			{
				case PeriodType.Day:
					return classDate.AddDays(1);
				case PeriodType.Month:
					return classDate.AddMonths(1);
				case PeriodType.Year:
					return classDate.AddYears(1);
				case PeriodType.Week:
					return classDate.AddDays(7);
				default:
					throw new ArgumentException(
						$"Timetable period [period: {timetable.PeriodType}] do not exists.");
			}
		}
	}
}
using System;
using System.Collections.Generic;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Helpers;
using MediatR;

namespace Fitverse.CalendarService.Queries
{
	public class GetClassByDateQuery : IRequest<List<CalendarClassDto>>
	{
		public GetClassByDateQuery(DateRange dateRange)
		{
			DateRange = dateRange;
		}

		public DateRange DateRange { get; }
	}
}
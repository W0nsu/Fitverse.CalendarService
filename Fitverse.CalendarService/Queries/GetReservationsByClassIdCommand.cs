using System.Collections.Generic;
using Fitverse.CalendarService.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Fitverse.CalendarService.Queries
{
	public class GetReservationsByClassIdCommand : IRequest<List<ReservationDtoGetter>>
	{
		public GetReservationsByClassIdCommand(int classId)
		{
			ClassId = classId;
		}
		
		public int ClassId { get; }
	}
}
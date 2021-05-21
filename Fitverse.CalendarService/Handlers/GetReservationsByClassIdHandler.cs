using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Queries;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class GetReservationsByClassIdHandler : IRequestHandler<GetReservationsByClassIdCommand, List<ReservationDtoGetter>>
	{
		private readonly CalendarContext _dbContext;

		public GetReservationsByClassIdHandler(CalendarContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<List<ReservationDtoGetter>> Handle(GetReservationsByClassIdCommand request, CancellationToken cancellationToken)
		{
			var reservationsList = await _dbContext
				.Reservations
				.Where(x => x.ClassId == request.ClassId)
				.ToListAsync(cancellationToken);
			var reservationsDtoList = new List<ReservationDtoGetter>();

			foreach (var reservation in reservationsList)
			{
				var reservationDto = reservation.Adapt<ReservationDtoGetter>();
				var member = await _dbContext
					.Members
					.FirstAsync(x => x.MemberId == reservation.MemberId, cancellationToken);
				reservationDto.Member = member.Adapt<MemberDto>();
				reservationsDtoList.Add(reservationDto);
			}

			reservationsDtoList = reservationsDtoList.OrderBy(x => x.Member.SurName).ToList();
			return reservationsDtoList;
		}
	}
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class SignOutOfClassHandler : IRequestHandler<SignOutOfClassCommand, ReservationDtoSetter>
	{
		private readonly CalendarContext _dbContext;
		private readonly ISignOutOfClassSender _signOutOfClassSender;

		public SignOutOfClassHandler(CalendarContext dbContext, ISignOutOfClassSender signOutOfClassSender)
		{
			_dbContext = dbContext;
			_signOutOfClassSender = signOutOfClassSender;
		}

		public async Task<ReservationDtoSetter> Handle(SignOutOfClassCommand request, CancellationToken cancellationToken)
		{
			var reservationEntity = await _dbContext
				.Reservations
				.SingleOrDefaultAsync(m => m.ReservationId == request.ReservationId, cancellationToken);

			_ = _dbContext.Remove(reservationEntity);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);
			
			_signOutOfClassSender.DeleteReservation(reservationEntity.ReservationId);

			var reservationDto = reservationEntity.Adapt<ReservationDtoSetter>();

			return reservationDto;
		}
	}
}
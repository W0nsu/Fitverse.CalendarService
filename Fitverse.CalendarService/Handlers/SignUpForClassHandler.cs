using System;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.CalendarService.Commands;
using Fitverse.CalendarService.Data;
using Fitverse.CalendarService.Dtos;
using Fitverse.CalendarService.Interfaces;
using Fitverse.CalendarService.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Handlers
{
	public class SignUpForClassHandler : IRequestHandler<SignUpForClassCommand, ReservationDtoSetter>
	{
		private readonly CalendarContext _dbContext;
		private readonly ISignUpForClassSender _signUpForClassSender;
		private int _classTypeParticipantsLimit;

		public SignUpForClassHandler(CalendarContext dbContext, ISignUpForClassSender signUpForClassSender)
		{
			_dbContext = dbContext;
			_signUpForClassSender = signUpForClassSender;
		}

		public async Task<ReservationDtoSetter> Handle(SignUpForClassCommand request, CancellationToken cancellationToken)
		{
			var reservationEntity = request.Reservation.Adapt<Reservation>();

			if (await IsLimitExceeded(reservationEntity, cancellationToken))
			{
				throw new ArgumentException(
					$"Limit of participants for this classes has been exceeded [Limit: {_classTypeParticipantsLimit}]");
			}

			_ = await _dbContext.AddAsync(reservationEntity, cancellationToken);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var newReservation = await _dbContext
				.Reservations
				.SingleOrDefaultAsync(
					m => m.ClassId == reservationEntity.ClassId && m.MemberId == reservationEntity.MemberId,
					cancellationToken);

			if (newReservation is null)
				throw new NullReferenceException("Failed to sign in for classes. Try again");
			
			_signUpForClassSender.AddReservation(newReservation);

			var newReservationDto = newReservation.Adapt<ReservationDtoSetter>();

			return newReservationDto;
		}

		private async Task<bool> IsLimitExceeded(Reservation newReservation,
			CancellationToken cancellationToken = default)
		{
			var classEntity = await _dbContext
				.Classes
				.SingleOrDefaultAsync(x => x.ClassId == newReservation.ClassId, cancellationToken);

			var classTypeEntity = await _dbContext
				.ClassTypes
				.SingleOrDefaultAsync(x => x.ClassTypeId == classEntity.ClassTypeId, cancellationToken);

			_classTypeParticipantsLimit = classTypeEntity.Limit;

			var numberOfReservations = await _dbContext
				.Reservations
				.CountAsync(x => x.ClassId == newReservation.ClassId, cancellationToken);

			return numberOfReservations >= _classTypeParticipantsLimit;
		}
	}
}
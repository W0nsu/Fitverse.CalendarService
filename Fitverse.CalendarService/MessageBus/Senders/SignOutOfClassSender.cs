using System;
using Fitverse.CalendarService.Interfaces;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.CalendarService.MessageBus.Senders
{
	public class SignOutOfClassSender : ISignOutOfClassSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public SignOutOfClassSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}
		
		public void DeleteReservation(int reservationId)
		{
			var exchangeConfig = new Tuple<string, string>("classes", "signOutOfClass");
			SendEventHandler.SendEvent(reservationId, _rabbitMqOptions, exchangeConfig);
		}
	}
}
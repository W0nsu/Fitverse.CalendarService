using System;
using Fitverse.CalendarService.Interfaces;
using Fitverse.CalendarService.Models;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.CalendarService.MessageBus.Senders
{
	public class SignUpForClassSender : ISignUpForClassSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public SignUpForClassSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}
		
		public void AddReservation(Reservation reservation)
		{
			var exchangeConfig = new Tuple<string, string>("classes", "signUpForClass");
			SendEventHandler.SendEvent(reservation, _rabbitMqOptions, exchangeConfig);
		}
	}
}
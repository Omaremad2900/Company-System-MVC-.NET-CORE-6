using Demo.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public static class EmailSettings
	{
		private static readonly Emailconfig _emailConfig;

		static EmailSettings()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			_emailConfig = new Emailconfig();
			configuration.GetSection("EmailSettings").Bind(_emailConfig);
		}

		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Password);
			Client.Send("oabouregaila@gmail.com", email.To, email.subject, email.body);

		}
	}
}
     

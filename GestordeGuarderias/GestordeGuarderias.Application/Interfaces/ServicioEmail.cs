using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IServicioEmail
    {
        Task EnviarEmail(string destinatario, string asunto, string contenido);
    }

    public class ServicioEmail : IServicioEmail
    {
        private readonly IConfiguration _configuration;

        public ServicioEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmail(string destinatario, string asunto, string contenido)
        {
            var host = _configuration["CONFIGURACIONES_EMAIL:HOST"];
            var puerto = Convert.ToInt32(_configuration["CONFIGURACIONES_EMAIL:PUERTO"]);
            var correo = _configuration["CONFIGURACIONES_EMAIL:EMAIL"]; 
            var contraseña = _configuration["CONFIGURACIONES_EMAIL:PASSWORD"];   

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
            {
                throw new InvalidOperationException("Faltan datos de configuración para el envío de correo.");
            }

            var client = new SmtpClient(host)
            {
                Port = puerto,
                Credentials = new NetworkCredential(correo, contraseña),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(correo),
                Subject = asunto,
                Body = contenido,
                IsBodyHtml = false
            };

            mailMessage.To.Add(destinatario);

            await client.SendMailAsync(mailMessage);
        }
    }
}

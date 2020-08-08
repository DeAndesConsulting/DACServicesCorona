using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DACServices.Repositories.Service
{
	public class ServiceMailingRepository
	{
		public async Task DacSendMail(string to, string subject, string body)
		{
			MailMessage mail = new MailMessage();

			foreach (var m in to.Split(';'))
				mail.To.Add(new MailAddress(m));

			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = true;

			//Por el momento no se permite adjuntar archivos
			//if (model.Attachment != null && model.Attachment.ContentLength > 0)
			//{
			//	var attachment = new Attachment(model.Attachment.InputStream, model.Attachment.FileName);
			//	mail.Attachments.Add(attachment);
			//}

			try
			{
				using (var smtp = new SmtpClient())
				{
					await smtp.SendMailAsync(mail);
				}
			}
			catch (SmtpFailedRecipientsException smtpFailed)
			{
				throw smtpFailed;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}

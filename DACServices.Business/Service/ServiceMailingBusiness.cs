using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServiceMailingBusiness
	{
		private ServiceMailingRepository serviceMailingRepository = new ServiceMailingRepository();

		public async Task DacSendMail(string to, string subject, string body)
		{
			try
			{
				await serviceMailingRepository.DacSendMail(to, subject, body);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
	}
}

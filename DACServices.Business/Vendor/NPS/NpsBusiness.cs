using DACServices.Repositories.Vendor.NpsVendor.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Vendor.NPS
{
	public class NpsBusiness
	{
		public string CreateForm(string merchantTransactionReference, string amount)
		{
			try
			{
				NpsPaymentForm npsPaymentForm = new NpsPaymentForm();
				return npsPaymentForm.CrearFormulario(merchantTransactionReference, amount);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

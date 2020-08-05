using DACServices.Entities;
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
		public string CreateForm(int idPayment, int monto, int producto, int cuotas, tbPaymentDetail paymentDetail)
		{
			try
			{
				string merchantTransactionReference = "CARGO-010000" + idPayment.ToString();
				string amount = monto.ToString();
				string product = producto.ToString();
				string numPayments = cuotas.ToString();

				NpsPaymentForm npsPaymentForm = new NpsPaymentForm();
				return npsPaymentForm.CrearFormulario(merchantTransactionReference, amount, product, numPayments, paymentDetail);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

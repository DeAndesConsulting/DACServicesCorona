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
				//Cuando se pase a test/pro se tiene que eliminar esto.
				string merchantTransactionReference = "CARGO-010000-" + idPayment.ToString();
				string amount = monto.ToString();
				string product = producto.ToString();
				string numPayments = cuotas.ToString();

				NpsPaymentFormRepository npsPaymentForm = new NpsPaymentFormRepository();
				return npsPaymentForm.CrearFormulario(merchantTransactionReference, amount, product, numPayments, paymentDetail);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool ConsultarEstadoDelPago(string psp_TransactionId, int psp_merchantTxReference, 
			tbPaymentDetail paymentDetail)
		{
			string transactionId = psp_TransactionId;
			string merchantTxReference = psp_merchantTxReference.ToString();

			NpsPaymentFormRepository npsPaymentForm = new NpsPaymentFormRepository();
			return npsPaymentForm.ConsultarEstadoDelPago(transactionId, merchantTxReference, paymentDetail);
		}
	}
}

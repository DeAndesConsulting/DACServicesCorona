using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class ServicePaymentFormListenerModel
	{
		public string psp_TransactionId { get; set; }
		public string psp_MerchTxRef { get; set; }
	}
}
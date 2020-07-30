using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class VendorPaymentFormModel
	{
		public string merchantTransactionReference { get; set; }
		public string amount { get; set; }
		public string urlPaymentForm { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Vendor.NpsVendor.Model
{
	public class NpsModel
	{
		public string psp_TransactionId { get; set; }
		public string psp_MerchTxRef { get; set; }
		public string psp_Amount { get; set; }
		public string psp_Product { get; set; }
		public string psp_NumPayments { get; set; }
		public string psp_CustomerId { get; set; }
		public string psp_AccountID { get; set; }
		public string psp_PaymentMethodTag { get; set; }
		public string psp_QueryCriteria { get; set; }
		public string psp_QueryCriteriaId { get; set; }
	}
}

using DACServices.Api.Models;
using DACServices.Business.Service;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
    public class ServicePaymentFormListenerController : ApiController
    {
		ILog log = LogManager.GetLogger(typeof(ServicePaymentFormListenerController));
		//public object Post([FromUri]ServicePaymentFormListenerModel model)

		[HttpPost]
		public object Post([FromUri] string psp_TransactionId, string psp_MerchTxRef)
		{
			try
			{
				//?psp_TransactionId=4556209&psp_MerchTxRef=1
				ServicePaymentFormListenerModel model = new ServicePaymentFormListenerModel();
				model.psp_MerchTxRef = psp_MerchTxRef;
				model.psp_TransactionId = psp_TransactionId;

				int idPayment = Int32.Parse(model.psp_MerchTxRef);

				ServicePaymentFormListenerBusiness paymentFormListenerBusiness = 
					new ServicePaymentFormListenerBusiness(model.psp_TransactionId, idPayment);

				var payment = paymentFormListenerBusiness.ActualizarPayment();
				//// now redirect
				var response = Request.CreateResponse(HttpStatusCode.Moved);
				response.Headers.Location = new Uri("http://www.ole.com.ar");
				return response;
			}
			catch (Exception ex)
			{
				log.Error("Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}
    }
}

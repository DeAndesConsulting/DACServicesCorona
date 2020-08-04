using DACServices.Api.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
	[Authorize]
    public class ServicePaymentFormController : ApiController
    {
		ILog log = LogManager.GetLogger(typeof(ServicePaymentFormController));
		
		public object Post(ServicePaymentFormModel model)
		{
			try
			{
				var sarasa = model.Cuotas;

				model.idPayment = "1";
				model.urlForm = "http://sarasa";

				return Request.CreateResponse(HttpStatusCode.Created, model);
			}
			catch (Exception ex)
			{
				log.Error("Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw new Exception("Error Post, ver log para mas detalle: " + ex.Message);
			}
		}
    }
}

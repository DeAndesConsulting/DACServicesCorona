using DACServices.Api.Models;
using DACServices.Business.Service;
using DACServices.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
	public class ServicePaymentController : ApiController
	{
		private ILog log = LogManager.GetLogger(typeof(ServicePaymentController));
		private ServicePaymentBusiness servicePaymentBusiness = new ServicePaymentBusiness();

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				ServicePaymentFormModel model = new ServicePaymentFormModel();

				bool func(tbPayment x) => x.pay_id == id;
				var paymentList = servicePaymentBusiness.Read(func) as List<tbPayment>;

				if (paymentList.Count > 0)
				{
					var result = model.ConvertPaymentToModel(paymentList.FirstOrDefault());
					return Request.CreateResponse(HttpStatusCode.Created, result);
				}
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, new Exception("El objeto no existe."));
			}
			catch (Exception ex)
			{
				log.Error(ex);
				if (ex.InnerException != null)
					log.Error("Inner ex: " + ex.InnerException.Message);
				throw ex;
			}
		}
	}
}

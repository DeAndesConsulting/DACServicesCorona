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
	[Authorize]
    public class ServicePaymentFormController : ApiController
    {
		ILog log = LogManager.GetLogger(typeof(ServicePaymentFormController));

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				ServicePaymentFormModel model = new ServicePaymentFormModel();

				ServicePaymentFormBusiness servicePaymentFormBusiness = new ServicePaymentFormBusiness(id);
				return model.ConvertListPaymentToListModel(servicePaymentFormBusiness.RecuperarPagosPorUsuario());
			}
			catch (Exception ex)
			{
				log.Error("Error get: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}

		[HttpPost]
		public object Post(ServicePaymentFormModel model)
		{
			try
			{
				tbPayment payment = model.ConvertModelToPayment();
				ServicePaymentFormModel response;

				if (payment.pay_id != 0)
					response = model.ConvertPaymentToModel(this.RecoverPayment(payment));
				else
					response = model.ConvertPaymentToModel(this.CreatePayment(payment));

				if (!string.IsNullOrEmpty(response.url_formulario))
					return Request.CreateResponse(HttpStatusCode.Created, response);

				return Request.CreateResponse(HttpStatusCode.Conflict);
			}
			catch (Exception ex)
			{
				log.Error("Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw new Exception("Error Post, ver log para mas detalle: " + ex.Message);
			}
		}

		private tbPayment RecoverPayment(tbPayment payment)
		{
			try
			{
				ServicePaymentFormBusiness paymentFormBusiness = new ServicePaymentFormBusiness();
				return paymentFormBusiness.RecuperarFormularioDePago(payment.pay_id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPayment CreatePayment(tbPayment payment)
		{
			try
			{
				ServicePaymentFormBusiness paymentFormBusiness = new ServicePaymentFormBusiness(payment);
				return paymentFormBusiness.GenerarFormularioDePago();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}

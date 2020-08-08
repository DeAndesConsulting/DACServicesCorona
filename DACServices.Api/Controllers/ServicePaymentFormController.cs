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
	[Authorize]
    public class ServicePaymentFormController : ApiController
    {
		ILog log = LogManager.GetLogger(typeof(ServicePaymentFormController));

		public object Post(ServicePaymentFormModel model)
		{
			try
			{
				ServicePaymentFormBusiness paymentFormBusiness =
					new ServicePaymentFormBusiness(model.UserName, model.Descripcion, model.Monto, model.Producto,
						model.Cuotas, model.Email);

				var payment = paymentFormBusiness.GenerarFormularioDePago();

				model.IdPayment = payment.pay_id;
				model.UrlForm = payment.pay_url_formulario;
				model.CantidadEmailsEnviados = payment.pay_cantidad_mails_enviados;

				if (!string.IsNullOrEmpty(model.UrlForm))
					return Request.CreateResponse(HttpStatusCode.Created, model);

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
    }
}

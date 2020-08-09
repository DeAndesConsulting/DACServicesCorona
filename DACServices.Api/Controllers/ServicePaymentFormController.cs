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
				model.Payments = new List<tbPayment>();

				ServicePaymentFormBusiness servicePaymentFormBusiness = new ServicePaymentFormBusiness(id);
				model.Payments = servicePaymentFormBusiness.RecuperarPagosPorUsuario();
				return model;
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
				tbPayment payment;

				if (model.Payment.pay_id != 0)
					payment = this.RecoverPayment(model);
				else
					payment = this.CreatePayment(model);

				model.Payment = payment;

				if (!string.IsNullOrEmpty(model.Payment.pay_url_formulario))
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

		private tbPayment RecoverPayment(ServicePaymentFormModel model)
		{
			try
			{
				int idUser = (int)model.IdUser;

				ServicePaymentFormBusiness paymentFormBusiness =
					new ServicePaymentFormBusiness(model.Payment.pay_id, idUser);

				var payment = paymentFormBusiness.RecuperarFormularioDePago();

				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPayment CreatePayment(ServicePaymentFormModel model)
		{
			try
			{
				int idUser = (int)model.IdUser;

				ServicePaymentFormBusiness paymentFormBusiness =
					new ServicePaymentFormBusiness(idUser, model.Payment.pay_concepto, model.Payment.pay_monto,
						model.Payment.pay_producto, model.Payment.pay_cuotas, model.Payment.pay_email_to);

				var payment = paymentFormBusiness.GenerarFormularioDePago();

				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}

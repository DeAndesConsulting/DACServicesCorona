using DACServices.Api.Models;
using DACServices.Business.Service;
using DACServices.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
    public class ServicePaymentFormListenerController : ApiController
    {
		ILog log = LogManager.GetLogger(typeof(ServicePaymentFormListenerController));

		[HttpPost]
		public object Post(HttpRequestMessage request)
		{
			try
			{
				string[] parametros = request.Content.ReadAsStringAsync().Result.Split('&');
				ServicePaymentFormListenerModel model = new ServicePaymentFormListenerModel();
				tbPayment paymentResult;

				foreach (var p in parametros)
				{
					string[] value = p.Split('=');
					if (p.Contains("psp_TransactionId"))
						model.psp_TransactionId = value[1];

					if (p.Contains("psp_MerchTxRef"))
						model.psp_MerchTxRef = value[1];
				}

				//Este parseo se hace unicamente porque tengo como merchTxRef CARG0-00001-idPayment
				string[] arregloTemp = model.psp_MerchTxRef.Split('-');
				string idPaymentTemp = arregloTemp[2];
				int idPayment = Int32.Parse(idPaymentTemp);

				//Esta parseo deberia ir, se deja el de arriba solo par avanzar el desarrollo
				//int idPayment = Int32.Parse(model.psp_MerchTxRef);

				//Antes de hacer un hit en NPS consulto el estado del pago, si es true es pq ya se actualizo
				tbPayment paymentStatus = this.GetPayment(idPayment);

				//consulto si no se proceso, si ya fue procesado no hiteo nps y retorno el valor (ya procesado)
				if (paymentStatus.pst_id == (int)EnumPaymentStatus.PENDIENTE)
				{
					//Si es falso, deberia analizar si hubo algun error de tarjeta y demas
					//Ese desarrollo esta pendiente, lo de abajo se deberia ejecutar igual para acualizar o no
					//Si se tuvo un error en el proceso de pago, consultar seria al pedo

					ServicePaymentFormListenerBusiness paymentFormListenerBusiness =
						new ServicePaymentFormListenerBusiness(model.psp_TransactionId, idPayment);

					paymentResult = paymentFormListenerBusiness.ActualizarPayment();
				}
				else
				{
					paymentResult = paymentStatus;
				}

				//now redirect
				var response = Request.CreateResponse(HttpStatusCode.Moved);
				response.Headers.Location =
					new Uri(ConfigurationManager.AppSettings["RESPONSE_SERVER"] + paymentResult.pst_id.ToString());
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

		private tbPayment GetPayment(int id)
		{
			try
			{
				bool func(tbPayment x) => x.pay_id == id;
				ServicePaymentBusiness business = new ServicePaymentBusiness();
				var result = business.Read(func) as List<tbPayment>;
				return result.FirstOrDefault();
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

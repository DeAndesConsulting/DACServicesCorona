﻿using DACServices.Api.Models;
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

		[HttpPost]
		public object Post(HttpRequestMessage request)
		{
			try
			{
				string[] parametros = request.Content.ReadAsStringAsync().Result.Split('&');
				ServicePaymentFormListenerModel model = new ServicePaymentFormListenerModel();

				foreach(var p in parametros)
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

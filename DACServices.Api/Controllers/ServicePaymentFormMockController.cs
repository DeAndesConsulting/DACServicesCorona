using DACServices.Api.Models;
using DACServices.Entities;
using DACServices.Mock;
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
    public class ServicePaymentFormMockController : ApiController
    {
		private ILog log = LogManager.GetLogger(typeof(ServicePaymentFormMockController));
		//private List<tbPayment> listaMock = PaymentFormMock.Instancia().GetList();

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				ServicePaymentFormModel model = new ServicePaymentFormModel();

				var lista = PaymentFormMock.Instancia().Get(id);
				var result = model.ConvertListPaymentToListModel(lista);

				if (result != null)
					return Request.CreateResponse(HttpStatusCode.OK, result);

				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
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
				var createdObj = PaymentFormMock.Instancia().Create(payment);
				var result = model.ConvertPaymentToModel(createdObj);

				if (result.id != 0)
					return Request.CreateResponse(HttpStatusCode.Created, result);

				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}

		[HttpPut]
		public object Put(tbPayment obj)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}

		[HttpDelete]
		public object Delete(int id)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}

	}
}

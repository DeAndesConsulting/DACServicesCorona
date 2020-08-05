using DACServices.Api.Models;
using DACServices.Business.Vendor.NPS;
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
    public class VendorPaymentFormController : ApiController
    {
		private ILog log = LogManager.GetLogger(typeof(VendorPaymentFormController));

		public object Post(VendorPaymentFormModel model)
		{
			try
			{
				NpsBusiness npsBusiness = new NpsBusiness();
				string result = npsBusiness.CreateForm(0, 1000, 14, 6, new Entities.tbPaymentDetail());

				if (result != null)
				{
					model.urlPaymentForm = result;
					return Request.CreateResponse(HttpStatusCode.Created, model);
				}
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception" + ex.InnerException.Message);
				throw ex;
			}
		}
    }
}

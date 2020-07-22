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
    public class ServiceUsuarioMenuController : ApiController
    {
		private ILog log = LogManager.GetLogger(typeof(ServiceUsuarioMenuController));

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				UsuarioMenuModel model = new UsuarioMenuModel();
				return model.GetMenu();
			}
			catch (Exception ex)
			{
				log.Error("Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Error inner: " + ex.InnerException.Message);
				throw;
			}
		}
	}
}

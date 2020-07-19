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
    public class ServiceSecurityUsuarioController : ApiController
    {
		private ILog log = LogManager.GetLogger(typeof(ServiceUsuarioController));
		private List<tbUsuario> listaUsuariosMock = UsuarioMock.Instancia().GetListaUsuarios();

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				//Retorno del mock de datos
				var result = UsuarioMock.Instancia().GetUsuario(id);

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

		[HttpGet]
		public object Get()
		{
			try
			{
				var result = listaUsuariosMock;

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
		public object Post(tbUsuario usuario)
		{
			try
			{
				//Se utiliza mock de datos
				var result = UsuarioMock.Instancia().CreateUsuario(usuario);

				if (result.usu_id != 0)
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
		public object Put(tbUsuario usuario)
		{
			try
			{
				//Se utiliza mock de datos
				var result = UsuarioMock.Instancia().Update(usuario);

				if (result != null)
					return Request.CreateResponse(HttpStatusCode.OK, result);

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

		[HttpDelete]
		public object Delete(int id)
		{
			try
			{
				tbUsuario usuario = new tbUsuario() { usu_id = id };

				//Mock de usuarios
				var result = UsuarioMock.Instancia().Delete(usuario);

				if (result != null)
					return Request.CreateResponse(HttpStatusCode.OK, result);

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
	}
}

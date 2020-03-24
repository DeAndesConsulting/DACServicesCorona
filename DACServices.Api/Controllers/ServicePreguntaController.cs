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
	public class ServicePreguntaController : ApiController
	{
		private ILog log = LogManager.GetLogger(typeof(RelevamientoController));
		ServicePreguntaBusiness preguntaBusiness;
		HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				Func<tbPregunta, bool> func = x => x.pre_id == id;

				preguntaBusiness = new ServicePreguntaBusiness();
				var result = preguntaBusiness.Read(func);

				if (result != null)
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

		[HttpGet]
		public object Get()
		{
			try
			{
				preguntaBusiness = new ServicePreguntaBusiness();
				var result = preguntaBusiness.Read();

				if (result != null)
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

		[HttpPost]
		public object Post([FromBody]tbPregunta pregunta)
		{
			log.Info("- Ingreso -");
			try
			{
				preguntaBusiness = new ServicePreguntaBusiness();
				var result = (tbPregunta)preguntaBusiness.Create(pregunta);

				if (result != null)
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
		public object Put([FromBody]tbPregunta pregunta)
		{
			try
			{
				preguntaBusiness = new ServicePreguntaBusiness();
				var result = preguntaBusiness.Update(pregunta);

				if (result != null)
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

		[HttpDelete]
		public object Delete(int id)
		{
			try
			{
				Func<tbPregunta, bool> func = x => x.pre_id == id;
				preguntaBusiness = new ServicePreguntaBusiness();
				tbPregunta pregunta = ((List<tbPregunta>)preguntaBusiness.Read(func)).FirstOrDefault();
				var result = preguntaBusiness.Delete(pregunta);

				if (result != null)
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
	}
}

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
    public class ServicePacienteController : ApiController
    {
		private ILog log = LogManager.GetLogger(typeof(RelevamientoController));
		ServicePacienteBusiness pacienteBusiness;

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				Func<tbPaciente, bool> func = x => x.pac_id == id;

				pacienteBusiness = new ServicePacienteBusiness();
				return pacienteBusiness.Read(func);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		[HttpGet]
		public object Get()
		{
			try
			{
				pacienteBusiness = new ServicePacienteBusiness();
				return pacienteBusiness.Read();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public object Post([FromBody]tbPaciente paciente)
		{
			try
			{
				pacienteBusiness = new ServicePacienteBusiness();
				return pacienteBusiness.Create(paciente);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPut]
		public object Put([FromBody]tbPaciente paciente)
		{
			try
			{
				pacienteBusiness = new ServicePacienteBusiness();
				return pacienteBusiness.Update(paciente);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpDelete]
		public object Delete(int id)
		{
			try
			{
				Func<tbPaciente, bool> func = x => x.pac_id == id;
				pacienteBusiness = new ServicePacienteBusiness();
				tbPaciente paciente = ((List<tbPaciente>)pacienteBusiness.Read(func)).FirstOrDefault();
				return pacienteBusiness.Delete(paciente);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

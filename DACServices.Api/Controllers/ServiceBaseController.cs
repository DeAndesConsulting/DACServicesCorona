using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
    public class ServiceBaseController<B, E> : ApiController
		where B: class, new()
		where E: class, new()
    {
		//private ILog log = LogManager.GetLogger(typeof(RelevamientoController));
		//B business;

		//[HttpGet]
		//public object Get(int id)
		//{
		//	try
		//	{
		//		Func<E, bool> func = x => x.pre_id == id;

		//		preguntaBusiness = new ServicePreguntaBusiness();
		//		return preguntaBusiness.Read(func);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}


		//[HttpGet]
		//public object Get()
		//{
		//	try
		//	{
		//		preguntaBusiness = new ServicePreguntaBusiness();
		//		return preguntaBusiness.Read();
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		//[HttpPost]
		//public object Post([FromBody]tbPregunta pregunta)
		//{
		//	try
		//	{
		//		preguntaBusiness = new ServicePreguntaBusiness();
		//		return preguntaBusiness.Create(pregunta);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		//[HttpPut]
		//public object Put([FromBody]tbPregunta pregunta)
		//{
		//	try
		//	{
		//		preguntaBusiness = new ServicePreguntaBusiness();
		//		return preguntaBusiness.Update(pregunta);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		//[HttpDelete]
		//public object Delete(int id)
		//{
		//	try
		//	{
		//		Func<tbPregunta, bool> func = x => x.pre_id == id;
		//		preguntaBusiness = new ServicePreguntaBusiness();
		//		tbPregunta pregunta = ((List<tbPregunta>)preguntaBusiness.Read(func)).FirstOrDefault();
		//		return preguntaBusiness.Delete(pregunta);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}
	}
}

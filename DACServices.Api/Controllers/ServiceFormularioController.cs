using DACServices.Api.Models;
using DACServices.Business.Service;
using DACServices.Entities;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
	public class ServiceFormularioController : ApiController
	{
		private ILog log = LogManager.GetLogger(typeof(ServiceFormularioController));

		[HttpGet]
		public object Get()
		{
			try
			{
				ServiceCuestionarioBusiness serviceCuestionarioBusiness = new ServiceCuestionarioBusiness();
				var result = serviceCuestionarioBusiness.Read();

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
		public object Post(FormularioModel model)
		{
			tbCuestionario cuestionarioResult = null;
			try
			{
				//Asigno nulos a las listas de paciente y cuestionario ya que modifican la salidan y nunca
				//Se utilizan como entrada
				model.Paciente.tbCuestionario = null;
				model.Cuestionario.tbCuestionarioPregunta = null;

				//Inserto el request
				ServiceRequestBusiness serviceRequestBusiness = new ServiceRequestBusiness();
				tbRequest request = new tbRequest();

				Func<tbRequest, bool> predicado = p => p.req_codigo == model.CodigoRequest;
				request = ((List<tbRequest>)serviceRequestBusiness.Read(predicado)).FirstOrDefault();

				if (request != null)
				{
					if (request.req_body_response != null)
					{
						model = JsonConvert.DeserializeObject<FormularioModel>(request.req_body_response);
						log.Info("Retorno el modelo almacenado en DB_DACS: " + JsonConvert.SerializeObject(model));
						return Request.CreateResponse(HttpStatusCode.Created, model);
					}
					else
					{
						//Tengo que procesar todo nuevemente
						model = JsonConvert.DeserializeObject<FormularioModel>(request.req_body_request);
						cuestionarioResult = InsertFormulario(model);
					}
				}
				else
				{
					//Insert bd local
					request = new tbRequest()
					{
						req_fecha_request = DateTime.Now,
						req_fecha_response = null,
						req_body_request = JsonConvert.SerializeObject(model),
						req_estado = false,
						req_codigo = model.CodigoRequest
					};
					//Creo objeto en base local
					serviceRequestBusiness.Create(request);

					cuestionarioResult = InsertFormulario(model);
				}

				if (cuestionarioResult != null)
				{
					//Actualizo tabla request
					request.req_fecha_response = DateTime.Now;
					request.req_body_response = JsonConvert.SerializeObject(model);
					request.req_estado = true;
					serviceRequestBusiness.Update(request);

					//Actualizo el modelo con los datos del paciente almacenado
					model.Paciente.pac_dni = (this.ObtenerPaciente(model.Paciente.pac_dni)).pac_dni;

					//Actualizo el modelo con los datos del cuestionario insertado
					var cuestionarioResponse = (tbCuestionario)cuestionarioResult;
					model.Cuestionario.cue_id = cuestionarioResponse.cue_id;
					model.Cuestionario.pac_id = model.Paciente.pac_id;
					model.ListaCuestionarioPregunta.ForEach(x => x.cue_id = cuestionarioResponse.cue_id);

					return Request.CreateResponse(HttpStatusCode.Created, model);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "No se inserto registros.");
				}
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}

		private tbCuestionario InsertFormulario(FormularioModel model)
		{
			try
			{
				tbCuestionario cuestionario = new tbCuestionario();
				
				//Cargo los datos del cuestionario
				cuestionario.cue_fecha = model.Cuestionario.cue_fecha;
				cuestionario.cue_diagnostico = model.Cuestionario.cue_diagnostico;
				cuestionario.cue_latitud = model.Cuestionario.cue_latitud.Replace(',','.');
				cuestionario.cue_longitud = model.Cuestionario.cue_longitud.Replace(',', '.');

				//Asigno al cuestionario los datos del paciente
				if (model.Paciente.pac_dni != 0)
				{
					//Agrego el paciente al cuestionario, obtengo el dni si existe y sino cargo los datos para insertar
					tbPaciente paciente = this.ObtenerPaciente(model.Paciente.pac_dni);
					if (paciente != null)
						cuestionario.pac_id = paciente.pac_id;
					else
					{
						cuestionario.tbPaciente = new tbPaciente
						{
							pac_dni = model.Paciente.pac_dni,
							pac_nombre = model.Paciente.pac_nombre,
							pac_apellido = model.Paciente.pac_apellido,
							pac_genero = model.Paciente.pac_genero
						};
					}
				}

				//Asigno las respuestas al cuestionario
				if (model.ListaCuestionarioPregunta.Count() > 1)
				{
					cuestionario.tbCuestionarioPregunta = new List<tbCuestionarioPregunta>();
					tbCuestionarioPregunta cuestionarioPregunta;

					foreach (var obj in model.ListaCuestionarioPregunta)
					{
						cuestionarioPregunta = new tbCuestionarioPregunta()
						{
							pre_id = obj.pre_id,
							cpr_respuesta = obj.cpr_respuesta,
							cpr_comentario = obj.cpr_comentario
						};
						cuestionario.tbCuestionarioPregunta.Add(cuestionarioPregunta);
					}
				}

				ServiceCuestionarioBusiness servicioCuestionarioBusiness = new ServiceCuestionarioBusiness();
				var result = servicioCuestionarioBusiness.Create(cuestionario);
				return (tbCuestionario)result;
			}
			catch (Exception ex)
			{
				log.Error("Mensaje de Error: " + ex.Message);
				if (ex.InnerException != null)
					log.Error("Inner exception: " + ex.InnerException.Message);
				throw ex;
			}
		}



		public tbPaciente ObtenerPaciente(int dni)
		{
			try
			{
				Func<tbPaciente, bool> func = x => x.pac_dni == dni;
				ServicePacienteBusiness pacienteBusiness = new ServicePacienteBusiness();
				return ((List<tbPaciente>)pacienteBusiness.Read(func)).FirstOrDefault();
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

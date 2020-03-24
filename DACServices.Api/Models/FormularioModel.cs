using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DACServices.Api.Models
{
	public class FormularioModel
	{
		public tbPaciente Paciente { get; set; }
		public tbCuestionario Cuestionario { get; set; }
		public List<tbCuestionarioPregunta> ListaCuestionarioPregunta { get; set; }
		public string CodigoRequest { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class ServicePaymentFormModel
	{
		public string UserName { get; set; }
		public string Descripcion { get; set; }
		public int Monto { get; set; }
		public int Producto { get; set; }
		public int Cuotas { get; set; }
		public string Email { get; set; }
		public int IdPayment { get; set; }
		public string UrlForm { get; set; }
		public int CantidadEmailsEnviados { get; set; }
		public bool EstadoDelPago { get; set; }
	}
}
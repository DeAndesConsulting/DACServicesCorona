using DACServices.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class ServicePaymentModel : ServicePaymentFormModel
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string comercio { get; set; }

		public ServicePaymentModel ConvertPaymentToModel(tbPayment payment)
		{
			try
			{
				ServicePaymentFormModel modelForm = base.ConvertPaymentToModel(payment);

				this.id = modelForm.id;
				this.usuario = modelForm.usuario;
				this.concepto = modelForm.concepto;
				this.monto = modelForm.monto;
				this.producto = modelForm.producto;
				this.cuotas = modelForm.cuotas;
				this.url_formulario = modelForm.url_formulario;
				this.emails = modelForm.emails;
				this.emails_enviados = modelForm.emails_enviados;
				this.estado_pago = modelForm.estado_pago;
				this.fecha = modelForm.fecha;

				return this;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
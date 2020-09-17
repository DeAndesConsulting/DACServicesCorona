using DACServices.Business.Service;
using DACServices.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class ServicePaymentFormModel
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string usuario { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Include)]
		public int estado_pago { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string concepto { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string monto { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string producto { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int cuotas { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string url_formulario { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string url_formulario_custom { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string emails { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Include)]
		public int emails_enviados { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string informacion_adicional { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string fecha { get; set; }

		public tbPayment ConvertModelToPayment()
		{
			try
			{
				tbPayment payment = new tbPayment()
				{
					pay_id = id,
					usu_id = getIdUsuerByName(this.usuario),
					pay_concepto = this.concepto,
					pay_monto = ConvertStringToInt(this.monto),
					pay_producto = GetProductoCode(this.producto),
					pay_cuotas = cuotas,
					pay_email_to = emails
				};

				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public ServicePaymentFormModel ConvertPaymentToModel(tbPayment payment, string userName)
		{
			try
			{
				ServicePaymentFormModel model = new ServicePaymentFormModel()
				{
					id = payment.pay_id,
					usuario = userName,
					estado_pago = payment.pst_id,
					concepto = payment.pay_concepto,
					monto = this.ConvertIntToString(payment.pay_monto),
					producto = this.ConvertProductoCode(payment.pay_producto),
					cuotas = payment.pay_cuotas,
					url_formulario_custom = payment.pay_url_formulario_custom,
					emails = payment.pay_email_to,
					emails_enviados = payment.pay_cantidad_mails_enviados,
					informacion_adicional = payment.pay_informacion_adicional,
					fecha = payment.pay_fecha.ToString("dd/MM/yyyy HH:mm:ss")
				};

				return model;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public ServicePaymentFormModel ConvertPaymentToModel(tbPayment payment)
		{
			try
			{
				ServicePaymentFormModel model = new ServicePaymentFormModel()
				{
					id = payment.pay_id,
					usuario = this.getUsuerNameByIdUser(payment.usu_id),
					estado_pago = payment.pst_id,
					concepto = payment.pay_concepto,
					monto = this.ConvertIntToString(payment.pay_monto),
					producto = this.ConvertProductoCode(payment.pay_producto),
					cuotas = payment.pay_cuotas,
					url_formulario = payment.pay_url_formulario,
					url_formulario_custom = payment.pay_url_formulario_custom,
					emails = payment.pay_email_to,
					emails_enviados = payment.pay_cantidad_mails_enviados,
					informacion_adicional = payment.pay_informacion_adicional,
					fecha = payment.pay_fecha.ToString("dd/MM/yyyy HH:mm:ss")
				};

				return model;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<ServicePaymentFormModel> ConvertListPaymentToListModel(List<tbPayment> listaPayment)
		{
			try
			{
				List<ServicePaymentFormModel> lista = new List<ServicePaymentFormModel>();
				var element = this.ConvertPaymentToModel(listaPayment.FirstOrDefault()) as ServicePaymentFormModel;

				foreach(var obj in listaPayment)
					lista.Add(this.ConvertPaymentToModel(obj, element.usuario));

				return lista;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#region Convertir model to tbPayment
		private int getIdUsuerByName(string userName)
		{
			try
			{
				ServiceUsuarioBusiness serviceUsuarioBusiness = new ServiceUsuarioBusiness();
				bool func(tbUsuario x) => x.usu_usuario == userName;
				var usuario = (serviceUsuarioBusiness.Read(func) as List<tbUsuario>).FirstOrDefault();
				return usuario.usu_id;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private int ConvertStringToInt(string montoString)
		{
			try
			{
				//Casteo a double el string del monto, con cultura invariante para que tome el "." como decimales
				double monto = double.Parse(montoString, System.Globalization.CultureInfo.InvariantCulture);
				
				//Calculo la cantidad de decimales que voy a tomar
				double digitos = (double)(Math.Pow(10.0, (double)2));

				//Lo convierto en entero con los dos decimales tomados
				int temp = (int)(digitos * monto);

				//Lo convierto nuevamente a double original, tomando los dos digitos
				double doubleDosDecimales = (double)temp / digitos;
				
				//Se toman los dos ultimos decimales, si vienen mas, se van a ignorar
				//double doubleDosDecimales = Convert.ToDouble(string.Format("{0:F2}", monto));
				int montoEntero = Convert.ToInt32(doubleDosDecimales * 100);

				return montoEntero;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		private int GetProductoCode(string codigoProducto)
		{
			try
			{
				//Se estan retornando los codigo de productos de NPS. Ojo por si se modifica de vendor.
				if(!string.IsNullOrEmpty(codigoProducto))
				{
					switch(codigoProducto)
					{
						case "VI": return 14;
					}
				}
				throw new Exception("El codigo de tarjeta no existe");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Convertir tbPayment to model
		private string getUsuerNameByIdUser(int idUser)
		{
			try
			{
				ServiceUsuarioBusiness serviceUsuarioBusiness = new ServiceUsuarioBusiness();
				bool func(tbUsuario x) => x.usu_id == idUser;
				var usuario = (serviceUsuarioBusiness.Read(func) as List<tbUsuario>).FirstOrDefault();
				return usuario.usu_usuario;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string ConvertIntToString(int monto)
		{
			try
			{
				double montoDecimal =  (double)monto / (double)100;
				return montoDecimal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string ConvertProductoCode(int codigoProducto)
		{
			try
			{
				//Se estan retornando los codigo de productos de NPS. Ojo por si se modifica de vendor.
				if (codigoProducto != 0)
				{
					switch (codigoProducto)
					{
						case 14: return "VI";
					}
				}
				throw new Exception("El codigo de tarjeta no existe");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion
	}
}
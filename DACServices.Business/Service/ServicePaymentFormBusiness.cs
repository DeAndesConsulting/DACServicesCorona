using DACServices.Business.Vendor.NPS;
using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServicePaymentFormBusiness
	{
		private int _idUser { get; set; }
		private string _concepto { get; set; }
		private int _monto { get; set; }
		private int _producto { get; set; }
		private int _cuotas { get; set; }
		private string _email { get; set; }

		private ServiceUsuarioBusiness usuarioBusiness = new ServiceUsuarioBusiness();
		private ServicePaymentBusiness paymentBusiness = new ServicePaymentBusiness();
		private ServicePaymentDetailBusiness paymentDetailBusiness = new ServicePaymentDetailBusiness();
		private ServiceMailingBusiness serviceMailingBusiness = new ServiceMailingBusiness();

		public ServicePaymentFormBusiness()
		{ }

		public ServicePaymentFormBusiness(tbPayment payment)
		{
			_idUser = payment.usu_id;
			_concepto = payment.pay_concepto;
			_monto = payment.pay_monto;
			_producto = payment.pay_producto;
			_cuotas = payment.pay_cuotas;
			_email = payment.pay_email_to;
		}

		public ServicePaymentFormBusiness(int idUser)
		{
			_idUser = idUser;
		}

		public List<tbPayment> RecuperarPagosPorUsuario()
		{
			try
			{
				return this.GetPaymentsByUserId();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public tbPayment RecuperarFormularioDePago(int idPayment)
		{
			try
			{
				tbPayment payment = null;
				bool func(tbPayment x) => x.pay_id == idPayment;
				List<tbPayment> resultList = paymentBusiness.Read(func) as List<tbPayment>;

				if (resultList.Count() > 0)
					payment = resultList.FirstOrDefault();

				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public tbPayment GenerarFormularioDePago()
		{
			try
			{
				//Creo el registro en la tabla payment
				tbPayment payment = this.CreatePayment();

				//Creo el detalle del pago
				tbPaymentDetail paymentDetail = this.CreatePaymentDetail(payment);

				//Creo el formulario de pago
				NpsBusiness npsBusiness = new NpsBusiness();
				string urlPaymentForm = npsBusiness.CreateForm(payment.pay_id, _monto, _producto, _cuotas, paymentDetail);

				//Actualizo payment detail
				paymentDetail = this.UpdatePaymentDetail(paymentDetail);

				//Actualizo el payment con el formulario de pagos si no retorno nulo
				if (!string.IsNullOrEmpty(urlPaymentForm))
				{
					//Valido que NPS retorne una url, sino es porque arrojo error y lo dejo registrado en tbPayment
					//y no envío el mail
					if (urlPaymentForm.Contains("http"))
					{
						//Actualizo payment con el formulario de pago
						payment.pay_url_formulario = urlPaymentForm;
						payment.pst_id = (int)EnumPaymentStatus.PENDIENTE;
						payment = this.UpdatePayment(payment);

						//Asigno la url del formulario de pago de la app cliente
						string vueAppUrl = ConfigurationManager.AppSettings["VUE_APP_URL_FORM"] + "/" + payment.pay_id.ToString();
						//envio e-mail
						this.SendEmamil(vueAppUrl);

						//Actualizo payment con el contador de envio de mails
						payment.pay_cantidad_mails_enviados++;
						payment = this.UpdatePayment(payment);
					}
					else
					{
						//Actualizo payment con el error que arrojo NPS
						payment.pay_informacion_adicional = urlPaymentForm;
						payment.pst_id = (int)EnumPaymentStatus.ERROR;
						payment = this.UpdatePayment(payment);
					}
				}

				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPaymentDetail UpdatePaymentDetail(tbPaymentDetail paymentDetail)
		{
			try
			{
				paymentDetail = paymentDetailBusiness.Update(paymentDetail) as tbPaymentDetail;
				return paymentDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPaymentDetail CreatePaymentDetail(tbPayment payment)
		{
			try
			{
				//Creo el registro del request en payment details
				tbPaymentDetail paymentDetail = new tbPaymentDetail()
				{
					pay_id = payment.pay_id,
					pde_operacion = "SOLICITUD_FORMULARIO",
					pde_fecha = DateTime.Now
				};
				paymentDetail = paymentDetailBusiness.Create(paymentDetail) as tbPaymentDetail;
				return paymentDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPayment CreatePayment()
		{
			try
			{
				//Creo el registro de pago
				tbPayment payment = new tbPayment()
				{
					usu_id = _idUser,
					pst_id = (int)EnumPaymentStatus.PROCESANDO,
					pay_concepto = _concepto,
					pay_monto = _monto,
					pay_producto = _producto,
					pay_cuotas = _cuotas,
					pay_email_to = _email,
					pay_fecha = DateTime.Now
				};
				payment = paymentBusiness.Create(payment) as tbPayment;
				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPayment UpdatePayment(tbPayment payment)
		{
			try
			{
				payment = paymentBusiness.Update(payment) as tbPayment;
				return payment;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private List<tbPayment> GetPaymentsByUserId()
		{
			try
			{
				bool func(tbPayment x) => x.usu_id == _idUser;
				return (paymentBusiness.Read(func) as List<tbPayment>).OrderByDescending(x => x.pay_id).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void SendEmamil(string urlPaymentForm)
		{
			try
			{
				string body = this.GetEmailBody(urlPaymentForm);
				string subject = string.Format("AP - Detalle Consumo");
				Task.Run(async () => await serviceMailingBusiness.DacSendMail(_email, subject, body));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string GetEmailBody(string urlPaymentForm)
		{
			try
			{
				string nombreFantasia = "Pizzeria los hijos de puta";
				string body = @"<h4>Hola {0}</h4>
					<br>
					<p>Gracias por venir a: {1}</p> 
					<br>
					<p>Detalle de su consumo: {2}</p> 
					<br>
					<p>El total a pagar es: $ {3}</p> 
					<br>
					<p>Haga click <a href='{4}'>acá</a> para pagar</p>
					<br>
					<img src='https://deandesconsulting.azurewebsites.net/Content/images/azure.png'/>
					<br>";

				return string.Format(body, _email, nombreFantasia, _concepto, ((double)_monto / 100).ToString(), urlPaymentForm);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}

using DACServices.Business.Vendor.NPS;
using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServicePaymentFormBusiness
	{
		private string _userName { get;}
		private string _descripcion { get; set; }
		private int _monto { get; set; }
		private int _producto { get; set; }
		private int _cuotas { get; set; }
		private string _email { get; set; }

		private ServiceUsuarioBusiness usuarioBusiness = new ServiceUsuarioBusiness();
		private ServicePaymentBusiness paymentBusiness = new ServicePaymentBusiness();
		private ServicePaymentDetailBusiness paymentDetailBusiness = new ServicePaymentDetailBusiness();

		public ServicePaymentFormBusiness(string userName, string descripcion, int monto, int producto, int cuotas, string email)
		{
			_userName = userName;
			_descripcion = descripcion;
			_monto = monto;
			_producto = producto;
			_cuotas = cuotas;
			_email = email;
		}

		public void GenerarFormularioDePago()
		{
			try
			{
				//Obtengo el usuario que solicito el formulario por el userName
				tbUsuario usuario = this.GetUserByName();

				//Creo el registro en la tabla payment
				tbPayment payment = this.CreatePayment(usuario);

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
					payment.pay_url_formulario = urlPaymentForm;
					this.UpdatePayment(payment);
				}
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

		private tbUsuario GetUserByName()
		{
			try
			{
				//Obengo el usuario por user name
				bool func(tbUsuario x) => x.usu_usuario == _userName;
				List<tbUsuario> listaUsuarios = usuarioBusiness.Read(func) as List<tbUsuario>;
				if (listaUsuarios != null)
					return listaUsuarios.FirstOrDefault();
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private tbPayment CreatePayment(tbUsuario usuario)
		{
			try
			{
				//Creo el registro de pago
				tbPayment payment = new tbPayment()
				{
					usu_id = usuario.usu_id,
					pay_email_to = _email,
					pay_estado = false,
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
	}
}

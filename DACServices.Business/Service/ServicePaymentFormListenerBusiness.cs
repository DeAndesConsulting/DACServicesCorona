using DACServices.Business.Vendor.NPS;
using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServicePaymentFormListenerBusiness
	{
		private string _transactionId { get; set; }
		private int _idPayment { get; set; }

		private ServicePaymentDetailBusiness paymentDetailBusiness = new ServicePaymentDetailBusiness();
		private ServicePaymentBusiness paymentBusiness = new ServicePaymentBusiness();

		public ServicePaymentFormListenerBusiness(string transactionId, int idPayment)
		{
			_transactionId = transactionId;
			_idPayment = idPayment;
		}

		public tbPayment ActualizarPayment()
		{
			string server = Dns.GetHostEntry(Dns.GetHostName()).HostName;// System.Environment.MachineName;
			string urlOk = "http://urlOK";

			//Inserto en payment detail el post del proveedor
			tbPaymentDetail paymentDetailPost = this.InsertPaymentDetail("POST_VENDOR");

			//Inserto en payment detail el request que voy a realizar a SimpleQueryTx
			tbPaymentDetail paymentDetailSimpleQuery = this.InsertPaymentDetail("SIMPLE_QUERY_TX");

			//Consulto el estado del pago
			NpsBusiness npsBusiness = new NpsBusiness();
			bool estadoDelPago = npsBusiness.ConsultarEstadoDelPago(_transactionId, _idPayment, paymentDetailSimpleQuery);

			//Actualizo tbPaymentDetails
			paymentDetailSimpleQuery = this.UpdatePaymentDetail(paymentDetailSimpleQuery);

			//Obtengo el registro del pago
			tbPayment payment = this.ObtengoPaymentPorId();

			//Actualizo registro del pago, con estado del pago
			payment.pay_estado_pago = estadoDelPago;
			payment = this.UpdatePayment(payment);

			return payment;
		}

		private tbPayment ObtengoPaymentPorId()
		{
			try
			{
				bool func(tbPayment x) => x.pay_id == _idPayment;
				var result = paymentBusiness.Read(func) as List<tbPayment>;
				return result.FirstOrDefault();
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

		private tbPaymentDetail InsertPaymentDetail(string OPERATION_TYPE)
		{
			try
			{
				tbPaymentDetail paymentDetail = new tbPaymentDetail()
				{
					pay_id = _idPayment,
					pde_operacion = OPERATION_TYPE,
					pde_vendor_response_id = _transactionId,
					pde_fecha = DateTime.Now
				};

				return paymentDetailBusiness.Create(paymentDetail) as tbPaymentDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}

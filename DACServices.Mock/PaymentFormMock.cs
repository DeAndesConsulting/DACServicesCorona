using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Mock
{
	public class PaymentFormMock
	{
		public static PaymentFormMock _instancia;
		List<tbPayment> _lista = null;
		tbPayment payment = null;

		protected PaymentFormMock()
		{
			_lista = new List<tbPayment>();

			payment = new tbPayment()
			{
				pay_id = 1,
				usu_id = 1,
				pay_concepto = "asd",
				pay_monto = 1000,
				pay_producto = 14,
				pay_cuotas = 1,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4554203&t=cb128f66f0fd088eb18e63557edfee4c",
				pay_email_to = "tec.lrodriguez@gmail.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = false,
				pay_fecha = Convert.ToDateTime("2020-08-05T17:32:44.453")
			};
			_lista.Add(payment);

			payment = new tbPayment()
			{
				pay_id = 2,
				usu_id = 1,
				pay_concepto = "asd",
				pay_monto = 2000,
				pay_producto = 14,
				pay_cuotas = 1,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4558369&t=313d1fd99e27f71afe2be2dbef511a5a",
				pay_email_to = "tec.lrodriguez@gmail.com;leonardorodriguez@outlook.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = false,
				pay_fecha = Convert.ToDateTime("2020-08-08T15:55:51.563")
			};
			_lista.Add(payment);

			payment = new tbPayment()
			{
				pay_id = 3,
				usu_id = 1,
				pay_concepto = "asd",
				pay_monto = 2115,
				pay_producto = 14,
				pay_cuotas = 1,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4558431&t=605ce7ec2365dd5c99bf20705b004cd0",
				pay_email_to = "tec.lrodriguez@gmail.com;leonardorodriguez@outlook.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = true,
				pay_fecha = Convert.ToDateTime("2020-08-08T16:25:56.013")
			};
			_lista.Add(payment);

			payment = new tbPayment()
			{
				pay_id = 4,
				usu_id = 1,
				pay_concepto = "asd",
				pay_monto = 2335,
				pay_producto = 14,
				pay_cuotas = 1,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4558462&t=bbc25065487ab2a16f54990746b42720",
				pay_email_to = "tec.lrodriguez@gmail.com;leonardorodriguez@outlook.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = false,
				pay_fecha = Convert.ToDateTime("2020-08-08T16:36:59.36")
			};
			_lista.Add(payment);

			payment = new tbPayment()
			{
				pay_id = 5,
				usu_id = 1,
				pay_concepto = "2 pintas",
				pay_monto = 1545,
				pay_producto = 14,
				pay_cuotas = 2,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4558685&t=42f1e14a16fef8fd006dd2ba89f957ad",
				pay_email_to = "tec.lrodriguez@gmail.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = true,
				pay_fecha = Convert.ToDateTime("2020-08-08T18:32:39.57")
			};
			_lista.Add(payment);

			payment = new tbPayment()
			{
				pay_id = 6,
				usu_id = 1,
				pay_concepto = "5 pintas",
				pay_monto = 1195,
				pay_producto = 14,
				pay_cuotas = 2,
				pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4560468&t=d149726e910712efdc92464e0a5f372a",
				pay_email_to = "tec.lrodriguez@gmail.com",
				pay_cantidad_mails_enviados = 1,
				pay_estado_pago = false,
				pay_fecha = Convert.ToDateTime("2020-08-09T18:38:17.04")
			};
			_lista.Add(payment);

		}

		public static PaymentFormMock Instancia()
		{
			if (_instancia == null)
				_instancia = new PaymentFormMock();
			return _instancia;
		}

		public List<tbPayment> Get(int id)
		{
			return _lista.Where(x => x.usu_id == id).OrderByDescending(x => x.pay_id).ToList();
		}

		public tbPayment Create(tbPayment obj)
		{
			obj.pay_id = _lista.OrderByDescending(x => x.pay_id).FirstOrDefault().pay_id + 1;
			obj.pay_url_formulario = "https://implementacion.nps.com.ar/psp3p_gen_form.php?id=4572320&t=68fdf1db8ab9b704290b392bf638236a";
			obj.pay_cantidad_mails_enviados = 1;
			obj.pay_estado_pago = false;
			obj.pay_fecha = DateTime.Now;

			_lista.Add(obj);
			return obj;
		}

		public tbPayment Delete(tbPayment obj)
		{
			var result = _lista.Where(x => x.pay_id == obj.pay_id).FirstOrDefault();
			if (result != null)
				_lista.Remove(result);
			return result;
		}

		public tbPayment Update(tbPayment obj)
		{
			tbPayment result = null;
			var tempObj = _lista.Where(x => x.pay_id == obj.pay_id).FirstOrDefault();
			if (tempObj != null)
			{
				_lista.Remove(tempObj);
				_lista.Add(obj);
				result = obj;
			}
			return result;
		}

	}
}

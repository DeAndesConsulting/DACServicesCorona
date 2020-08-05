using DACServices.Entities;
using DACServices.Repositories.NpsService;
using DACServices.Repositories.Service;
using DACServices.Repositories.Vendor.NpsVendor.Model;
using DACServices.Repositories.Vendor.NpsVendor.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Vendor.NpsVendor.Payment
{
	public class NpsPaymentForm
	{
		private tbPaymentDetail _paymentDetail = new tbPaymentDetail();
		private ServiceRequestPaymentVendorRepository requestPaymentVendorRepository = new ServiceRequestPaymentVendorRepository();
		private tbRequestPaymentVendor requestPaymentVendor = new tbRequestPaymentVendor();

		public string CrearFormulario(string merchantTransactionReference, string amount, string product, 
			string numPayments, tbPaymentDetail paymentDetail)
		{
			try
			{
				_paymentDetail = paymentDetail;

				NpsModel model = new NpsModel();
				model.psp_MerchTxRef = merchantTransactionReference;
				model.psp_Amount = amount;
				model.psp_Product = product;
				model.psp_NumPayments = numPayments;

				return this.ArmarRequest(model);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private string ArmarRequest(NpsModel model)
		{
			try
			{
				//var proxy = new NpsService.PaymentServicePlatformPortTypeClient("PaymentServicePlatformPort");

				System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

				PaymentServicePlatformPortTypeClient client =
					new PaymentServicePlatformPortTypeClient("PaymentServicePlatformPort");

				RespuestaStruct_PayOnLine_3p result = new RespuestaStruct_PayOnLine_3p();
				RequerimientoStruct_PayOnLine_3p query = new RequerimientoStruct_PayOnLine_3p();
				query = GetRequestPayOnline3p(model);

				this.AuditRequestResponse(JsonConvert.SerializeObject(query));
				result = client.PayOnLine_3p(query);
				this.AuditRequestResponse(JsonConvert.SerializeObject(result));

				_paymentDetail.pde_vendor_response_id = result.psp_TransactionId;
				_paymentDetail.pde_vendor_response_status = result.psp_ResponseCod;

				if (!string.IsNullOrEmpty(result.psp_FrontPSP_URL))
					return result.psp_FrontPSP_URL;
				else
					return result.psp_ResponseExtended;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static RequerimientoStruct_PayOnLine_3p GetRequestPayOnline3p(NpsModel model)
		{
			RequerimientoStruct_PayOnLine_3p req = new RequerimientoStruct_PayOnLine_3p();

			req.psp_Version = "2.2";
			req.psp_MerchantId = "arsa_smartcar";//<-- this will be parametrisable
			req.psp_TxSource = "WEB";
			req.psp_MerchTxRef = model.psp_MerchTxRef; //This will be guide numer
			req.psp_MerchOrderId = "003";
			req.psp_ReturnURL = "http://localhost:52991/Test/GetResult/";
			req.psp_FrmLanguage = "es_AR";//<-- this will be parameter (ex. cookies)
			//req.psp_FrmBackButtonURL = "https://returnurl/back.html";
			req.psp_Amount = model.psp_Amount;
			req.psp_NumPayments = model.psp_NumPayments;
			req.psp_Currency = "032"; //<-- this will be parametrisable
			req.psp_Country = "ARG";//<-- this will be parametrisable
			req.psp_Product = model.psp_Product; //14 is Visa, see nps documentation.
			req.psp_CustomerMail = "sample@aerolineas.com.ar";
			req.psp_MerchantMail = "sample@aerolineas.com.ar";
			req.psp_PurchaseDescription = "ENVIO DE CARTA";
			req.psp_PosDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// "2016-06-26 15:00:00";

			if (!string.IsNullOrEmpty(model.psp_CustomerId))
				req.psp_VaultReference = new VaultReference3pStruct() { CustomerId = model.psp_CustomerId };

			NpsEncryptFields operations = new NpsEncryptFields();
			req.psp_SecureHash = operations.EncriptarCampos(GetCatParamsPayOnline3p(req));

			return req;
		}

		public static string GetCatParamsPayOnline3p(RequerimientoStruct_PayOnLine_3p PayOnline3p)
		{
			return string.Concat(PayOnline3p.psp_Amount, PayOnline3p.psp_Country, PayOnline3p.psp_Currency,
				PayOnline3p.psp_CustomerMail, PayOnline3p.psp_FrmBackButtonURL, PayOnline3p.psp_FrmLanguage,
				PayOnline3p.psp_MerchOrderId, PayOnline3p.psp_MerchTxRef, PayOnline3p.psp_MerchantId,
				PayOnline3p.psp_MerchantMail, PayOnline3p.psp_NumPayments, PayOnline3p.psp_PosDateTime,
				PayOnline3p.psp_Product, PayOnline3p.psp_PurchaseDescription, PayOnline3p.psp_ReturnURL,
				PayOnline3p.psp_TxSource, PayOnline3p.psp_Version);
		}


		private void AuditRequestResponse(string json)
		{
			try
			{
				if (string.IsNullOrEmpty(requestPaymentVendor.rpv_request_body))
				{
					requestPaymentVendor.pde_id = _paymentDetail.pde_id;
					requestPaymentVendor.rpv_request_fecha = DateTime.Now;
					requestPaymentVendor.rpv_request_body = JsonConvert.SerializeObject(json);
					requestPaymentVendorRepository.Create(requestPaymentVendor);
				}
				else
				{
					requestPaymentVendor.rpv_response_fecha = DateTime.Now;
					requestPaymentVendor.rpv_response_body = JsonConvert.SerializeObject(json);
					requestPaymentVendorRepository.Update(requestPaymentVendor);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}

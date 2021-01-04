using DACServices.Entities.Vendor.Clases;
using DACServices.Mock;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
	public class ItrisPedidoMockController : ApiController
	{
		private List<ItrisPedidoEntity> listaMock = PedidosMock.Instancia().Get(1);

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				ItrisPedidoEntity model = new ItrisPedidoEntity();

				listaMock = PedidosMock.Instancia().Get(id);


				if (listaMock != null)
					return Request.CreateResponse(HttpStatusCode.OK, listaMock);

				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}



		[HttpPost]
		public object Post(ItrisPedidoEntity model)
		{
			try
			{
				var createdObj = PedidosMock.Instancia().Create(model);

				//var result = model.ConvertPaymentToModel(createdObj);

				if (createdObj.Pedido.ID != 0)
					return Request.CreateResponse(HttpStatusCode.Created, createdObj);

				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpPut]
		public object Put(ItrisPedidoEntity obj)
		{
			try
			{
				ItrisPedidoEntity model = new ItrisPedidoEntity();

				model = PedidosMock.Instancia().Update(obj);


				if (model != null)
					return Request.CreateResponse(HttpStatusCode.OK, model);

				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpDelete]
		public object Delete(ItrisPedidoEntity obj)
		{
			try
			{
				ItrisPedidoEntity model = new ItrisPedidoEntity();

				model = PedidosMock.Instancia().Delete(obj);


				if (model != null)
					return Request.CreateResponse(HttpStatusCode.OK, model);

				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}



﻿using DACServices.Api.Models;
using DACServices.Business.Service;
using DACServices.Entities;
using DACServices.Entities.Vendor.Clases;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DACServices.Api.Controllers
{
	public class ItrisPedidosController : ApiController
	{
		// GET: ItrisPedidos
		private ILog log = LogManager.GetLogger(typeof(ItrisPedidosController));
		private ServicePedidosBusiness servicePaymentBusiness = new ServicePedidosBusiness();

		[HttpGet]
		public object Get(int id)
		{
			try
			{
				ItrisPedidoEntity model = new ItrisPedidoEntity();
				bool func(ItrisPedidoEntity x) => x.Pedido.FK_ERP_EMPRESAS == id.ToString();
				var paymentList = servicePaymentBusiness.Read(func) as List<ItrisPedidoEntity>;
				if (paymentList.Count > 0)
				{
					return Request.CreateResponse(HttpStatusCode.Created, model);
				}
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, new Exception("El objeto no existe."));
			}
			catch (Exception ex)
			{
				log.Error(ex);
				if (ex.InnerException != null)
					log.Error("Inner ex: " + ex.InnerException.Message);
				throw ex;
			}
		}
	}
}


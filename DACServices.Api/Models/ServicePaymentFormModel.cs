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
		public int? IdUser { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public tbPayment Payment { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<tbPayment> Payments { get; set; }
	}
}
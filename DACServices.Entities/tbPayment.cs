using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbPayment_Metadata))]
	public partial class tbPayment
	{
	}

	[DataContract]
	public class tbPayment_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int pay_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int usu_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pay_concepto { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pay_monto { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pay_producto { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pay_cuotas { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pay_url_formulario { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pay_email_to { get; set; }

		[DataMember(EmitDefaultValue = true)]
		public int pay_cantidad_mails_enviados { get; set; }

		[DataMember(EmitDefaultValue = true)]
		public bool pay_estado_pago { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public System.DateTime pay_fecha { get; set; }
	}
}

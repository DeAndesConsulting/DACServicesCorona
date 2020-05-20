using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbUsuario_Metadata))]
	public partial class tbUsuario
	{
	}

	[DataContract]
	public class tbUsuario_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int usu_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string usu_usuario { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string usu_password { get; set; }

	}
}

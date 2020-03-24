using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbPaciente_Metadata))]
	public partial class tbPaciente
	{
	}

	[DataContract]
	public class tbPaciente_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int pac_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pac_dni { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pac_genero { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pac_nombre { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pac_apellido { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public virtual ICollection<tbCuestionario> tbCuestionario { get; set; }
	}
}

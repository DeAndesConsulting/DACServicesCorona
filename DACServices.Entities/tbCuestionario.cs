using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbCuestionario_Metadata))]
	public partial class tbCuestionario
	{
	}

	[DataContract]
	public class tbCuestionario_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int cue_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pac_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public System.DateTime cue_fecha { get; set; }

		[DataMember(EmitDefaultValue = true)]
		public bool cue_diagnostico { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string cue_latitud { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string cue_longitud { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public tbPaciente tbPaciente { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public virtual ICollection<tbCuestionarioPregunta> tbCuestionarioPregunta { get; set; }
	}
}

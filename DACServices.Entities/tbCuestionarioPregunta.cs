using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbCuestionarioPregunta_Metadata))]
	public partial class tbCuestionarioPregunta
	{
	}

	[DataContract]
	public class tbCuestionarioPregunta_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int cue_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int pre_id { get; set; }

		[DataMember(EmitDefaultValue = true)]
		public bool cpr_respuesta { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string cpr_comentario { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public virtual tbCuestionario tbCuestionario { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public virtual tbPregunta tbPregunta { get; set; }
	}
}

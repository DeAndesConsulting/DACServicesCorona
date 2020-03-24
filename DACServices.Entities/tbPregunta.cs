using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities
{
	[MetadataType(typeof(tbPregunta_Metadata))]
	public partial class tbPregunta
	{
	}

	[DataContract]
	public class tbPregunta_Metadata
	{
		[DataMember(EmitDefaultValue = false)]
		public int pre_id { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string pre_pregunta { get; set; }
		
		[DataMember(EmitDefaultValue = true)]
		public bool pre_respuesta_positiva { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public virtual ICollection<tbCuestionarioPregunta> tbCuestionarioPregunta { get; set; }
	}
}

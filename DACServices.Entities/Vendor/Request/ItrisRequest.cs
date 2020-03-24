using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Entities.Vendor.Request
{
	public class ItrisRequest
	{
		public string usersession { get; set; }
		public string @class { get; set; }
		public object data { get; set; }
	}
}

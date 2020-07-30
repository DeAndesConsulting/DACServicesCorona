using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Vendor.NpsVendor.Security
{
	public class NpsTypeEncrypt
	{
		public string GetEncriptacion(string parametros, string SECRETKEY)
		{
			return NpsHash.GenerarHMAC_SHA256(parametros, SECRETKEY);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Vendor.NpsVendor.Security
{
	public class NpsEncryptFields
	{
		private static string SECRETKEY = "vNLxAaYW2j1H9tWKtLJbUZF7S5m8rjrxaNhDY3keB01ZYGrbuZGtxMdJ2YxrUZSg";

		public string EncriptarCampos(string parametros)
		{
			//VIEJA ENCRIPTACIÓN
			//TipoEncriptacion tipoEncriptacion = new EncriptacionMD5();
			//NUEVA ENCRIPTACIÓN
			NpsTypeEncrypt tipoEncriptacion = new NpsTypeEncrypt();
			return EncriptarCampos(parametros, tipoEncriptacion);
		}

		private string EncriptarCampos(string parametros, NpsTypeEncrypt tipoEncriptacion)
		{
			return tipoEncriptacion.GetEncriptacion(parametros, SECRETKEY);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DACServices.Repositories.Vendor.NpsVendor.Security
{
	public sealed class NpsHash
	{
		/// <summary>
		/// String utilizado para encriptar mediante MD5 RFC 1321
		/// </summary>
		private const string CARACTER = "x2";

		/// <summary>
		/// Encriptacion que utiliza NPS, la cual implementa HMAC SHA256.
		/// </summary>
		/// <param name="parametros">campos concatenados del request.</param>
		/// <param name="secretKey">secret key provista por NPS.</param>
		/// <returns></returns>
		public static string GenerarHMAC_SHA256(string parametros, string secretKey)
		{
			return GetHmacSha256Hash(parametros, secretKey);
		}

		private static string GetHmacSha256Hash(string message, string key)
		{
			byte[] keyByte = new ASCIIEncoding().GetBytes(key);
			byte[] messageBytes = new ASCIIEncoding().GetBytes(message);

			byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);

			return String.Concat(Array.ConvertAll(hashmessage, x => x.ToString(CARACTER)));
		}
	}
}

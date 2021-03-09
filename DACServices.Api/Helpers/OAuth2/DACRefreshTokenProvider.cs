using DACServices.Business.Service;
using DACServices.Entities;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DACServices.Api.Helpers.OAuth2
{
	public class DACRefreshTokenProvider : IAuthenticationTokenProvider
	{
		private ServiceUsuarioBusiness usuarioBusiness = new ServiceUsuarioBusiness();

		public void Create(AuthenticationTokenCreateContext context)
		{
			throw new NotImplementedException();
		}

		public async Task CreateAsync(AuthenticationTokenCreateContext context)
		{
			//throw new NotImplementedException();

			//var clientId = context.Ticket.Properties.Dictionary["client_id"];
			var clientId = context.Ticket.Properties.Dictionary["userName"];
						
			if (string.IsNullOrEmpty(clientId)) return;

			context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
			context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(7200));

			string refresh_token = context.SerializeTicket();
			context.SetToken(refresh_token);

			//Traigo el usuario por el username y actualizo user con el refresh_token (mock user no lo utiliza)
			//Func<tbUsuario, bool> func = x => x.usu_usuario == clientId;
			//var listaUsuario = usuarioBusiness.Read(func) as List<tbUsuario>;
			//var usuario = listaUsuario.FirstOrDefault();
			//usuario.usu_refresh_token = refresh_token;
			//usuarioBusiness.Update(usuario);
			//Traigo el usuario por el username y actualizo user con el refresh_token (mock user no lo utiliza)
		}

		public void Receive(AuthenticationTokenReceiveContext context)
		{
			throw new NotImplementedException();
		}

		public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
		{
			//throw new NotImplementedException();
			string token = context.Token;
			context.DeserializeTicket(token);
		}
	}
}
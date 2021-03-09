using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using DACServices.Repositories;
using DACServices.Business.Service;
using DACServices.Entities;

namespace DACServices.Api.Helpers.OAuth2
{
    public class DACOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        /// <summary>
        /// Propiedad para las entities de la base de datos
        /// </summary>
        private DB_DACSEntities databaseManager = new DB_DACSEntities();
        private ServiceUsuarioBusiness usuarioBusiness = new ServiceUsuarioBusiness();

        public DACOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
                throw new ArgumentNullException(nameof(publicClientId));
            _publicClientId = publicClientId;
        }

        /// <summary>
        /// Grant resource owner credentials overload method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Initialization
            string usernameVal = context.UserName;
            string passwordVal = context.Password;

			//Valido usuario y password - Funciona OK, se comenta para evitar gastos
			//var user = this.databaseManager.LoginByUsernamePassword(usernameVal, passwordVal).ToList();

			//HC user para que no me rompan en bolsillo con la bd
			var user = new List<Entities.tbUsuario>();
			if (!string.IsNullOrEmpty(usernameVal) && !string.IsNullOrEmpty(passwordVal))
			{
				if (usernameVal == "refresUser" && passwordVal == "mock")
					user.Add(new Entities.tbUsuario() { usu_id = 1, usu_usuario = "refresUser", usu_password = "mock" });
			}
			//HC user

			//Valído
			if (user == null || user.Count() <= 0)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                return;
            }

            //Inicialización
            var claims = new List<Claim>();
            var userInfo = user.FirstOrDefault();

            //Setting
            claims.Add(new Claim(ClaimTypes.Name, userInfo.usu_usuario));

            //Setting claim identities for OAUTH protocol
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
			oAuthClaimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

			ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            //Setting user authentication
            AuthenticationProperties authenticationProperties = CreateProperties(userInfo.usu_usuario);
            AuthenticationTicket authenticationTicket = new AuthenticationTicket(oAuthClaimIdentity, authenticationProperties);

            //Grant access to authorize user
            context.Validated(authenticationTicket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
        }

        /// <summary>
        /// Token endpoint override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Return when task is completed</returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            //Return info
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Validate client authentication override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns validation of client authentication</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //Para obtener el client id desde el request wwwwurlencode
            //string client_id = string.Empty;
            //string client_secret = string.Empty;
            //context.TryGetFormCredentials(out client_id, out client_secret);
            //string token = context.Parameters.Get("refresh_token");
            
            //Resource owner password credentials does not provider a client ID
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Validate client redirect URI override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns validation of client redirect URI</returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            //Verification
            if (context.ClientId == _publicClientId)
            {
                //Initialization
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                //Verification
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                    context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Create Authentication method properties
        /// </summary>
        /// <param name="userName">Username parameter</param>
        /// <returns>Returns Authenticated properties</returns>
        public static AuthenticationProperties CreateProperties(string userName)
        {
            //Settings
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName }
            };

            //return info
            return new AuthenticationProperties(data);
        }

		public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
		{
			//return base.GrantRefreshToken(context);
			var originalClient = context.Ticket.Properties.Dictionary["userName"];
            //var currentCliente = context.ClientId;

            //Valido que el user este activo, con usuario mock no se va a usar
            //Func<tbUsuario, bool> func = x => x.usu_usuario == originalClient;
            //var listaUsuario = usuarioBusiness.Read(func) as List<tbUsuario>;
            //var usuario = listaUsuario.FirstOrDefault();
            //if (!usuario.usu_active)
            //{
            //	context.SetError("invalid_refresh_token", "Refresh token is issued for inactive user.");
            //	return Task.FromResult<object>(null);
            //}
            //Valido que el user este activo, con usuario mock no se va a usar

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
		}
	}
}
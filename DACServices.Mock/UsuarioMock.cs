using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Mock
{
    public class UsuarioMock
    {
		private static UsuarioMock _instancia;
        private List<tbUsuario> _listaUsuarios = null;
        private tbUsuario _usuario = null;

		protected UsuarioMock()
		{
			//Constuctor tiene que estar protegido
			_listaUsuarios = new List<tbUsuario>();

			_usuario = new tbUsuario() { usu_id = 1, usu_usuario = "Jonatan Maidana", usu_password = "jjj" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 2, usu_usuario = "Ignacio Fernandez", usu_password = "iii" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 3, usu_usuario = "Franco Armani", usu_password = "fff" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 4, usu_usuario = "Lucas Pratto", usu_password = "ppp" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 5, usu_usuario = "Javier Pinola", usu_password = "jjj" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 6, usu_usuario = "Leonardo Ponzio", usu_password = "lll" };
			_listaUsuarios.Add(_usuario);

			_usuario = new tbUsuario() { usu_id = 7, usu_usuario = "Milton Casco", usu_password = "mmm" };
			_listaUsuarios.Add(_usuario);
		}

		public static UsuarioMock Instancia()
		{
			if (_instancia == null)
				_instancia = new UsuarioMock();
			return _instancia;
		} 

        public List<tbUsuario> GetListaUsuarios()
        {
            return _listaUsuarios.OrderBy(x => x.usu_id).ToList();
        }

		public tbUsuario GetUsuario(int id)
		{
			return _listaUsuarios.Where(x => x.usu_id == id).FirstOrDefault();
		}

		public tbUsuario CreateUsuario(tbUsuario usuario)
		{
			usuario.usu_id = _listaUsuarios.OrderByDescending(x => x.usu_id).FirstOrDefault().usu_id + 1;
			_listaUsuarios.Add(usuario);
			return usuario;
		}

		public tbUsuario Delete(tbUsuario usuario)
		{
			var result = _listaUsuarios.Where(x => x.usu_id == usuario.usu_id).FirstOrDefault();
			if (result != null)
				_listaUsuarios.Remove(result);
			return result;
		}

		public tbUsuario Update(tbUsuario usuario)
		{
			tbUsuario result = null;
			var tempUsuario = _listaUsuarios.Where(x => x.usu_id == usuario.usu_id).FirstOrDefault();
			if (tempUsuario != null)
			{
				_listaUsuarios.Remove(tempUsuario);
				_listaUsuarios.Add(usuario);
				result = usuario;
			}
			return result;
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACServices.Api.Models
{
	public class UsuarioMenuModel
	{
		public List<MenuFather> GetMenu()
		{
			List<MenuFather> menu = new List<MenuFather>();

			//creo padre
			MenuFather menuFather = new MenuFather();
			menuFather.icon = "mdi-chevron-up";
			menuFather.icon_alt = "mdi-chevron-down";
			menuFather.text = "Home";
			//creo lista hijos
			menuFather.children = new List<MenuChild>();
			//creo hijo
			MenuChild menuChild = new MenuChild();
			menuChild.text = "Inicio";
			menuChild.path = "/";
			//agrego hijo a padre
			menuFather.children.Add(menuChild);
				//agrego padre a menu
			menu.Add(menuFather);

			//creo padre
			menuFather = new MenuFather();
			menuFather.icon = "mdi-chevron-up";
			menuFather.icon_alt = "mdi-chevron-down";
			menuFather.text = "Seguridad";
			//creo lista hijos
			menuFather.children = new List<MenuChild>();
			//creo hijo
			menuChild = new MenuChild();
			menuChild.text = "Usuarios";
			menuChild.path = "/usuarios";
			//agrego hijo a padre
			menuFather.children.Add(menuChild);
			//creo hijo
			menuChild = new MenuChild();
			menuChild.text = "Roles";
			menuChild.path = "/roles";
			//agrego hijo a padre
			menuFather.children.Add(menuChild);
			//creo hijo
			menuChild = new MenuChild();
			menuChild.text = "Perfiles";
			menuChild.path = "/perfiles";
			//agrego hijo a padre
			menuFather.children.Add(menuChild);

			//agrego padre a menu
			menu.Add(menuFather);

			return menu;
		}
	}

	public class MenuFather
	{
		public string icon { get; set; }
		public string icon_alt { get; set; }
		public string text { get; set; }
		public bool model { get; set; }
		public List<MenuChild> children { get; set; }
 }
	public class MenuChild
	{
		public string text { get; set; }
		public string path { get; set; }
	}
}
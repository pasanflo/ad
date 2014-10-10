using MySql.Data.MySqlClient;
using System;
using Gtk;

namespace PCategoria
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			App.Instance.MySqlConnection = new MySqlConnection(
				"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas"
			);

			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}

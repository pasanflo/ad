using System;
using MySql.Data.MySqlClient;

namespace PCategoria
{
	public partial class CategoriaView : Gtk.Window
	{
		private object id;
		public CategoriaView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		public CategoriaView(object id) : this(){
			this.id = id;
			MySqlCommand mySqlCommand =
				App.Instance.MySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = String.Format (
				"SELECT * FROM categoria WHERE id={0}", id
				);
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			mySqlDataReader.Read ();
			entryNombre.Text = mySqlDataReader ["nombre"].ToString ();
		}

		protected void OnSaveActionActivated (object sender, EventArgs e)
		{
			MySqlCommand mySqlCommand =
				App.Instance.MySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = String.Format (
				"UPDATE categoria SET nombre=@nombre WHERE id={0}", id
				);
			MySqlParameter mySqlParameter = mySqlCommand.CreateParameter ();
			mySqlParameter.ParameterName = "nombre";
			mySqlParameter.Value = entryNombre.Text;
			mySqlCommand.Parameters.Add (mySqlParameter);

			mySqlCommand.ExecuteNonQuery ();

			Destroy ();
		}
	}
}


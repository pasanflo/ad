using System;
using System.Data;
using Gtk;
using MySql.Data;
using MySql.Data.MySqlClient;


public partial class MainWindow: Gtk.Window
{	
	//Conexion

	private MySqlConnection mySqlConnection;
	private ListStore listStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		//El listStore es el modelo del TreeView. Para meter datos en un treeView, metemos datos
		//en el listStore y el listStore en el treeView.
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		listStore = new ListStore (typeof(string), typeof(string));

		treeView.Model = listStore;

		mySqlConnection = new MySqlConnection (
			"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "SELECT * FROM categoria";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues (id, nombre);
		}



	}

	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		listStore.AppendValues ("1", DateTime.Now.ToString());
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear();

		listStore = new ListStore (typeof(string), typeof(string));

		treeView.Model = listStore;

		mySqlConnection = new MySqlConnection (
			"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "SELECT * FROM categoria";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues (id, nombre);
		}


	}
}

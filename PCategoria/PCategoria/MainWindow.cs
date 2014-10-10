using Gtk;
using MySql.Data.MySqlClient;
using System;

using PCategoria;


public partial class MainWindow: Gtk.Window
{	
	//Conexion

	private MySqlConnection mySqlConnection;
	private ListStore listStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		deleteAction.Sensitive = false;

		mySqlConnection = new MySqlConnection ("Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");
		mySqlConnection.Open ();
	
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		listStore = new ListStore (typeof(int), typeof(string));

		treeView.Model = listStore;

		fillListStore ();

		treeView.Selection.Changed += delegate {
			deleteAction.Sensitive = treeView.Selection.CountSelectedRows () > 0;
			editAction.Sensitive = treeView.Selection.CountSelectedRows () > 0;
		};
	}

		
	private void fillListStore ()
	{
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "SELECT id, nombre FROM categoria";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues (id, nombre);
		}
		mySqlDataReader.Close();
	}

	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = String.Format("INSERT INTO categoria (nombre) VALUES ('{0}')", DateTime.Now);
		mySqlCommand.ExecuteNonQuery();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		mySqlConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear();
		fillListStore ();
	}
	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		//TODO Implementar Delete.
		//TODO Investigar Delete con "Nueva ventana".
	}
	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		object nombre = listStore.GetValue (treeIter, 0);
		CategoriaView categoriaView = new CategoriaView (nombre);
	}
}

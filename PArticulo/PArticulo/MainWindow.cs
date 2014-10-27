using Gtk;
using System;
using System.Data;

using SerpisAd;
using PArticulo;


public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		llamarArticulo ();
		llamarCategoria ();



		treeViewCategoria.Selection.Changed += selectionChanged;
	}

	void llamarCategoria ()
	{
		dbConnection = App.Instance.DbConnection;
		treeViewCategoria.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeViewCategoria.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		listStore = new ListStore (typeof(ulong), typeof(string));
		treeViewCategoria.Model = listStore;
		fillListStoreCategoria ();
	}

	void llamarArticulo()
	{
		dbConnection = App.Instance.DbConnection;
		treeViewArticulo.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeViewArticulo.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		treeViewArticulo.AppendColumn ("categoria", new CellRendererText (), "text", 2);
		treeViewArticulo.AppendColumn ("precio", new CellRendererText (), "text", 3);
		listStore = new ListStore (typeof(ulong), typeof(string), typeof(ulong), typeof(string));
		treeViewArticulo.Model = listStore;
		fillListStoreArticulo ();
	}

	private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeViewCategoria.Selection.CountSelectedRows () > 0;
		deleteAction.Sensitive = hasSelected;
		editAction.Sensitive = hasSelected;
	}

	private void fillListStoreCategoria() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			listStore.AppendValues (id, nombre);
		}
		dataReader.Close ();
	}

	private void fillListStoreArticulo() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object categoria = dataReader ["categoria"];
			object precio = dataReader ["precio"].ToString();
			listStore.AppendValues (id, nombre, categoria, precio);
		}
		dataReader.Close ();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnRemoveActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
}

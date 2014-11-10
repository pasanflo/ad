using Gtk;
using System;
using System.Data;

using SerpisAd;
using PArticulo;


public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStoreArticulo;
	private ListStore listStoreCategoria;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		llamarArticulo ();
		llamarCategoria ();



		treeViewCategoria.Selection.Changed += selectionChanged;
		treeViewArticulo.Selection.Changed += selectionChanged;
	}

	void llamarCategoria ()
	{
		dbConnection = App.Instance.DbConnection;
		treeViewCategoria.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeViewCategoria.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		listStoreCategoria = new ListStore (typeof(ulong), typeof(string));
		treeViewCategoria.Model = listStoreCategoria;
		fillListStoreCategoria ();
	}

	void llamarArticulo()
	{
		dbConnection = App.Instance.DbConnection;
		treeViewArticulo.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeViewArticulo.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		treeViewArticulo.AppendColumn ("categoria", new CellRendererText (), "text", 2);
		treeViewArticulo.AppendColumn ("precio", new CellRendererText (), "text", 3);
		listStoreArticulo = new ListStore (typeof(ulong), typeof(string), typeof(ulong), typeof(string));
		treeViewArticulo.Model = listStoreArticulo;
		fillListStoreArticulo ();
	}

	private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		Console.WriteLine("notebook.TabPos= "+notebook.TabPos);
		bool hasSelectedCategoria = treeViewCategoria.Selection.CountSelectedRows () > 0;
		Console.WriteLine ("hasSelectedCategoria= "+hasSelectedCategoria);
		bool hasSelectedArticulo = treeViewArticulo.Selection.CountSelectedRows () > 0;
		Console.WriteLine ("hasSelectedArticulo= "+hasSelectedArticulo);

		if (notebook.CurrentPage == 0 & hasSelectedArticulo == false) { //Si está elegida la pestaña Artículo (0)
			deleteAction.Sensitive = false;
			editAction.Sensitive = false;
		}
		else { deleteAction.Sensitive = true; editAction.Sensitive = true; }
		if (notebook.CurrentPage == 1 & hasSelectedCategoria == false) { //Si está elegida la pestaña Categoria (1)
			deleteAction.Sensitive = false;
			editAction.Sensitive = false;
		}
		else { deleteAction.Sensitive = true; editAction.Sensitive = true; }
	}

	private void fillListStoreCategoria() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			listStoreCategoria.AppendValues (id, nombre);
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
			listStoreArticulo.AppendValues (id, nombre, categoria, precio);
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
		listStoreArticulo.Clear ();
		fillListStoreArticulo ();
		listStoreCategoria.Clear ();
		fillListStoreCategoria ();
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
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Seguro que quieres borrar?"
			);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();

		if (response != ResponseType.Yes)
			return;

		TreeIter treeIter;
		string deleteSql = "";
		if (notebook.Page == 0) { //Si está elegida la pestaña Artículo (0)
			treeViewArticulo.Selection.GetSelected (out treeIter);
			object id = listStoreArticulo.GetValue (treeIter, 0);
			deleteSql = string.Format ("DELETE FROM articulo WHERE id={0}", id);
			Console.WriteLine (deleteSql);
		} 
		if (notebook.Page == 1) { //Si está elegida la pestaña Categoria (1)
			treeViewCategoria.Selection.GetSelected (out treeIter);
			object id = listStoreCategoria.GetValue (treeIter, 0);
			deleteSql = string.Format ("DELETE FROM categoria WHERE id={0}", id);
		}
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = deleteSql;

		dbCommand.ExecuteNonQuery ();
	}
}

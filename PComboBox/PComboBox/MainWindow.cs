using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		ListStore<Categoria> categorias = new ListStore<Categoria> ();
		categorias.Add (new Categoria (1, "Uno"));
		categorias.Add (new Categoria (2, "Dos"));
		categorias.Add (new Categoria (3, "Tres"));
		categorias.Add (new Categoria (4, "Cuatro"));

		CellRendererText cellRendererText = new CellRendererText ();
		comboBox.PackStart (cellRendererText, false);
		comboBox.AddAttribute (cellRendererText, "text", 1);

		ListStore listStore = new ListStore (typeof(int), typeof(string));
		TreeIter initialTreeIter = listStore.AppendValues (0, "<sin asignar>");


	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnPropertiesActionActivated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		bool activeIter = comboBox.GetActiveIter (out treeIter);
		object id = activeIter ? ListStore.GetValue (treeIter, 0) : 0;
		Console.WriteLine ("id={0}", id);
	}
}
public class Categoria 
{
	public Categoria(int id, string nombre){
		Id = id;
		Nombre = nombre;
	}
	public int Id {get; set;}
	public string Nombre {get; set;}
}

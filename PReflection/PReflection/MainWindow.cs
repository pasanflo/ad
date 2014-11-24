using Gtk;
using System;
using System.Reflection;

using PReflection;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Type type = typeof(Categoria);
		PropertyInfo[] properties = type.GetProperties ();

		foreach(PropertyInfo property in properties){
			Console.WriteLine (property.Name + "= " + property.PropertyType);
		}

		Categoria categoria = new Categoria (14, "numero catorce");
		showValues (categoria);

	}

	private void showValues(object obj){
		Type type = obj.GetType ();
		FieldInfo[] fields = type.GetFields (BindingFlags.Instance | BindingFlags.NonPublic);
		foreach (FieldInfo field in fields) {
			Console.WriteLine ("field.Name.GetValue = " + property.GetValue());
		}
	
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

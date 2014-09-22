using System;
using System.Data;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PHolaMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{


			MySqlConnection mySqlConnection = new MySqlConnection (
				"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");

			mySqlConnection.Open ();

			//Factory
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = 
				string.Format ("SELECT * FROM categoria");

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

			Console.WriteLine (mySqlDataReader.FieldCount);

			for (int index=0; index < mySqlDataReader.FieldCount; index++) {
				Console.WriteLine ("columna {0}={1}", index, mySqlDataReader.GetName (index));
			}
			while (mySqlDataReader.Read()) {
				object id = mySqlDataReader ["id"];
				object nombre = mySqlDataReader ["nombre"];
				Console.WriteLine ("id={0} nombre={1}", id, nombre);
			}

			int opcion = Console.ReadLine ();



			switch (opcion){ 
			case "0":
				Process process = new Process
			case "1":
				1 ();
			case "2":
				2 ();
			case "3":
				3 ();
			case "4":
				4 ();
			}
			
//			while (mySqlDataReader.Read()) {
//				for (int index=0; index < mySqlDataReader.FieldCount; index++) {
//					Console.WriteLine (mySqlDataReader.GetValue (index));
//				}
//			}
			mySqlDataReader.Close ();

			mySqlConnection.Close ();
		}
	}
}

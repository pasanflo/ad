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
			MySqlDataReader mySqlDataReader;

			MySqlConnection mySqlConnection = new MySqlConnection (
				"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");

			mySqlConnection.Open ();

			//Factory


//			Console.WriteLine (mySqlDataReader.FieldCount);

//			for (int index=0; index < mySqlDataReader.FieldCount; index++) {
//				Console.WriteLine ("columna {0}={1}", index, mySqlDataReader.GetName (index));
//			}
//			while (mySqlDataReader.Read()) {
//				object id = mySqlDataReader ["id"];
//				object nombre = mySqlDataReader ["nombre"];
//				Console.WriteLine ("id={0} nombre={1}", id, nombre);
//			}

			//mySqlDataReader.Close ();

			while(true){

				Console.WriteLine ("------------------------");
				Console.WriteLine ("0. Salir");
				Console.WriteLine ("1. Nuevo");
				Console.WriteLine ("2. Modificar");
				Console.WriteLine ("3. Eliminar");
				Console.WriteLine ("4. Ver todos");
				Console.WriteLine ("------------------------");

				string opcion = Console.ReadLine ().ToString();



				switch (opcion) {
				case "0":
					Environment.Exit (0);
					break;
				case "1":
					Console.Write ("Inserta un valor nuevo: ");
					string nuevo = Console.ReadLine ();

					MySqlCommand mySqlCommandInsert = mySqlConnection.CreateCommand ();
					mySqlCommandInsert.CommandText = 
						string.Format ("INSERT INTO categoria (nombre) VALUES ('{0}')", nuevo);

					mySqlCommandInsert.ExecuteNonQuery();
		
					break;
				case "2":
					Console.Write ("Inserta un valor de ID: ");
					string targetId = Console.ReadLine ();
					Console.Write ("Inserta un nombre nuevo: ");
					string newName = Console.ReadLine ();

					MySqlCommand mySqlCommandModify = mySqlConnection.CreateCommand ();
					mySqlCommandModify.CommandText = 
						string.Format ("UPDATE categoria SET nombre='{0}' WHERE id='{1}'", newName, targetId);

					mySqlCommandModify.ExecuteNonQuery ();

					break;
				case "3":
					Console.Write ("Inserta un valor de ID: ");
					string deleteId = Console.ReadLine ();

					MySqlCommand mySqlCommandDelete = mySqlConnection.CreateCommand ();
					mySqlCommandDelete.CommandText = 
						string.Format ("DELETE FROM categoria WHERE id='{0}'", deleteId);

					mySqlCommandDelete.ExecuteNonQuery ();

					break;
				case "4": 
					MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
					mySqlCommand.CommandText =
						string.Format ("SELECT * FROM categoria");
					mySqlDataReader = mySqlCommand.ExecuteReader();

					while (mySqlDataReader.Read()) {
						object id = mySqlDataReader ["id"];
						object nombre = mySqlDataReader ["nombre"];
						Console.WriteLine ("id={0} nombre={1}", id, nombre);
					}
					mySqlDataReader.Close ();

					break;
			}
			
		}
			
//			while (mySqlDataReader.Read()) {
//				for (int index=0; index < mySqlDataReader.FieldCount; index++) {
//					Console.WriteLine (mySqlDataReader.GetValue (index));
//				}
//			}

		}
	}
}

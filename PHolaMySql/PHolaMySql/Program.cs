using System;
using System.Data;
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



			mySqlConnection.Close ();
		}
	}
}

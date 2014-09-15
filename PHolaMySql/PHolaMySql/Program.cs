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

			MySqlDataReader mysqlDataReader = mySqlCommand.ExecuteReader;
			while (mysqlDataReader.Read ()) {
				Console.WriteLine (mysqlDataReader.FieldCount);

			}

			mySqlConnection.Close ();
		}
	}
}

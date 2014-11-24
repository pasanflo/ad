using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PDataSet
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mySqlConnection = new MySqlConnection (
				"DataSource=localhost;Database=dbprueba;User ID=root;Password=sistemas"
			);
			mySqlConnection.Open();
			string selectSql = "SELECT * FROM articulo";
			IDbDataAdapter dbDataAdapter = new MySqlDataAdapter (selectSql, mySqlConnection);

			DataSet dataSet = new DataSet ();

		}
	}
}

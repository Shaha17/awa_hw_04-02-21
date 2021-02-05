using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DZ_04_02_21
{
	class Program
	{
		static string conString = "Data source = 10.211.55.3;Initial catalog=AlifAcademy;user id=sa;password=1234";
		string userChoose = "";
		static void Main(string[] args)
		{
			bool isOn = true;
			var conn = new SqlConnection(conString);
			while (isOn)
			{
				Console.WriteLine("1. Добавить");
				Console.WriteLine("2. Удалить");
				Console.WriteLine("3. Выбрать всё");
				Console.WriteLine("4. Выбрать");
				Console.WriteLine("5. Обновить");
				Console.WriteLine("Нажмите enter для выхода");
				string choose = Console.ReadLine();
				switch (choose)
				{
					case "1":
						{
							InsertToDB(conn);
							break;
						}
					case "2":
						{
							DeleteByIdFromDB(conn);
							break;
						}
					case "3":
						{
							SelectAllFromDB(conn);
							break;
						}
					case "4":
						{
							SelectByIdFromDB(conn);
							break;
						}
					case "5":
						{
							UpdateByIdInDB(conn);
							break;
						}
					default:
						isOn = false;
						break;
				}
			}


		}
		List<Person> GetDataFromDB(SqlConnection conn)
		{
			var lst = new List<Person>();

			return lst;
		}
		static void WriteLineSuccessText(string text)
		{
			WriteLineWithColor(text, ConsoleColor.Green);
		}
		static void WriteLineFailText(string text)
		{
			WriteLineWithColor(text, ConsoleColor.Red);
		}
		static void WriteLineWithColor(string text, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.White;
		}
		static void InsertToDB(SqlConnection conn)
		{
			var firstName = ConsoleReadLineWithText("Enter FirstName: ");
			var lastName = ConsoleReadLineWithText("Enter LastName: ");
			var middleName = ConsoleReadLineWithText("Enter MiddleName: ");
			var birthDate = ConsoleReadLineWithText("Enter BirthDate: ");

			SqlCommand command;
			try
			{
				conn.Open();
				command = conn.CreateCommand();
				command.CommandText = $"INSERT INTO Person(FirstName,LastName,MiddleName,BirthDate) VALUES('{firstName}','{lastName}','{middleName}','{birthDate}')";
				var result = command.ExecuteNonQuery();
				if (result > 0)
				{
					WriteLineSuccessText("Person inserted");
				}
			}
			catch (Exception e)
			{
				WriteLineFailText(e.Message);
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
				Console.WriteLine();

			}
		}
		static void SelectAllFromDB(SqlConnection conn)
		{
			SqlCommand command;
			List<Person> list = new List<Person>();
			try
			{
				conn.Open();
				command = conn.CreateCommand();
				command.CommandText = "SELECT * FROM Person";

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					var person = new Person
					{
						Id = Convert.ToInt32(reader["Id"]),
						FirstName = Convert.ToString(reader["FirstName"]),
						LastName = Convert.ToString(reader["LastName"]),
						MiddleName = Convert.ToString(reader["MiddleName"]),
						BirthDate = Convert.ToString(reader["BirthDate"])
					};
					list.Add(person);
				}
				Console.ForegroundColor = ConsoleColor.Green;
				foreach (var per in list)
				{
					per.Show();
				}
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;
			}
			catch (Exception e)
			{
				WriteLineFailText(e.Message);
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
				Console.WriteLine();

			}

		}
		static void SelectByIdFromDB(SqlConnection conn)
		{
			SqlCommand command;
			string id = ConsoleReadLineWithText("Id: ");
			try
			{
				conn.Open();
				command = conn.CreateCommand();
				command.CommandText = $"SELECT * FROM Person WHERE Id = {id}";
				SqlDataReader reader = command.ExecuteReader();
				Person person;
				if (reader.Read())
				{
					person = new Person
					{
						Id = Convert.ToInt32(reader["Id"]),
						FirstName = Convert.ToString(reader["FirstName"]),
						LastName = Convert.ToString(reader["LastName"]),
						MiddleName = Convert.ToString(reader["MiddleName"]),
						BirthDate = Convert.ToString(reader["BirthDate"])
					};
					Console.ForegroundColor = ConsoleColor.Green;
					person.Show();
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					WriteLineFailText("Bad Id");
				}
			}
			catch (Exception e)
			{
				WriteLineFailText(e.Message);
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
				Console.WriteLine();

			}
		}
		static void UpdateByIdInDB(SqlConnection conn)
		{
			string id = ConsoleReadLineWithText("Id: ");
			var firstName = ConsoleReadLineWithText("Enter new FirstName: ");
			var lastName = ConsoleReadLineWithText("Enter new LastName: ");
			var middleName = ConsoleReadLineWithText("Enter new MiddleName: ");
			var BirthDate = ConsoleReadLineWithText("Enter new BirthDate: ");

			SqlCommand command;
			try
			{
				conn.Open();
				command = conn.CreateCommand();
				command.CommandText = $"UPDATE Person SET "
				+ $" FirstName = '{firstName}' , LastName = '{lastName}', MiddleName = '{middleName}', BirthDate = '{BirthDate}' "
				+ $" WHERE Id = {id}";
				var result = command.ExecuteNonQuery();
				if (result > 0)
				{
					WriteLineSuccessText("Person updated");
				}
				else
				{
					WriteLineFailText("Bad Id");
				}
			}
			catch (Exception e)
			{
				WriteLineFailText(e.Message);
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
				Console.WriteLine();

			}
		}
		static void DeleteByIdFromDB(SqlConnection conn)
		{
			string id = ConsoleReadLineWithText("Id: ");
			SqlCommand command;
			try
			{
				conn.Open();
				command = conn.CreateCommand();
				command.CommandText = $"DELETE Person WHERE Id = {id}";
				var result = command.ExecuteNonQuery();
				if (result > 0)
				{
					WriteLineSuccessText("Person deleted");
				}
				else
				{
					WriteLineFailText("Bad Id");
				}
			}
			catch (Exception e)
			{
				WriteLineFailText(e.Message);
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
				Console.WriteLine();

			}
		}
		static string ConsoleReadLineWithText(string text)
		{
			Console.WriteLine(text);
			return Console.ReadLine();
		}

	}
}

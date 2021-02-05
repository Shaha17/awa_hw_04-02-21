using System;

namespace DZ_04_02_21
{
	class Person
	{
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string BirthDate { get; set; }
		public void Show()
		{
			Console.WriteLine($"Id: {this.Id}, \tLastName: {this.LastName}, \tFirstName: {this.FirstName}, \tMiddleName: {this.MiddleName}, \t\tBirthDate: {this.BirthDate}");
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugEnt.CommonFunctions.ConsoleApp
{
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; private set; }
		public int Age { get; init; }

		public Person (string firstName, string lastName, int age) {
			FirstName = firstName;
			LastName = lastName;
			Age = age;	
		}


		public override string ToString () { return String.Format("{0}, {1}  [{2}]", LastName, FirstName, Age); }


	}
}

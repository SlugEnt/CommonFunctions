using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Slugent.CommonFunctions
{
	public static class ConsoleExtensions
	{

		/// <summary>
		/// Prompts for the user to hit a key to continue
		/// </summary>
		/// <param name="prompt"></param>
		public static void PressAnyKey(string prompt = "")
		{
			if (!String.IsNullOrEmpty(prompt))
				Console.WriteLine(prompt );
			else { Console.WriteLine("Press any key to continue..."); }


			// Flush Keyboard buffer
			while (Console.KeyAvailable) { Console.ReadKey(); }
			Console.ReadKey(true);
		}


		/// <summary>
		/// Prompts for the user to hit a key to continue.  Displays prompt in specified color.
		/// </summary>
		/// <param name="prompt"></param>
		public static void PressAnyKeyInColor(Color promptColor, string prompt = "" )
		{
			if (!String.IsNullOrEmpty(prompt))
				Console.WriteLine(prompt,promptColor);
			else { Console.WriteLine("Press any key to continue...",promptColor); }


			// Flush Keyboard buffer
			while (Console.KeyAvailable) { Console.ReadKey(); }
			Console.ReadKey(true);
		}

	}
}

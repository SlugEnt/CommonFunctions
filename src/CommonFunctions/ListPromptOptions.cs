using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slugent.CommonFunctions
{
	/// <summary>
	/// Settings used in various Display functions for Lists and prompts
	/// </summary>
	public class ListPromptOptions<T> {
		#region "Color Settings"

		/// <summary>
		/// If true, prompts will utilize Console.Color to be displayed.
		/// </summary>
		public bool UseColorPrompts { get; set; }= true;


		/// <summary>
		/// Color used to display that there are no items in the list.
		/// </summary>
		public Color ColorNoItemsInList { get; set; } = Color.Yellow;


		/// <summary>
		/// Color used to display the list prompt for selection
		/// </summary>
		public Color ColorListSelectionPrompt { get; set; } = Color.DarkOrange;
		#endregion


		#region "List Display"       
		/// <summary>
		/// Method to be used to display each list item.  Method should return a formatted string.
		/// </summary>
		public Func<T,string> ListItemDisplay_AsString { get; set; } 
		
		/// <summary>
		/// Method to be used to display each list item.  Method must write the item information to the console and return true;
		/// <para>If this and ListItemDisplay_AsString are set, this overrides</para>
		/// </summary>
		public Func<T,bool> ListItemDisplay_Custom { get; set; }


		/// <summary>
		/// This is what each item in the list is, such as car, person, name, etc.  Should be in singular case.
		/// </summary>
		public string ItemSingularText { get; set; } = "item";

		#endregion

		#region "List Options"
		/// <summary>
		/// If true, then the user must select an item.  If False, then the option X is available to exit without selecting.  When true, this will prevent the NewItemAllowed and Exit Options from working
		/// </summary>
		public bool SelectOptionMustChooseItem { get; set; }


		/// <summary>
		/// Prompt to be used at bottom of list to request user to select item.  If not set then standard prompt used
		/// </summary>
		public string CustomPrompt { get; set; } = "";


		/// <summary>
		/// If true, as soon as the user has entered enough digits to uniquely identify their selection, the result is returned without user needing to hit enter. Otherwise enter must be pressed.
		/// </summary>
		public bool AutoItemSelection { get; set; } = true;


		/// <summary>
		/// Determines if the selection allows the new item method.  If so, then the list allows the user to select N for new.  A special negative return value is returned to indicate new.
		/// </summary>
		public bool SelectOptionNewItemAllowed { get; set; } = false;
#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public ListPromptOptions () {}
	}
}

# CommonFunctions
Provides some useful functions

## Console List Display and Selection
The ListFX set of methods provide a means to automatically display a list or dictionary of items to the user and allow them to easily pick one.

Provides the following enhanced capabilities
* Smart selection, user is not required to hit enter to select.  If the list contains <10 items, then a single button press of a digit will select
* Provides options for Adding a new item
* Provides X or Q options for exiting without secting an item
* Provides for the ability to call a custom method used to display the contents of the list item.  For instance if you are displaying a Person object, then your custom method could display the person as Name (Age) for instance.
* Provides an advanced custom display option that enables the use of color within the list item display.



## ListDisplayOptions
List display is controlled through a ListDisplayOptions object.  Setting the various parameters determines how the list displays and operates.
`

	// Define List Prompt options
	ListPromptOptions<Person> listPromptOptions = new ListPromptOptions<Person>()
	{

		ItemSingularText = "person",
		// This is the method that is called for each list item to display it.
		ListItemDisplay_AsString = DisplayPersonWithAgeFirst
	};
`

The following methods are supported:
* ListItemDisplay_AsString   - Custom method provided by caller that returns a string of the formatted list item
* ListItemDisplay_Custom     - Custom method provided by caller that builds an enhanced console line
* ItemSingularText           - Singular name of an item in the list.  For instance a list of cars would be car, People would be person
* SelectOptionMustChooseItem - If set to true, the user must select an item.  If false, they can choose to exit without making a choice
* CustomPrompt               - If set this will be the prompt at bottom of list, otherwise default prompts are used.
* AutoItemSelection          - If true, the user does not need to hit enter to confirm choice.
* SelectOptionNewItemAllowed - If true, the choice to add a new item to the list is presented.  





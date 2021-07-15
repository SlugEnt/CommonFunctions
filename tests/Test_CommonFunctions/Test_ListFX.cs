using NUnit.Framework;
using SlugEnt.CommonFunctions;

namespace SlugEnt.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		
		public void ListSelection_Success(int listCount, string itemInput) {
			ListFX.ChooseListItem(listCount);
			Assert.Pass();
		}
	}
}
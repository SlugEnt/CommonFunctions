using CommonFunctions;
using NUnit.Framework;

namespace Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ListSelection_Success(int listCount, string itemInput) {
			ListFX.ChooseListItem(listCount);
			Assert.Pass();
		}
	}
}
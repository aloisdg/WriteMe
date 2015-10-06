using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WriteMe.Test
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void TestReadMe()
		{
			Program.Main(new[] { @"..\..\readme.json" });

			var expected = File.ReadAllText(@"..\..\expected.md");
			var actual = File.ReadAllText(@"..\..\README.md");
			Assert.AreEqual(expected, actual);
		}
	}
}

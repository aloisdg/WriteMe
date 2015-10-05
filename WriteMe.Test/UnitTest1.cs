using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WriteMe.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Program.Main(new[] { @"..\..\readme.json" });

			var expected = File.ReadAllText(@"..\..\expected.md");
			var actual = File.ReadAllText(@"..\..\README.md");
			Assert.AreEqual(expected, actual);
		}
	}
}

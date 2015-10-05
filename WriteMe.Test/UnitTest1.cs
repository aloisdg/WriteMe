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
			LaunchCommandLineApp();
		}

		private void LaunchCommandLineApp()
		{
			Debug.WriteLine(Environment.CurrentDirectory);

			var exe = @"..\..\..\WriteMe\bin\Debug\WriteMe.exe";
			var json = @"..\..\readme.json";

			var startInfo = new ProcessStartInfo
			{
				CreateNoWindow = false,
				UseShellExecute = false,
				FileName = exe,
				WindowStyle = ProcessWindowStyle.Normal,
				Arguments = exe + " " + json
			};
			try
			{
				using (var exeProcess = Process.Start(startInfo))
				{
					if (exeProcess == null)
						Assert.Fail("exeProcess is null");
					exeProcess.WaitForExit();
					var expected = File.ReadAllText(@"..\..\expected.md");
					if (!File.Exists(@"..\..\README.md"))
						Debugger.Break();
					var actual = File.ReadAllText(@"..\..\README.md");
					Assert.Equals(expected, actual);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.StackTrace);
			}
		}
	}
}

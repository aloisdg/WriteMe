using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WriteMe.Model;

namespace WriteMe
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args == null || !args.Any())
			{
				Console.WriteLine("Usage: ./WriteMe.exe path/readme.json");
				return;
			}
			Start(args[0]);
		}

		// ToDO: Check for error
		private static void Start(string path)
		{
			var json = File.ReadAllText(path);
			var project = JsonConvert.DeserializeObject<Project>(json);
			project = project.Clean(project);
			var content = WriteHelper.WriteMe(project);
			var pathReadMe = Path.Combine(Path.GetDirectoryName(path), "README.md");
			Console.WriteLine(pathReadMe);
			Console.ReadLine();
			File.WriteAllText(pathReadMe, content);
		}
	}
}
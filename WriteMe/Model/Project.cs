using System;
using System.Collections.Generic;
using System.Linq;

namespace WriteMe.Model
{
	public class Project
	{
		public Basics Basics { get; set; }
		public Version[] Versions { get; set; }

		public Project Clean(Project project)
		{
			Basics = project.Basics;
			Versions = (OrderByVersion(project.Versions)).ToArray();
			return this;
		}

		private static IEnumerable<Version> OrderByVersion(IEnumerable<Version> versions)
		{
			return from v in versions.Select(v => new
			{
				v.Name,
				v.Evolutions,
				SemVer = ExtractSemVer(v.Name)
			}).OrderByDescending(v => v.SemVer[0])
			.ThenByDescending(v => v.SemVer[1]).ThenByDescending(v => v.SemVer[2])
			       select new Version { Name = v.Name, Evolutions = v.Evolutions };
		}

		private static string[] ExtractSemVer(string version)
		{
			var semver = new string(version.Where(c => c.Equals('.') || Char.IsDigit(c)).ToArray());
			return !semver.Contains('.') ? new[] { semver, "", "" }
				: new List<string>(semver.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries)) {"", ""}.ToArray();
		}
	}

	public class Basics
	{
		public string Author { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public string Image { get; set; }
		public string Video { get; set; }
	}

	public class Version
	{
		public string Name { get; set; }
		public string[] Evolutions { get; set; }
	}
}
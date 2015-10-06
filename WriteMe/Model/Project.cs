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
			Versions = (OrderByDescendingVersion(project.Versions)).ToArray();
			return this;
		}

		/// <summary>
		/// Order by descending version an enumerable of Version
		/// </summary>
		/// <param name="versions">An enumerable of Version to sort</param>
		/// <returns>An enumerable of Version sorted</returns>
		/// <example>
		/// [[1, 2], [1, 1], [2, 0]] will give [[2, 0], [1, 2], [1, 1]]
		/// </example>
		private static IEnumerable<Version> OrderByDescendingVersion(IEnumerable<Version> versions)
		{
			return versions
				.Select(v => new { v.Name, v.Evolutions, SemVer = ExtractSemanticVersion(v.Name) })
				.OrderByDescending(v => v.SemVer[0])
				.ThenByDescending(v => v.SemVer[1])
				.ThenByDescending(v => v.SemVer[2])
				.Select(v => new Version { Name = v.Name, Evolutions = v.Evolutions });
		}

		/// <summary>
		/// Extract SemVer format from string into an array
		/// </summary>
		/// <param name="version">A version in the SemVer format</param>
		/// <returns>An array of string composed by SemVer format</returns>
		/// <example>
		/// v0 will give [0, "", ""]
		/// v1.2.3-foobar will give [1, 2, 3]
		/// </example>
		private static string[] ExtractSemanticVersion(string version)
		{
			var semver = new string(version.Where(c => c.Equals('.') || Char.IsDigit(c)).ToArray());
			return !semver.Contains('.') ? new[] { semver, String.Empty, String.Empty }
				: new List<string>(semver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)) { String.Empty, String.Empty }.ToArray();
		}
	}
}
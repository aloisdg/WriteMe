using System;
using System.Linq;
using SemVer;

namespace WriteMe.Model
{
	public class Version
	{
		public SemVersion Name { get; set; }
		public string[] Evolutions { get; set; }
	}

	public class SemVersion : SemanticVersion
	{
		public SemVersion() { }

		public SemVersion(string str) : base(Clean(str)) { }

		public static explicit operator SemVersion(string str)
		{
			return new SemVersion(str);
		}

		private static string Clean(string str)
		{
			var versionWithoutV = str.StartsWith("v") ? str.Substring(1) : str;
			var start = versionWithoutV;
			var end = String.Empty;

			if (versionWithoutV.Contains('-'))
			{
				var index = versionWithoutV.IndexOf('-');
				start = versionWithoutV.Remove(index);
				end = versionWithoutV.Substring(index);
			}

			return ExtractSemanticVersion(start) + end;
		}

		private static string ExtractSemanticVersion(string version)
		{
			var tab = new[] { "0", "0", "0" };
			var semanticVersions = new string(version.Where(c => c.Equals('.') || Char.IsDigit(c)).ToArray());
			var values = semanticVersions.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

			for (var i = 0; i < tab.Length && i < values.Length; i++)
				tab[i] = values[i];

			return String.Join(".", tab);
		}
	}
}
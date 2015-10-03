using System;
using System.Collections.Generic;
using System.Text;
using WriteMe.Model;
using Version = WriteMe.Model.Version;

namespace WriteMe
{
	public static class WriteHelper
	{
		/// <summary>
		///Write the project name as a Markdown main title
		/// </summary>
		/// <param name="name">Project name</param>
		/// <returns>Project title as a Markdown main title</returns>
		public static string WriteTitle(string name)
		{
			return String.Format("# {0}", name);
		}

		public static string WriteSummary(string summary)
		{
			return summary;
		}

		public static string WriteBadges(string author, string name)
		{
			return String.Format(@"![Build](https://img.shields.io/badge/Build-:-lightgrey.svg?style=flat-square)
[![AppVeyor](https://img.shields.io/appveyor/ci/{0}/{1}.svg?style=flat-square)](https://ci.appveyor.com/project/{0}/{2})
![Version](https://img.shields.io/badge/Version-:-lightgrey.svg?style=flat-square)
[![NuGet](https://img.shields.io/nuget/v/{1}.svg?style=flat-square)](https://www.nuget.org/packages/{1}/)
[![GitHub release](https://img.shields.io/github/release/{0}/{2}.svg?style=flat-square)](https://github.com/{0}/{1}/releases/latest)
![Miscellaneous](https://img.shields.io/badge/Miscellaneous-:-lightgrey.svg?style=flat-square)
[![GitHub license](https://img.shields.io/github/license/{0}/{2}.svg?style=flat-square)](https://github.com/{0}/{1}/blob/master/License)",
				author, name, name.ToLowerInvariant());
		}

		/// <summary>
		/// Write a demonstration as a picture or a video Markdown
		/// </summary>
		/// <param name="name">Project name</param>
		/// <param name="image">Project image url</param>
		/// <param name="video">Project video url</param>
		/// <returns>Demonstration as a picture or a video Markdown</returns>
		public static string WriteDemo(string name, string image, string video)
		{
			return String.Format(@"## Demo

[![Demo {0}]({1})]({2})", name, image, video);
		}

		public static string WriteVersion(IList<Version> versions)
		{
			Func<int, string> line = i => i + 1 < versions.Count ? Environment.NewLine : String.Empty;
			var stringBuilder = new StringBuilder("## Evolutions" + Environment.NewLine + Environment.NewLine);
			for (var i = 0; i < versions.Count; i++)
			{
				stringBuilder.AppendFormat("### {0}{1}{1}", versions[i].Name, Environment.NewLine);
				foreach (var evolution in versions[i].Evolutions)
					stringBuilder.AppendFormat("* {0}{1}", evolution, line(i));
				stringBuilder.Append(line(i));
			}
			return stringBuilder.ToString();
		}

		public static string WriteIssue(string author, string name)
		{
			return String.Format(@"## Bug Reports & Feature Requests

You can help by reporting bugs, suggesting features, reviewing feature specifications or just by sharing your opinion.

Use [GitHub Issues](https://github.com/{0}/{1}/issues) for all of that.", author, name);
		}

		// ToDo
		public static string WriteContributing()
		{
			return @"## Contributing

1. Fork the project.
2. Create a branch for your new feature.
3. Write tests.
4. Write code to make the tests pass.
5. Submit a pull request.

All pull requests are welcome !";
		}

		public static string WriteMe(Project project)
		{
			return String.Join(Environment.NewLine + Environment.NewLine,
				WriteTitle(project.Basics.Name),
				WriteSummary(project.Basics.Summary),
				WriteBadges(project.Basics.Author, project.Basics.Name),
				WriteDemo(project.Basics.Name, project.Basics.Image, project.Basics.Video),
				WriteVersion(project.Versions),
				WriteIssue(project.Basics.Author, project.Basics.Name),
				WriteContributing());
		}
	}
}

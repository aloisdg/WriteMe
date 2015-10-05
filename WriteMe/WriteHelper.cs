using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Schema;
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

		public static string WriteInstall()
		{
			throw new NotImplementedException();
		}

		public static string WriteNugetInstall(string name)
		{
			string s = String.Format(@"You can install {0} as a nuget package: Install-Package {0}

{0} is a Portable Class Library with support for .Net 4+, SilverLight 5, Windows Phone 8 and Win Store applications. Also {0} symbols nuget package is published so you can step through {0} code while debugging your code.",
				name);
			return s;
		}

		public static string WriteVersion(IList<Version> versions)
		{
			Func<bool, string> lineOrEmpty = b => b ? Environment.NewLine : String.Empty;
			Func<int, int, bool> isLimit = (n, limit) => n + 1 < limit;
			Func<int, string> line = n => lineOrEmpty(isLimit(n, versions.Count));

			var stringBuilder = new StringBuilder("## Evolutions" + Environment.NewLine + Environment.NewLine);
			for (var i = 0; i < versions.Count; i++)
			{
				stringBuilder.AppendFormat("### {0}{1}{1}", versions[i].Name, Environment.NewLine);
				for (int index = 0; index < versions[i].Evolutions.Length; index++)
				{
					var length = versions[i].Evolutions.Length;
					stringBuilder.AppendFormat("* {0}{1}",
						versions[i].Evolutions[index],
						lineOrEmpty(!String.IsNullOrEmpty(line(i)) || isLimit(index, length)));
				}
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

		public static string WriteContributing(string author, string name)
		{
			return String.Format(@"## Contributing

1. Talk about your feature on [issues](https://github.com/{0}/{1}/issues).
2. [Fork](https://help.github.com/articles/fork-a-repo/) the project.
3. Create a branch for your awesome feature.
4. Write tests.
5. Write code to make the tests pass.
6. [Submit a pull request](https://help.github.com/articles/creating-a-pull-request/).

All pull requests are welcome !", author, name);
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
				WriteContributing(project.Basics.Author, project.Basics.Name));
		}
	}
}

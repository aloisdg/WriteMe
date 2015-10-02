using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WriteMe.Model;
using Version = WriteMe.Model.Version;

namespace WriteMe
{
	public static class WriteHelper
	{
		public static string WriteTitle(string title)
		{
			return String.Format("# {0}", title);
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

		public static string WriteDemo(string name, string image, string video)
		{
			return String.Format(@"## Demo

[![Demo {0}]({1})]({2})", name, image, video);
		}

		public static string WriteVersion(IList<Version> versions)
		{
			var stringBuilder = new StringBuilder("## Evolutions" + Environment.NewLine + Environment.NewLine);
			for (var i = 0; i < versions.Count; i++)
			{
				var version = versions[i];
				stringBuilder.AppendLine("### " + version.Name + Environment.NewLine);
				foreach (var evolution in version.Evolutions)
				{
					stringBuilder.Append("* " + evolution);
					if (i + 1 < versions.Count)
						stringBuilder.AppendLine();
				}
				if (i + 1 < versions.Count)
					stringBuilder.AppendLine();
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

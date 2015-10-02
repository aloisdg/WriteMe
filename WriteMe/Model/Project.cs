namespace WriteMe.Model
{
	public class Project
	{
		public Basics Basics { get; set; }
		public Version[] Versions { get; set; }
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
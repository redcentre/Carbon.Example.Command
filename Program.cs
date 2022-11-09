using System;
using System.Threading.Tasks;
using RCS.Carbon.Shared;
using RCS.Carbon.Tables;

namespace Carbon.Example.Command
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var engine = new CrossTabEngine();
			await engine.GetFreeLicence("GitHub Example App");
			engine.OpenJob("rcsruby", "demo");
			var sprops = new XSpecProperties();
			var dprops = new XDisplayProperties();
			dprops.Output.Format = XOutputFormat.TSV;
			string report = engine.GenTab("Demo of Age × Region", "Age", "Region", null, null, sprops, dprops);
			Console.WriteLine(report);
			engine.CloseJob();
			Console.WriteLine("PAUSE...");
			Console.ReadLine();
		}
	}
}

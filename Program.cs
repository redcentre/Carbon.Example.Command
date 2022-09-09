using System;
using System.Threading.Tasks;
using RCS.Carbon.Shared;
using RCS.Carbon.Tables;

namespace Carbon.Example.Command
{
	internal class Program
	{
		// This value is for temporary testing only
		const string TestLicensing = "https://rcsapps.azurewebsites.net/licensing2test/";

		static async Task Main(string[] args)
		{
			var engine = new CrossTabEngine();
			await engine.GetFreeLicence("Demo App", TestLicensing);
			engine.OpenJob("client1rcs", "demo");
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

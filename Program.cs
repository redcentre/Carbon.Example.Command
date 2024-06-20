using System;
using System.Threading.Tasks;
using RCS.Carbon.Licensing.RedCentre;
using RCS.Carbon.Shared;
using RCS.Carbon.Tables;

namespace Carbon.Example.Command;

internal class Program
{
	// Choose either the live or testing licensing server base address.
	// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	const string LicenceBaseAddress = "https://rcsapps.azurewebsites.net/licensing2/";
	//const string LicenceBaseAddress = "https://rcsapps.azurewebsites.net/licensing2test/";

	static async Task Main(string[] args)
	{
		var engine = new CrossTabEngine(new RedCentreLicensingProvider(LicenceBaseAddress));
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

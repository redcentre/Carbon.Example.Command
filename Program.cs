using System;
using System.Threading.Tasks;
using RCS.Carbon.Licensing.RedCentre;
using RCS.Carbon.Licensing.Shared;
using RCS.Carbon.Shared;
using RCS.Carbon.Tables;

namespace Carbon.Example.Command;

/// <summary>
/// For information see the Wiki page.
/// </summary>
internal class Program
{
	const string LicenceBaseAddress = "https://rcsapps.azurewebsites.net/licensing2test/";

	static async Task Main(string[] _)
	{
		var engine = new CrossTabEngine(new RedCentreLicensingProvider(LicenceBaseAddress));
		LicenceInfo lic = await engine.GetFreeLicence("GitHub Example App", true);
		Console.WriteLine($"Licence -> {lic.Id} { lic.Name}");
		engine.OpenJob("rcsruby", "demo");
		Console.WriteLine("Opened job");
		var sprops = new XSpecProperties();
		var dprops = new XDisplayProperties();
		dprops.Output.Format = XOutputFormat.TSV;
		string report = engine.GenTab("Demo of Age × Region", "Age", "Region", null, null, sprops, dprops);
		Console.WriteLine(report);
		bool closed= engine.CloseJob();
		Console.WriteLine($"Job closed -> {closed}");
		Console.WriteLine("PAUSE...");
		Console.ReadLine();
	}
}

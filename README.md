# Overview

This project contains the simplest possible example of how C# code can use the Carbon library to generate a cross-tabulation report. The next section explains the key lines of code, and is followed by a section that discusses the code in more detail and includes related issues and links.

## Code Explanation

```
using RCS.Carbon.Shared;
using RCS.Carbon.Tables;
```

These two using statements are the minimum required for generating a simple report. Other `using` statements may be required for more advanced features such as import, export, variable management, etc, and are discussed in other example projects.

```
var engine = new CrossTabEngine();
```

The `CrossTabEngine` class contains the Carbon API methods related to report generation and management. An instance of this class can normally live for the lifetime of single-threaded applications such as commands and desktop GUI apps. Engine lifetime in complex hosting environments such as services is discussed in other example projects.

```
await engine.GetFreeLicence("Example Project");
```

Carbon must be provided with licence credentials. This example uses the `GetFreeLicence` method to indicate that an anonymous client is using the *free* licence that is available for evaluation, educational use  or small business use. Customers with an existing Red Centre Software product licence can use the `LoginId` method to enter their credentials, which is discussed in other sample projects.

```
engine.OpenJob("rcsruby", "demo");
```

Opens a Cloud *job* identified by customer "rcsruby" and job "demo". Carbon normally has one job open and active at any time.

```
var sprops = new XSpecProperties();
```

Create an instance of a specification properties class which can provide advanced control over how a report is generated. A default class instance using all default values is satisfactory.

```
var dprops = new XDisplayProperties();
dprops.Output.Format = XOutputFormat.TSV;
```

Create an instance of a display properties class which can provide detailed control over report appearance. A single property is set to specify that the report should be genrated in TSV (tab separated values) format. Other default values are satisfactory.

```
string report = engine.GenTab("Demo of Age × Region", "Age", "Region", null, null, sprops, dprops);
```

A cross-tabulation report in TSV format is generated with a specified title, top variable "Age" and side variable "Region". The returned string is a set of text lines joined with a newline (\n) character, which display like the following partial screenshot.

![Carbon Report][img1]

```
engine.CloseJob();
```

Closes any open and active job. Technically not needed in this simple example, but it is the correct coding pattern and is shown for completeness.

## Discussion Points

### Jobs

> Carbon operates on units of data called *jobs*. A job is a collection of related data such as survey results, variables, saved reports, custom settings and much more. Jobs can be stored in folders in the local filesystem or they can be stored in Azure Cloud storage containers. The Cloud job used in the code is part of a set of public sample jobs published by Red Centre Software. 

### Display Properties

> The `XDisplayProperties` class used in the code contains over 250 properties which control every aspect of the report shape and appearance. All properties are listed in Appendix 5 of the [Carbon Scripting](#) PDF document. The defaults are suitable for most simple usage scenarios.

### Report Format

> The Carbon engine can produce cross-tabulation reports in a variety of formats. This example specifies the TSV output text format, but alternative text formats include CSV, HTML, JSON and XML. The JSON can be returned in the exact shape required by the Python [pandas][pandas] library to create dataframes. Carbon can also directly produce an Excel Workbook.

### Variables

> The `GenTab` call line uses the top and side variables "Age" and "Region" respectively, but it would be fair to ask how it's possible to know such variable names exist. For that purpose, Carbon provides the methods `ListVartreeNames` and `GetVartreeAsNodes` to list groups of variables and drill down into their names and relationships. A developer writing a GUI app could use those methods to create a variable selector.

### Avanced Scenarios

> The Carbon API is comprehensive and provides methods for full management of jobs and reports. It is possible to use these methods to create a complete management system over cross-tabulation reports. The [Carbon.Example.Desktop][sampwpf] repository contains a moderately sophisticated GUI project that exercises a large part of the Carbon API. Developers can use that project as a starting point for writing their own applications based on Carbon.

[img1]:https://rcsapps.azurewebsites.net/doc/carbon/articles/img/demo-cons-output.png
[pandas]: https://pandas.pydata.org/
[sampwpf]: https://github.com/redcentre/Carbon.Example.Desktop

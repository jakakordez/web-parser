# Web parser

The parser should run on any system supported by .NET Core: Windows, Ubuntu, Mac but is tested only on Windows.
1. .NET Core Framework 2.2 must be installed
2. Set the desired html file path string and method in launchSettings.json. Examples:
  - "\"../../../sites/overstock.com/jewelry01.html\" regex"
  - "\"../../../sites/overstock.com/jewelry01.html\" xpath"
  - "\"../../../sites/overstock.com/jewelry01preprocessed.html\" roadrunner \"../../../sites/overstock.com/jewelry02preprocessed.html\""
3. Run the project WebParser.csproj:
	- with Visual Studio

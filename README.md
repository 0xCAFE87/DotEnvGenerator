#### DotEnvSource Generator
#### A simple Roslyn source generators for dotenv files

This is still an ungoing amateur project, but I really wanted to try out the source generators.

This SourceGenerator will create a class for your .dot.env files, each .env file also requires a schema json file describing the type of each entry in your dotnenv file in order to genreate a class that gives you proper types.

The required structure for the json file is as follows:

```json
	[
		{
			"name":"THE_NAME_OF_YOUR_VARIABLE",
			"type": "DotNET Type (System.Int32, System.String, etc)"
		}
	]
```

So far the generator can only create accessors for Int, String, Double and Booleans. And it will only work with one file.

Your target project also requires dotenv.net. But if the assembly is not in your project the analyzer will issue a warning.
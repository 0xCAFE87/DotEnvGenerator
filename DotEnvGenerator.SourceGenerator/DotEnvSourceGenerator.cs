using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DotEnvGenerator.SourceGenerator
{
    [Generator]
    public class DotEnvSourceGenerator
        : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
           
            var envFiles = context.AdditionalFiles.Where(f => f.Path.EndsWith(".env", StringComparison.OrdinalIgnoreCase));
            var schemaFiles = context.AdditionalFiles.Where(f => f.Path.EndsWith(".schema.json", StringComparison.OrdinalIgnoreCase));

            if (!envFiles.Any())
            {
                return;
            }

            //now we should check for the dotenv.net assembly
            if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals("dotenv.net", StringComparison.OrdinalIgnoreCase)))
            {
                var descriptor = new DiagnosticDescriptor("DOTENVGEN01",
                   "Couldn't start generator",
                   "The target project requires dotenv.net, please install the nuget package",
                   "DotEnvSourceGenerator",
                   DiagnosticSeverity.Error, true);
                context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None));
            }

            if (envFiles.Count() != schemaFiles.Count())
            {
                var descriptor = new DiagnosticDescriptor("DOTENVGEN02",
                    "Couldn't start generator", 
                    "The number of .env and .schema files do not match",
                    "DotEnvSourceGenerator",
                    DiagnosticSeverity.Error, true);
                context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None));
            }
            
            var processor = new SchemaProcessor.SchemaProcessor();
            foreach(var schema in schemaFiles)
            {
                var schemaFile = processor.CreateSchemaFileFromText(schema);
                string klass = ConfigurationClassesCreator.ClassesCreator.CreateClass(schemaFile);
                context.AddSource(schemaFile.FileName, klass);
            }
            
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            //if (!Debugger.IsAttached)
            //    Debugger.Launch();
#endif
        }
    }
}

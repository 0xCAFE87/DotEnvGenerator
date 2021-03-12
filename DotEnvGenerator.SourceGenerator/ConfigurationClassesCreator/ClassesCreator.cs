using DotEnvGenerator.SourceGenerator.SchemaProcessor;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace DotEnvGenerator.SourceGenerator.ConfigurationClassesCreator
{
    public static class ClassesCreator
    {
        private static TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        private static string CreateFields(List<SchemaEntry> entries)
        {
            var sb = new StringBuilder();
            
            foreach(var entry in entries)
            {
                var t = entry.GetEntryType();
                object type;
                if(t.Name == "String")
                {
                    type = "placeholder";
                }
                else
                {
                    type = FormatterServices.GetUninitializedObject(t);
                }
                dynamic dynaVar = Convert.ChangeType(type, t);
                var getMethodBody = ClassesCreator.GenerateWithTypeField(entry.Name, dynaVar);
                var titleCase = ConvertToTitleCase(entry.Name.ToLowerInvariant());
                sb.AppendLine($"public {t.Name} {titleCase} {{ get {{ return {getMethodBody} }}}}");
            }

            return sb.ToString();
        }

        private static string GenerateWithTypeField(string propertyName, double data)
        {
            return $"_reader.GetDoubleValue(\"{propertyName}\");";
        }

        private static string GenerateWithTypeField(string propertyName, string data)
        {
            return $"_reader.GetStringValue(\"{propertyName}\");";
        }

        private static string GenerateWithTypeField(string propertyName, int data)
        {
            return $"_reader.GetIntValue(\"{propertyName}\");";
        }

        private static string GenerateWithTypeField(string propertyName, bool data)
        {
            return $"_reader.GetBooleanValue(\"{propertyName}\");";
        }

        private static string ConvertToTitleCase(string aString)
        {
            return _textInfo.ToTitleCase(aString);
        }

        public static string CreateClass(SchemaFile schemaFile)
        {
            var sb = new StringBuilder();
            var className = ConvertToTitleCase(schemaFile.FileName);
            sb.AppendLine("using System;");
            sb.AppendLine("using dotenv.net;");
            sb.AppendLine("using dotenv.net.Interfaces;");
            sb.AppendLine("using dotenv.net.Utilities;");
            sb.AppendLine("namespace DotEnvConfig");
            sb.AppendLine("{");
            
            sb.AppendLine($"     public class {className}");
            sb.AppendLine("      {");
            sb.AppendLine("    private IEnvReader _reader = new EnvReader();");
            sb.Append(GenerateConstructor(className, schemaFile.EnvFilePath));
            sb.Append(CreateFields(schemaFile.Entries));
            sb.AppendLine("      }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateConstructor(string className, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"         public {className}()");
            sb.AppendLine("         {");
            sb.AppendLine($"            DotEnv.Config(true, @\"" + fileName + "\");");
            sb.AppendLine("          }");
            return sb.ToString();
            
        }
    }
}

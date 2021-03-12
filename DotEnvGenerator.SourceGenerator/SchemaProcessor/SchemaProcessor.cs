using DotEnvGenerator.SourceGenerator.SchemaProcessor;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DotEnvGenerator.SourceGenerator.SchemaProcessor
{
    public class SchemaProcessor
    {
        private List<SchemaEntry> GetSchemaEntries(AdditionalText text)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SchemaEntry>>(text.GetText().ToString());
            }
            catch (JsonException ex)
            {
                throw ex;
            }
            
        }

        public SchemaFile CreateSchemaFileFromText(AdditionalText text)
        {
            return new SchemaFile()
            {
                FilePath = text.Path,
                Entries = GetSchemaEntries(text)
            };
        }
    }
}

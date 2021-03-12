using System;
using System.Collections.Generic;
using System.Text;

namespace DotEnvGenerator.SourceGenerator.SchemaProcessor
{
    public class SchemaEntry
    {
        public string Name { get; set; }
        public string Type { get; set; }
        

        public Type GetEntryType()
        {
            return System.Type.GetType(Type);
        }
    }
}

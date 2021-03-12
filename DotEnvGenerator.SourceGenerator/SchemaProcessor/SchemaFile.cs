using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DotEnvGenerator.SourceGenerator.SchemaProcessor
{
    public class SchemaFile
    {
        private static Regex _regex = new Regex(@"(?<nombre>[a-zA-Z0-9\.-]*)\b\.schema\.json$");
        
        public string FilePath { get; set; }
        public List<SchemaEntry> Entries { get; set; }
        
        public string FileName { get {
                var match = _regex.Matches(FilePath);
                return match[0].Groups["nombre"].Value;
            } }

        public string EnvFilePath { get
            {
                return FilePath.Replace(".schema.json", ".env");
            } }
    }
}

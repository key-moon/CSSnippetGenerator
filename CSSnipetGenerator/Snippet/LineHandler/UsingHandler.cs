using System;
using System.Linq;
using System.Collections.Generic;

partial class CodeSnippet
{
    const string DeclarationBeginToken = "#if !DECLARATIONS";
    private class UsingHandler : LineHandler
    {
        List<string> Imports = new List<string>();

        public UsingHandler(CodeSnippet snippet) : base(snippet) { }

        public override LineHandler NextLine(string line)
        {
            if (!line.StartsWith("using"))
            {
                if (line == "") return this;
                FinalizeSnippet();
                if (line == DeclarationBeginToken) return new DeclarationHandler(SnippetObject).NextLine(line);
                return new CodeHandler(SnippetObject).NextLine(line);
            }
            var import = line.Substring("using".Length).Trim(' ', ';');
            Imports.Add(import);
            return this;
        }

        public override void FinalizeSnippet()
        {
            if (Imports.Count != 0) SnippetObject.Snippet.Add(new CodeSnippetImports() { Import = Imports.Select(x => new CodeSnippetImportsImport() { Namespace = x }).ToArray() });
        }
    }
}
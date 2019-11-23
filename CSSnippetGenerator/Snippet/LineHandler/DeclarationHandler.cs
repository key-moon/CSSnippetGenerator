using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

partial class CodeSnippet
{
    private class DeclarationHandler : LineHandler
    {
        int depth = 0;
        List<string> LiteralTokens = new List<string>();

        StringBuilder ToolTipBuilder = new StringBuilder();
        List<DeclarationsElement> Declarations = new List<DeclarationsElement>();
        public DeclarationHandler(CodeSnippet snippet) : base(snippet) { }

        public override LineHandler NextLine(string line)
        {
            if (line.StartsWith("#if")) depth++;
            else if (line == "#endif")
            {
                depth--;
                if (depth <= 0)
                {
                    FinalizeSnippet();
                    return new CodeHandler(SnippetObject, LiteralTokens);
                }
            }
            else if (line.StartsWith("//"))
            {
                ToolTipBuilder.AppendLine(line.TrimStart('/'));
            }
            else if (line.EndsWith(';'))
            {
                string id, @default, tooltip;
                if (line.Contains('='))
                {
                    var tokens = line.Split('=').TakeLast(2).ToArray();
                    id = tokens[0].Trim().Split(' ').Last();
                    @default = tokens[1].Trim().TrimEnd(';');
                }
                else
                {
                    id = line.Split(' ').Last().TrimEnd(';');
                    @default = id.TrimStart('@');
                }
                tooltip = ToolTipBuilder.ToString().Trim();
                ToolTipBuilder.Clear();
                AddDeclaration(id, @default, tooltip);
            }
            return this;
        }

        public override void FinalizeSnippet()
        {
            if (Declarations.Count != 0) SnippetObject.Snippet.Add(new CodeSnippetDeclarations() { Items = Declarations });
        }

        public void AddDeclaration(string id, string @default, string tooltip)
        {
            if (LiteralTokens.Contains(id)) return;
            LiteralTokens.Add(id);
            var literal = new CodeSnippetDeclarationsLiteral(editable: true, id: id.TrimStart('@'), @default: @default, tooltip: tooltip);
            Declarations.Add(literal);
        }
    }
}

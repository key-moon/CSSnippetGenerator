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
                if (depth < 0)
                {
                    FinalizeSnippet();
                    return new CodeHandler(SnippetObject, LiteralTokens);
                }
            }
            else if (line.StartsWith("//"))
            {
                ToolTipBuilder.Append("\n");
                ToolTipBuilder.Append(line.TrimStart('/'));
            }
            else
            {
                var tokens = line.Split('=').TakeLast(2).ToArray();
                if (tokens.Length < 2) return this;
                var nameToken = tokens[0].Trim().Split(' ').Last();
                var defaultToken = tokens[1].Trim().Split(' ').First().TrimEnd(';');
                LiteralTokens.Add(nameToken);
                var literal = new CodeSnippetDeclarationsLiteral() { Editable = true, ItemsElementName = new List<CodeSnippetDeclarationsLiteral.ItemsChoiceType>(), Items = new List<string>() };
                literal.Items.Add(nameToken.TrimStart('@'));
                literal.ItemsElementName.Add(CodeSnippetDeclarationsLiteral.ItemsChoiceType.ID);
                literal.Items.Add(defaultToken);
                literal.ItemsElementName.Add(CodeSnippetDeclarationsLiteral.ItemsChoiceType.Default);
                literal.Items.Add(ToolTipBuilder.ToString());
                literal.ItemsElementName.Add(CodeSnippetDeclarationsLiteral.ItemsChoiceType.ToolTip);
                ToolTipBuilder.Clear();
                Declarations.Add(literal);
            }
            return this;
        }

        public override void FinalizeSnippet()
        {
            SnippetObject.Snippet.Add(new CodeSnippetDeclarations() { Items = Declarations });
        }
    }
}

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
                ToolTipBuilder.Append("\n");
                ToolTipBuilder.Append(line.TrimStart('/'));
            }
            else
            {
                var tokens = line.Split('=').TakeLast(2).ToArray();
                if (tokens.Length < 2) return this;
                var nameToken = tokens[0].Trim().Split(' ').Last();
                var defaultToken = tokens[1].Trim().Split(' ').First().TrimEnd(';');
                if (LiteralTokens.Contains(nameToken))
                {
                    ToolTipBuilder.Clear();
                    return this;
                }
                LiteralTokens.Add(nameToken);
                List<string> items = new List<string>();
                List<CodeSnippetDeclarationsLiteral.DeclarationsLiteralItemsChoiceType> itemsElementName = new List<CodeSnippetDeclarationsLiteral.DeclarationsLiteralItemsChoiceType>();
                items.Add(nameToken.TrimStart('@'));
                itemsElementName.Add(CodeSnippetDeclarationsLiteral.DeclarationsLiteralItemsChoiceType.ID);
                items.Add(defaultToken);
                itemsElementName.Add(CodeSnippetDeclarationsLiteral.DeclarationsLiteralItemsChoiceType.Default);
                items.Add(ToolTipBuilder.ToString().Trim());
                itemsElementName.Add(CodeSnippetDeclarationsLiteral.DeclarationsLiteralItemsChoiceType.ToolTip);
                var literal = new CodeSnippetDeclarationsLiteral() { Editable = true, ItemsElementName = itemsElementName.ToArray(), Items = items.ToArray() };
                ToolTipBuilder.Clear();
                Declarations.Add(literal);
            }
            return this;
        }

        public override void FinalizeSnippet()
        {
            if (Declarations.Count != 0) SnippetObject.Snippet.Add(new CodeSnippetDeclarations() { Items = Declarations });
        }
    }
}

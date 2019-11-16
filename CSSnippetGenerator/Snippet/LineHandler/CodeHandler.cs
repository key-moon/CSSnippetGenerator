using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

partial class CodeSnippet
{
    private class CodeHandler : LineHandler
    {
        IEnumerable<string> LiteralTokens;
        StringBuilder codeBuilder = new StringBuilder();
        public CodeHandler(CodeSnippet snippet) : this(snippet, Enumerable.Empty<string>()) { }
        public CodeHandler(CodeSnippet snippet, IEnumerable<string> literalTokens) : base(snippet)
        {
            LiteralTokens = literalTokens;
        }

        public override LineHandler NextLine(string line)
        {
            codeBuilder.AppendLine(line.Replace("$", "$$"));
            return this;
        }

        public override void FinalizeSnippet()
        {
            var code = codeBuilder.ToString().Trim();
            code = code.Replace("/*cursor*/", "$end$");
            foreach (var token in LiteralTokens)
                code = code.Replace(token, $"${token.TrimStart('@')}$");
            SnippetObject.Snippet.Add(new CodeSnippetCode() { Language = "CSharp", Text = new string[] { code } });
        }
    }
}

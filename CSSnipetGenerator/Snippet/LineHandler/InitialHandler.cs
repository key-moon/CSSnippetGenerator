
partial class CodeSnippet
{
    private class InitialHandler : LineHandler
    {
        public InitialHandler(CodeSnippet snippetObject) : base(snippetObject) { }
        public override LineHandler NextLine(string line)
        {
            if (line.StartsWith("//")) return new HeaderHandler(SnippetObject).NextLine(line);
            return this;
        }

        public override void FinalizeSnippet() { }
    }
}
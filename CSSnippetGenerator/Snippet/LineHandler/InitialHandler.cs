
partial class CodeSnippet
{
    private class InitialHandler : LineHandler
    {
        public InitialHandler(CodeSnippet snippetObject) : base(snippetObject) 
        {
            SnippetObject.Format = "1.0.0";
        }
        public override LineHandler NextLine(string line)
        {
            if (line.StartsWith("//")) return new HeaderHandler(SnippetObject).NextLine(line);
            return this;
        }

        public override void FinalizeSnippet() 
        {
            SnippetObject = null;
        }
    }
}
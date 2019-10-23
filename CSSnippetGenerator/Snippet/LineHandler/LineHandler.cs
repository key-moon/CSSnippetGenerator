

partial class CodeSnippet
{
    private abstract class LineHandler
    {
        public CodeSnippet SnippetObject { get; protected set; }
        public LineHandler(CodeSnippet snippetObject) { SnippetObject = snippetObject; }
        public abstract LineHandler NextLine(string line);
        public abstract void FinalizeSnippet();
    }
}
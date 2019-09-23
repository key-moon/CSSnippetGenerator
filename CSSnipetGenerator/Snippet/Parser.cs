using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

partial class CodeSnippet
{
    static CodeSnippet Parse(StreamReader reader)
    {
        LineHandler handler = new InitialHandler(new CodeSnippet());
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            handler = handler.NextLine(line);
        }
        handler.FinalizeSnippet();
        return handler.SnippetObject;
    }
}

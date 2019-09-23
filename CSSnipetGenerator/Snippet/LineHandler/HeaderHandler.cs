using System;
using System.Linq;
using System.Collections.Generic;



partial class CodeSnippet
{
    private class HeaderHandler : LineHandler
    {
        private List<object> Items;
        private List<CodeSnippetHeader.ItemsChoiceType> ElementNames;

        public HeaderHandler(CodeSnippet snippetObject) : base(snippetObject)
        {
            SnippetObject.Header = new CodeSnippetHeader();
        }
        public override LineHandler NextLine(string line)
        {
            if (!line.StartsWith("//"))
            {
                SnippetObject.Header.Items = Items.ToArray();
                SnippetObject.Header.ItemsElementName = ElementNames.ToArray();
                return new UsingHandler(SnippetObject).NextLine(line);
            }
            var trimmed = line.TrimStart('/').Split(':', 2).Select(x => x.Trim()).ToArray();

            if (trimmed.Length < 2) return this;
            (object item, CodeSnippetHeader.ItemsChoiceType ElementName) =
            trimmed[0].ToLower() switch
            {
                "author" => ((object)trimmed[1], CodeSnippetHeader.ItemsChoiceType.Author),
                "description" => (trimmed[1], CodeSnippetHeader.ItemsChoiceType.Description),
                "helpurl" => (trimmed[1], CodeSnippetHeader.ItemsChoiceType.HelpUrl),
                "keywords" =>
                (
                    new CodeSnippetKeywords()
                    {
                        Keyword = trimmed[1].Split(',').Select(x => x.Trim()).ToArray()
                    },
                    CodeSnippetHeader.ItemsChoiceType.Keywords
                ),
                "shortcut" => (trimmed[1], CodeSnippetHeader.ItemsChoiceType.Shortcut),
                "Sdnippettypes" =>
                (
                    new CodeSnippetSnippetTypes()
                    {
                        SnippetType = trimmed[1].Split(',').Select(x => x.Trim()).ToArray()
                    },
                    CodeSnippetHeader.ItemsChoiceType.SnippetTypes
                ),
                "title" => (trimmed[1], CodeSnippetHeader.ItemsChoiceType.Title),
                _ => throw new KeyNotFoundException()
            };
            Items.Add(item);
            ElementNames.Add(ElementName);
            return this;
        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;

partial class CodeSnippet
{
    private class HeaderHandler : LineHandler
    {
        List<object> Items = new List<object>();
        List<CodeSnippetHeader.HeaderItemsChoiceType> ItemsElementName = new List<CodeSnippetHeader.HeaderItemsChoiceType>();
        public HeaderHandler(CodeSnippet snippetObject) : base(snippetObject)
        {
            SnippetObject.Header = new CodeSnippetHeader();
        }

        public override LineHandler NextLine(string line)
        {
            if (!line.StartsWith("//"))
            {
                FinalizeSnippet();
                return new UsingHandler(SnippetObject).NextLine(line);
            }
            
            var trimmed = line.TrimStart('/').Split(':', 2).Select(x => x.Trim()).ToArray();

            if (trimmed.Length < 2) return this;
            (object item, CodeSnippetHeader.HeaderItemsChoiceType ElementName) =
            trimmed[0].ToLower() switch
            {
                "author" => ((object)trimmed[1], CodeSnippetHeader.HeaderItemsChoiceType.Author),
                "description" => (trimmed[1], CodeSnippetHeader.HeaderItemsChoiceType.Description),
                "helpurl" => (trimmed[1], CodeSnippetHeader.HeaderItemsChoiceType.HelpUrl),
                "keywords" =>
                (
                    new CodeSnippetKeywords()
                    {
                        Keyword = trimmed[1].Split(',').Select(x => x.Trim()).ToArray()
                    },
                    CodeSnippetHeader.HeaderItemsChoiceType.Keywords
                ),
                "shortcut" => (trimmed[1], CodeSnippetHeader.HeaderItemsChoiceType.Shortcut),
                "Sdnippettypes" =>
                (
                    new CodeSnippetSnippetTypes()
                    {
                        SnippetType = trimmed[1].Split(',').Select(x => x.Trim()).ToArray()
                    },
                    CodeSnippetHeader.HeaderItemsChoiceType.SnippetTypes
                ),
                "title" => (trimmed[1], CodeSnippetHeader.HeaderItemsChoiceType.Title),
                _ => throw new KeyNotFoundException()
            };
            Items.Add(item);
            ItemsElementName.Add(ElementName);
            return this;
        }

        public override void FinalizeSnippet()
        {
            SnippetObject.Header.Items = Items.ToArray();
            SnippetObject.Header.ItemsElementName = ItemsElementName.ToArray();
        }
    }
}
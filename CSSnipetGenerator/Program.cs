using System;
using System.IO;
using System.Xml.Serialization;

namespace CSSnipetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var snippets = new CodeSnippets();
            foreach (var path in Directory.GetFiles(Path.Combine(Secrets.LibraryDirectory, "src"), "*.csx", SearchOption.AllDirectories))
            {
                using StreamReader reader = new StreamReader(path);
                var parsed = CodeSnippet.Parse(reader);
                snippets.CodeSnippet.Add(parsed);
            }
            var serializer = new XmlSerializer(typeof(CodeSnippets));
            using StreamWriter writer = new StreamWriter(Path.Combine(Secrets.LibraryDirectory, "snippets/snippet.snippet"));
            serializer.Serialize(writer, snippets);
        }
    }
}

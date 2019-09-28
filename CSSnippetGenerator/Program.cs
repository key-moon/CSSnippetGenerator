using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CSSnippetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var libDir = args.Length == 0 ? GetLibraryDirectory() : args[0];
            
            var snippets = new CodeSnippets();
            foreach (var path in Directory.GetFiles(Path.Combine(libDir, "src"), "*.csx", SearchOption.AllDirectories))
            {
                using StreamReader reader = new StreamReader(path);
                var parsed = CodeSnippet.Parse(reader);
                snippets.CodeSnippet.Add(parsed);
            }

            var serializer = new XmlSerializer(typeof(CodeSnippets));
            using StreamWriter writer = new StreamWriter(Path.Combine(libDir, "snippets/snippet.snippet"));
            serializer.Serialize(writer, snippets);
        }

        static string GetLibraryDirectory()
        {
            Console.WriteLine("ライブラリのソースファイルが格納されているディレクトリを指定してください。");
            return Console.ReadLine();
        }
    }
}

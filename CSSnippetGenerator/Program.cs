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

            if (!Directory.Exists(libDir)) throw new DirectoryNotFoundException("指定されたディレクトリが見つかりません。");

            var srcDir = Path.Combine(libDir, "src");
            var snippetsDir = Path.Combine(libDir, "snippets");

            Directory.CreateDirectory(snippetsDir);
            Directory.CreateDirectory(srcDir);

            var snippets = new CodeSnippets();
            foreach (var path in Directory.GetFiles(srcDir, "*.csx", SearchOption.AllDirectories))
            {
                using StreamReader reader = new StreamReader(path);
                var parsed = CodeSnippet.Parse(reader);
                if (parsed is null) continue;
                snippets.CodeSnippet.Add(parsed);
            }

            var serializer = new XmlSerializer(typeof(CodeSnippets));
            using StreamWriter writer = new StreamWriter(Path.Combine(snippetsDir, "snippet.snippet"));
            serializer.Serialize(writer, snippets);
        }

        static string GetLibraryDirectory()
        {
            Console.WriteLine("ライブラリのソースファイルが格納されているディレクトリを指定してください。");
            return Console.ReadLine();
        }
    }
}

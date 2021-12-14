using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Corsis.Xhtml;

namespace Test
{
    class Program
    {
        static void ReportEncodings(string desc, IEnumerable<string> encodings)
        {
            Console.WriteLine("## {0} ##", desc);
            foreach (var encoding in encodings.OrderBy(a => a))
                Console.WriteLine(encoding);
        }

        static void Main(string[] args)
        {
            HashSet<string> dotNetOnly, iconvOnly, both;
            Html2Xhtml.SupportedEncodings.GetSupportSets(out both, out dotNetOnly, out iconvOnly);
            ReportEncodings(" both  ", both);
            ReportEncodings(" .net  ", dotNetOnly);
            ReportEncodings(" iconv ", iconvOnly);



            Console.WriteLine(Html2Xhtml.SupportedEncodings.Contains("UTF-8"));

            Html2XhtmlBasicExample();
            Html2XDocumentExample1();
            Html2XDocumentExample2();
            Html2XDocumentExample3();

            Console.ReadKey(true);
        }

        static string html = "<html><head><TITLE>title</TITLE></head><body>I♥NY☢<p>b<br>c: <img src=2 nonsense=x></a></body></html>";

        static void Html2XhtmlBasicExample()
        {
            var xhtml = Html2Xhtml.RunAsFilter(stdin => stdin.Write(html), compactBlockElements: true).ReadToEnd();
            Console.WriteLine(xhtml);
            File.WriteAllText("1.out", xhtml);
        }

        static void Html2XDocumentExample1()
        {
            var xhtml = Html2Xhtml.RunAsFilter(stdin => stdin.Write(html), compactBlockElements: true).ReadToEnd();
            var xml   = XhtmlNamedEntities.NameToNumber.Map(xhtml);
            var xdoc  = XDocument.Parse(xml);
            Console.WriteLine(xdoc);
            File.WriteAllText("2.out", xdoc.ToString());
        }

        static void Html2XDocumentExample2()
        {
            var xhtml = Html2Xhtml.RunAsFilter(stdin => stdin.Write(html), compactBlockElements: true).ReadToEnd();
            var xml1  = XhtmlNamedEntities.NameToNumber.Map(xhtml);
            var xml2  = Html2XhtmlExtensions.RipNamespace(xml1);
            var xdoc  = XDocument.Parse(xml2);
            Console.WriteLine(xdoc);
            File.WriteAllText("3.out", xdoc.ToString());
        }

        static void Html2XDocumentExample3()
        {
            var xdoc = Html2Xhtml.RunAsFilter(stdin => { Console.ReadKey(true); stdin.Write(html); }, Html2Xhtml.DocType.Xhtml_1_1);//.ReadToXDocument();
            Console.WriteLine(xdoc);
            File.WriteAllText("4.out", xdoc.ToString());
        }
    }
}

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerge6
{
    class Program
    {

        public static void Merge(List<String> InFiles, String OutFile)
        {
            try
            {
                using (FileStream stream = new FileStream(OutFile, FileMode.Create))
                using (Document doc = new Document())
                using (PdfCopy pdf = new PdfCopy(doc, stream))
                {
                    doc.Open();

                    PdfReader reader = null;
                    PdfImportedPage page = null;

                    InFiles.ForEach(file =>
                    {
                        reader = new PdfReader(file);

                        for (int i = 0; i < reader.NumberOfPages; i++)
                        {
                            page = pdf.GetImportedPage(reader, i + 1);
                            pdf.AddPage(page);
                        }

                        pdf.FreeReader(reader);
                        reader.Close();
                    });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        static string ProcessingFolder = @"C:\Test\Merge3\";
        static string DestinationFolder = @"C:\Test\Merge3\Merged\";
        static string MessageFolder = @"C:\Test\Merge3\Message\";

        static void Main(string[] args)
        {
            try
            {
                var dir = new DirectoryInfo(ProcessingFolder);
                FileInfo[] files = dir.GetFiles("*.pdf");

                foreach (var item in files)
                {
                    List<string> Files = new List<string>();
                    Files.Add(item.FullName);
                    Files.Add(MessageFolder + "message.pdf");

                    Merge(Files, DestinationFolder + item.Name);
                }

                Console.WriteLine("Done");
                
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

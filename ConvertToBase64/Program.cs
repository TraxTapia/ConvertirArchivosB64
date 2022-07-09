using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace ConvertToBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            //string pathFilePdf = @"C:\Users\ivan.tapia\Downloads\Facturas Canceladas\A439B481-7AB7-4D1E-B4CD-C4D7BA1FA403.pdf";
            //string pathFileXML = @"C:\Users\ivan.tapia\Downloads\Facturas Canceladas\A439B481-7AB7-4D1E-B4CD-C4D7BA1FA403.xml";
            //string resultBase64Pdf = EncodeFileToBase64(pathFilePdf);
            //string Base64xml = EncodeFileToBase64(pathFileXML);
            //string resultBase64Xml = contenidoArchivoBase64(Base64xml);
            //Console.WriteLine("Base 64 pdf = "+ resultBase64Pdf);
            //Console.WriteLine("Base 64 xml = "+ resultBase64Xml);
            var path = @"C:\Temp\file.zip";
            string[] files = Directory.GetFiles(@"C:\Temp\files");
            var zipFile = createFileZip();
            var zipFiles = FilesZip(path,files);

            Console.WriteLine(zipFile);
            Console.WriteLine(zipFiles);
            Console.ReadLine();


        }
        public static string EncodeFileToBase64(string pathFile)
        {
            string encodedFile = string.Empty;
            try
            {

                encodedFile = Convert.ToBase64String(System.IO.File.ReadAllBytes(pathFile));
            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo convertir el archivo a base 64 " + ex.Message);
            }
            return encodedFile;

        }
        public static string contenidoArchivoBase64(string archivoBase64)
        {
            var archivoByte = Convert.FromBase64String(archivoBase64);
            var contentFile = Encoding.UTF8.GetString(archivoByte, 0, archivoByte.Length);
            return contentFile;

        }

        //Crea y escribe en el archivo para generar un zip
        public static string createFileZip()
        {

            string mensaje = string.Empty;
            try
            {

                
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry("foo.txt");
                        using (var entryStream = demoFile.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write("Bar!");
                        }



                    }

                    using (var fileStream = new FileStream(@"C:\Temp\test2.zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
                mensaje = "Se creo el zip correctamente";


            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un problema al generar el archivo zip \n" + ex.Message;
            }
            return mensaje;

        }
        //crea  un archivo Zip con varios documentos en su contenido
        public static string FilesZip(string pathSaveFile,string[] PathFiles)
        {
            string mensaje = string.Empty;
            try
            {

                using (var archive = ZipFile.Open(pathSaveFile, ZipArchiveMode.Create))
                {
                    foreach (var fPath in PathFiles)
                    {
                        archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                    }
                }
                mensaje = "Se creo el zip correctamente con todos los archivos";


            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un problema al generar el archivo zip \n" + ex.Message;
            }
            return mensaje;

        }
    }

}





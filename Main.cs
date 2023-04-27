using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;
using Wox.Plugin;
using System.Linq;
using System.Windows.Forms;

namespace base64_gzip_decoder
{
    public class Main : IPlugin
    {
        public void Init(PluginInitContext context) { }
        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            string result = Decompress(String.Join("", query.Terms.Skip(1)));
            results.Add(new Result()
            {
                Title = "BB",
                SubTitle = result,
                IcoPath = "Images\\gzip.png",
                Action = e =>
                {
                    Clipboard.SetText(result);
                    return true;
                }
            });
            return results;
        }

        public static string Decompress(string compressedText)
        {
            byte[] array = Convert.FromBase64String(compressedText);

            string @string;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(array, 0);
                memoryStream.Write(array, 4, array.Length - 4);
                byte[] array2 = new byte[num];
                memoryStream.Position = 0L;
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gzipStream.Read(array2, 0, array2.Length);
                }
                @string = Encoding.UTF8.GetString(array2);
            }
            return @string;
        }
    }
}
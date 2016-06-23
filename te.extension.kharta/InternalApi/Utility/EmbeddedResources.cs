using System.Text;
using System.IO;

namespace te.extension.kharta.InternalApi.Utility
{
    internal static class EmbeddedResources
    {
        internal static string GetString(string path)
        {
            using (System.IO.Stream stream = GetStream(path))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                var text = Encoding.UTF8.GetString(data);
                if (text[0] > 255)
                    return text.Substring(1);
                else
                    return text;
            }
        }

        internal static Stream GetStream(string path)
        {
            return typeof(SourceDataService).Assembly.GetManifestResourceStream(path);
        }
    }
}

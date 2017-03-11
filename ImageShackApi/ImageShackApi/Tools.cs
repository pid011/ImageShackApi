using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    public class Tools
    {
        public static string GetContentType(FileInfo info)
        {
            switch (info.Extension.ToLower())
            {
                case ".jpg":
                    return "image/jpg";

                case ".jpeg":
                    return "image/jpeg";

                case ".gif":
                    return "image/gif";

                case ".png":
                    return "image/png";

                case ".bmp":
                    return "image/bmp";

                case ".tif":
                    return "image/tiff";

                case ".tiff":
                    return "image/tiff";

                default:
                    return "image/unknown";
            }
        }

        public static void Write(MemoryStream p_memoryStream, String p_data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(p_data);
            p_memoryStream.Write(bytes, 0, bytes.Length);
        }
    }
}

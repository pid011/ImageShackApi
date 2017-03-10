using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    public class ImageShackUploader
    {
        public static string ApiKey { get; set; }

        public static UploadResult UploadImage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new InvalidOperationException("The API Key is empty.");
            }

            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException($"file not found: {filePath}");
            }

            var result = new UploadResult();

            return result;
        }
    }
}

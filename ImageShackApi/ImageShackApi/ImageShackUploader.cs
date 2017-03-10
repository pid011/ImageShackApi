using Newtonsoft.Json.Linq;
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

            // return JsonParse();
        }

        private UploadResult JsonParse(Stream imageInfoJson)
        {
            string json = null;
            using (var reader = new StreamReader(imageInfoJson))
            {
                json = reader.ReadToEnd();
            }

            if (json == null)
            {
                throw new NullReferenceException();
            }
            JObject o = JObject.Parse(json);

            return new UploadResult()
            {
                Rating = new UploadResult.RatingDetails()
                {
                    Ratings = int.Parse((string)o["rating"]["ratings"]),
                    Average = double.Parse((string)o["rating"]["avg"])
                },
                Links = new UploadResult.LinkDetails()
                {
                    ImageLink = (string)o["links"]["image_link"],
                    ThumbLink = (string)o["links"]["thumb_link"]
                },
                Resolution = new UploadResult.ResolutionDetails()
                {
                    Width = int.Parse((string)o["resolution"]["width"]),
                    Height = int.Parse((string)o["resolution"]["height"])
                }
            };
            
        }
    }
}

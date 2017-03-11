using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    /// <summary>
    /// ImageShack image uploader class.
    /// </summary>
    public class ImageShackUploader
    {
        /// <summary>
        /// API key. this property is must be filled.
        /// </summary>
        public static string ApiKey { get; set; }

        /// <summary>
        /// Upload the image to ImageShack.
        /// </summary>
        /// <param name="filePath">path of file.</param>
        /// <returns>uploaded result</returns>
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

            var values = new NameValueCollection
            {
                { "key", ApiKey },
                { "format", "json" }
            };

            var files = new NameValueCollection
            {
                { "fileupload", filePath }
            };

            return JsonParse(SendHttpRequest(@"https://post.imageshack.us/upload_api.php", values, files));
        }

        private static string SendHttpRequest(string url, NameValueCollection values, NameValueCollection files = null)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            // The first boundary
            byte[] boundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            // The last boundary
            byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
            byte[] boundaryBytesF = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            // Create the request and set parameters
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get request stream
            Stream requestStream = request.GetRequestStream();

            foreach (string key in values.Keys)
            {
                // Write item to stream
                byte[] formItemBytes = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Write(formItemBytes, 0, formItemBytes.Length);
            }

            if (files != null)
            {
                foreach (string key in files.Keys)
                {
                    if (File.Exists(files[key]))
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[2048];
                        byte[] formItemBytes = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                        using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                // Write file content to stream, byte by byte
                                requestStream.Write(buffer, 0, bytesRead);
                            }

                            fileStream.Close();
                        }
                    }
                }
            }

            // Write trailer and close stream
            requestStream.Write(trailer, 0, trailer.Length);
            requestStream.Close();

            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return reader.ReadToEnd();
            };
        }

        private static UploadResult JsonParse(string json)
        {
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

        private static UploadResult JsonParse(Stream imageInfoJson)
        {
            string json = null;
            using (var reader = new StreamReader(imageInfoJson))
            {
                json = reader.ReadToEnd();
            }

            return JsonParse(json);
        }
    }
}

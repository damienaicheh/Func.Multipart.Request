using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Func.SendFile.Tests.Helpers
{
    public class HttpHelper
    {
        public static DefaultHttpRequest CreateMultipartRequest(FormCollection formCollection)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Method = "POST",
                ContentType = "multipart/form-data",
                Form = formCollection,
            };

            return request;
        }

        public static IFormFile CreateFormFile(string keyname, string filePath, string contentType)
        {
            using (var stream = File.OpenRead(filePath))
                return new FormFile(stream, 0, stream.Length, keyname, filePath)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
        }
    }
}
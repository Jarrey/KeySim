using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace KeySim.Common
{
    public static class HttpUtils
    {
        public const string HEADER_COTENT_TYPE = "Content-type";

        public static HttpWebResponse Get(string uri, WebHeaderCollection headers = null)
        {
            HttpWebRequest request = CreateRequest(uri, headers);
            return (HttpWebResponse)request.GetResponse();
        }

        public async static Task<HttpWebResponse> GetAsync(string uri, WebHeaderCollection headers = null)
        {
            HttpWebRequest request = CreateRequest(uri, headers);
            return (HttpWebResponse)await request.GetResponseAsync();
        }

        public static string ReadResponse(HttpWebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public async static Task<string> ReadResponseAsync(HttpWebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        private static HttpWebRequest CreateRequest(string uri, WebHeaderCollection headers = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            if (headers != null) request.Headers = headers;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return request;
        }
    }
}

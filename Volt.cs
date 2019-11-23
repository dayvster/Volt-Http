namespace Arctekdev.Volt {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;
    // Client class
    public static class VHttp {
        public static HttpClient Http { get; private set; }
        public static HttpResponseMessage Response { get; private set; }

        public static HttpResponseMessage Get(Uri uri) {
            return Send(uri, HttpMethod.Get);
        }
		public static HttpResponseMessage Get(string URL) {
			return Send(new Uri(URL), HttpMethod.Get);
		}
        public static HttpResponseMessage Get(Uri uri, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Get, null, customHeaders);
        }
        public static HttpResponseMessage PostText(Uri uri, string text) {
            return Send(uri, HttpMethod.Post, new StringContent(text));
        }
        public static HttpResponseMessage PostText(Uri uri, string text, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(text), customHeaders);
        }
        public static HttpResponseMessage PostJson(Uri uri, string json) {
            return Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"));
        }
        public static HttpResponseMessage PostJson(Uri uri, string json, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"), customHeaders);
        }
        public static HttpResponseMessage PostJson(Uri uri, Object jsonObject) {
            return Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"));
        }
        public static HttpResponseMessage PostJson(Uri uri, Object jsonObject, Dictionary<string, string> customheaders) {
            return Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"), customheaders);
        }

        public static HttpResponseMessage Post(Uri uri, string content, string contentType) {
            return Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType));
        }
        public static HttpResponseMessage Post(Uri uri, string content, string contentType, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
        }
        public static HttpResponseMessage Post(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string, string >customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(content, encoding, contentType), customHeaders);
        }

        public static HttpResponseMessage Put(Uri uri, string content) {
            return Send(uri, HttpMethod.Put, new StringContent(content));
        }
        public static HttpResponseMessage Put(Uri uri, string content, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content), customHeaders);
        }
        public static HttpResponseMessage Put(Uri uri, string content,string contentType, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
        }
        public static HttpResponseMessage Put(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content, encoding, contentType), customHeaders);
        }
   
        public static HttpResponseMessage Delete(Uri uri) {
            return Send(uri, HttpMethod.Delete);
        }

        public static HttpResponseMessage Delete(Uri uri, Dictionary<string,string> customheaders) {
            return Send(uri, HttpMethod.Delete, null, customheaders);
        }


        public static HttpResponseMessage GetResult() {
            return Response;
        }
        public static string GetResultAsString() {
            return Response.Content.ReadAsStringAsync().Result;
        }

        public static HttpResponseMessage BasicAuth(Uri uri, StringContent content, string authstring) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = HttpMethod.Post,
                Content = content
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(authstring)));
            return Http.SendAsync(req).Result;
        }

        private static HttpResponseMessage Send(Uri uri, HttpMethod method, HttpContent content, Dictionary<string, string> headers) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = method,
                Content = content,
            };
            foreach (KeyValuePair<string, string> header in headers) {
                req.Headers.Add(header.Key, header.Value);
            }
            return Http.SendAsync(req).Result;
        }
        private static HttpResponseMessage Send(Uri uri, HttpMethod method, HttpContent content) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = method,
                Content = content,
            };
            return Http.SendAsync(req).Result;
        }
        private static HttpResponseMessage Send(Uri uri, HttpMethod method) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = method
            };
            return Http.SendAsync(req).Result;
        }
    }
}

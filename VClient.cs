namespace Volt.Http {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;
    // Client class
    public class VClient {
        public HttpClient Http { get; private set; }
        public HttpResponseMessage Response { get; private set; }

        public VClient() {
            Http = new HttpClient();
        }

        public HttpResponseMessage Get(Uri uri) {
            return Send(uri, HttpMethod.Get);
        }
        public HttpResponseMessage Get(Uri uri, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Get, null, customHeaders);
        }
        public VClient SendGet(Uri uri) {
            Response = Send(uri, HttpMethod.Get);
            return this;
        }
        public VClient SendGet(Uri uri, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Get, null, customHeaders);
            return this;
        }
        public HttpResponseMessage PostText(Uri uri, string text) {
            return Send(uri, HttpMethod.Post, new StringContent(text));
        }
        public HttpResponseMessage PostText(Uri uri, string text, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(text), customHeaders);
        }
        public VClient SendPostText(Uri uri, string text) {
            Response = Send(uri, HttpMethod.Post, new StringContent(text));
            return this;
        }
        public VClient SendPostText(Uri uri, string text, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Post, new StringContent(text), customHeaders);
            return this;
        }
        public HttpResponseMessage PostJson(Uri uri, string json) {
            return Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"));
        }
        public HttpResponseMessage PostJson(Uri uri, string json, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"), customHeaders);
        }
        public HttpResponseMessage PostJson(Uri uri, Object jsonObject) {
            return Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"));
        }
        public HttpResponseMessage PostJson(Uri uri, Object jsonObject, Dictionary<string, string> customheaders) {
            return Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"), customheaders);
        }
        public VClient SendPostJson(Uri uri, string json) {
            Response = Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"));
            return this;
        }

        public VClient SendPostJson(Uri uri, string json, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Post, new StringContent(json, Encoding.UTF8, "application/json"), customHeaders);
            return this;
        }

        public VClient SendPostJson(Uri uri, Object jsonObject) {
            Response = Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"));
            return this;
        }

        public VClient SendPostJson(Uri uri, Object jsonObject, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json"), customHeaders);
            return this;
        }

        public HttpResponseMessage Post(Uri uri, string content, string contentType) {
            return Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType));
        }
        public HttpResponseMessage Post(Uri uri, string content, string contentType, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
        }
        public HttpResponseMessage Post(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string, string >customHeaders) {
            return Send(uri, HttpMethod.Post, new StringContent(content, encoding, contentType), customHeaders);
        }
        public VClient SendPost(Uri uri, string content, string contentType) {
            Response = Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType));
            return this;
        }
        public VClient SendPost(Uri uri, string content, string contentType, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
            return this;
        }
        public VClient SendPost(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Post, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
            return this;
        }

        public HttpResponseMessage Put(Uri uri, string content) {
            return Send(uri, HttpMethod.Put, new StringContent(content));
        }
        public HttpResponseMessage Put(Uri uri, string content, Dictionary<string, string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content), customHeaders);
        }
        public HttpResponseMessage Put(Uri uri, string content,string contentType, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
        }
        public HttpResponseMessage Put(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string,string> customHeaders) {
            return Send(uri, HttpMethod.Put, new StringContent(content, encoding, contentType), customHeaders);
        }
        public VClient SendPut(Uri uri, string content) {
            Response = Send(uri, HttpMethod.Put, new StringContent(content));
            return this;
        }
        public VClient SendPut(Uri uri, string content, Dictionary<string,string> customHeaders) {
            Response = Send(uri, HttpMethod.Put, new StringContent(content), customHeaders);
            return this;
        }
        public VClient SendPut(Uri uri, string content, string contentType, Dictionary<string, string> customHeaders) {
            Response = Send(uri, HttpMethod.Put, new StringContent(content, Encoding.UTF8, contentType), customHeaders);
            return this;
        }
        public VClient SendPut(Uri uri, string content, string contentType, Encoding encoding, Dictionary<string, string> customHeaders) {
            Response = Send(uri, HttpMethod.Put, new StringContent(content, encoding, contentType), customHeaders);
            return this;
        }

        public HttpResponseMessage Delete(Uri uri) {
            return Send(uri, HttpMethod.Delete);
        }

        public HttpResponseMessage Delete(Uri uri, Dictionary<string,string> customheaders) {
            return Send(uri, HttpMethod.Delete, null, customheaders);
        }
        public VClient SendDelete(Uri uri) {
            Response = Send(uri, HttpMethod.Delete);
            return this;
        }

        public HttpResponseMessage GetResult() {
            return Response;
        }
        public string GetResultAsString() {
            return Response.Content.ReadAsStringAsync().Result;
        }

        public HttpResponseMessage BasicAuth(Uri uri, StringContent content, string authstring) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = HttpMethod.Post,
                Content = content
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(authstring)));
            return Http.SendAsync(req).Result;
        }

        private HttpResponseMessage Send(Uri uri, HttpMethod method, HttpContent content, Dictionary<string, string> headers) {
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
        private HttpResponseMessage Send(Uri uri, HttpMethod method, HttpContent content) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = method,
                Content = content,
            };
            return Http.SendAsync(req).Result;
        }
        private HttpResponseMessage Send(Uri uri, HttpMethod method) {
            HttpRequestMessage req = new HttpRequestMessage() {
                RequestUri = uri,
                Method = method
            };
            return Http.SendAsync(req).Result;
        }
    }
}

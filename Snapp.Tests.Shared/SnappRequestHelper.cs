using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Snapp.Tests.Shared
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT
    }
    public interface IHttpCommand<TIn, TOut>
    {
        TOut Execute(TIn requestObj);
    }
    public class SnappRequestHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public string URL;

        public SnappRequestHelper(string url, IHttpClientFactory _httpClientFactory)
        {
            this.URL = url;
            this._httpClientFactory = _httpClientFactory;
        }

        public class HttpCommandFactory
        {
            public static  IHttpCommand<TIn, TOut> CommandFor<TIn, TOut>(HttpMethod httpMethod, string url, HttpClient httpClient)
            {
                switch (httpMethod)
                {
                    case HttpMethod.GET: return new HttpGetCommand<TIn, TOut>(httpClient, url);
                    case HttpMethod.POST: return new HttpPostCommand<TIn, TOut>(httpClient, url);
                    case HttpMethod.PUT: return new HttpPutCommand<TIn, TOut>(httpClient, url);
                    default: throw new Exception();
                }
            }
        }
    }


    public class HttpGetCommand<TIn, TOut> : IHttpCommand<TIn, TOut>
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public HttpGetCommand(HttpClient httpClient, string url)
        {
            this.httpClient = httpClient;
            this.url = url;

        }
        public TOut Execute(TIn requestObj)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            string data = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TOut>(data);
        }
    }


    public class HttpPostCommand<TIn, TOut> : IHttpCommand<TIn, TOut>
    {
        private readonly HttpClient httpClient;
        private readonly string url;
        public HttpPostCommand(HttpClient httpClient, string url)
        {
            this.httpClient = httpClient;
            this.url = url;

        }
        public TOut Execute(TIn requestObj)
        {
            string json = JsonConvert.SerializeObject(requestObj);
            var stringContent = new StringContent(json,
                         UnicodeEncoding.UTF8,
                         "application/json");
            string data = httpClient.PostAsync(new Uri(url), stringContent).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TOut>(data);
        }
    }


    public class HttpPutCommand<TIn, TOut> : IHttpCommand<TIn, TOut>
    {
        private readonly HttpClient httpClient;
        private readonly string url;
        public HttpPutCommand(HttpClient httpClient, string url)
        {
            this.httpClient = httpClient;
            this.url = url;

        }
        public TOut Execute(TIn requestObj)
        {
            string json = JsonConvert.SerializeObject(requestObj);
            var stringContent = new StringContent(json,
                         UnicodeEncoding.UTF8,
                         "application/json");
            string data = httpClient.PutAsync(new Uri(url), stringContent).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TOut>(data);
        }
    }
}


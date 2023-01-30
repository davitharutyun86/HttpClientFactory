using System.Net.Http;
using System.Threading.Tasks;
using static Snapp.Tests.Shared.SnappRequestHelper;

namespace Snapp.Tests.Shared
{
    //public  class Requests
    //{
    //    private readonly IHttpClientFactory httpClientFactory;
    //    public Requests(IHttpClientFactory httpClientFactory)
    //    {
    //        this.httpClientFactory = httpClientFactory;
    //    }
    //    public TOut MakeRequest<TIn, TOut>( HttpMethod method, string url, TIn model)
    //    {
    //            return HttpCommandFactory.CommandFor<TIn, TOut>(method, url, httpClientFactory.CreateClient()).Execute(model);

    //    }
    //    //public static TOut MakeRequest<TIn, TOut>(HttpClient client, HttpMethod method, string url, TIn model)
    //    //{
    //    //    return HttpCommandFactory.CommandFor<TIn, TOut>(method, url, new IHttpClientFactory).Execute(model);
    //    //}
    //    public  TOut MakeRequest<TOut>(HttpMethod method, string url)
    //    {
    //        return  MakeRequest<string, TOut>(method, url, null);
    //    }
    //    //public static TOut MakeRequest<TOut>(HttpClient client, HttpMethod method, string url)
    //    //{
    //    //    return MakeRequest<string, TOut>(client, method, url, null);
    //    //}
    //}

    public class Requests
    {
        private readonly IHttpClientFactory httpClientFactory;
        public Requests(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<TOut> MakeRequest<TIn, TOut>(HttpMethod method, string url, TIn model)
        {
            return await HttpCommandFactory.CommandFor<TIn, TOut>(method, url, httpClientFactory.CreateClient()).Execute(model);

        }
        //public static TOut MakeRequest<TIn, TOut>(HttpClient client, HttpMethod method, string url, TIn model) 
        //{ 
        //    return HttpCommandFactory.CommandFor<TIn, TOut>(method, url, new IHttpClientFactory).Execute(model); 
        //} 
        public async Task<TOut> MakeRequest<TOut>(HttpMethod method, string url)
        {
            return await MakeRequest<string, TOut>(method, url, null);
        }
        //public static TOut MakeRequest<TOut>(HttpClient client, HttpMethod method, string url) 
        //{ 
        //    return MakeRequest<string, TOut>(client, method, url, null); 
        //} 
    }

}

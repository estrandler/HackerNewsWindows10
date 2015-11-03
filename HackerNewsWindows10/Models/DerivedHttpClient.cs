using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HackerNewsWindows10.Models
{
    public class DerivedHttpClient : HttpClient
    {
        public DerivedHttpClient(string baseAdress) : base()
        {
            this.BaseAddress = new Uri(baseAdress);
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
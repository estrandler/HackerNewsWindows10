using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using HackerNewsWindows10.Models;
using Newtonsoft.Json;

namespace HackerNewsWindows10.Services
{
    public class NewsService
    {
        private const string Url = "https://hacker-news.firebaseio.com/v0/";
        private const string Item = "item/";

        public async Task<string[]> GetStoryIDs(string typeOfStory)
        {
            if (typeOfStory == null) typeOfStory = "topstories";

            using (var client = new DerivedHttpClient(Url))
            {
                var req = string.Format("{0}.json", typeOfStory);
                var response = client.GetAsync(req);
                response.Wait();
                if (!response.IsCompleted) throw new Exception("API-call failed");
                var res = await response.Result.Content.ReadAsStringAsync();
                return Util.StringToArray(res);
            }
        }

        internal async Task<Story> GetStory(string id)
        {
            using (var client = new DerivedHttpClient(Url))
            {
                var req = string.Format("{0}{1}.json?print=pretty", Item, id);
                var response = client.GetAsync(req);
                response.Wait();
                if (!response.IsCompleted) throw new Exception("API-call failed");
                var res = await response.Result.Content.ReadAsStringAsync();
                var s = JsonConvert.DeserializeObject<Story>(res);
                s.readableTime = s.SetReadableTime();

                return s;
            }
        }
    }
}
using Newtonsoft.Json;
using QSearch.Core.Impl.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core.Impl
{
    public class StackExchangeApiClientBasedOnHttpClient : IStackExchangeApiConsumer
    {
        private const string urltemplate = "http://api.stackexchange.com/2.2/search?order=desc&sort=activity&intitle={0}&site=stackoverflow";
        public IEnumerable<SearchResult> Search(SearchQuery query)
        {
            var url = string.Format(urltemplate, query.QueryText);

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                var responsedto = JsonConvert.DeserializeObject<QuestSearchResponse>(response);
                if (responsedto.items != null && response.Length > 0)
                {
                    foreach(var item in responsedto.items)
                    {
                        yield return new SearchResult() { link = item.link, title = item.title };
                    }
                }
            }
        }
    }
}

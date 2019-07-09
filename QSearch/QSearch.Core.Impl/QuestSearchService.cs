using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core.Impl
{
    public class QuestSearchService : IQuestSearchService
    {
        private ICacheServiceAsync cachesrv;
        private IStackExchangeApiConsumer apiclient;

        public QuestSearchService(ICacheServiceAsync cachesrv, IStackExchangeApiConsumer apiclient)
        {
            this.cachesrv = cachesrv;
            this.apiclient = apiclient;
        }

        public List<SearchResult> Search(SearchQuery query)
        {
            var task = Task.Run(async () => await cachesrv.getFromCache(query));            
            if (task.Result != null && task.Result.Count > 0)
            {
                return task.Result;
            }

            var res = this.apiclient.Search(query);
            Task.Run(async () => await  cachesrv.addToCache(query, new List<SearchResult>(res)));            
            return new List<SearchResult>(res);
        }
    }
}

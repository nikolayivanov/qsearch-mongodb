using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core
{
    public interface ICacheServiceAsync
    {
        Task<List<SearchResult>> getFromCache(SearchQuery query);

        Task addToCache(SearchQuery query, List<SearchResult> result);
    }
}
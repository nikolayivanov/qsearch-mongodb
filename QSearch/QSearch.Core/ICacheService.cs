using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core
{
    public interface ICacheService
    {
        List<SearchResult> getFromCache(SearchQuery query);

        void addToCache(SearchQuery query, List<SearchResult> result);
    }
}
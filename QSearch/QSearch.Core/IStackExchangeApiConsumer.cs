using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core
{
    public interface IStackExchangeApiConsumer
    {        IEnumerable<SearchResult> Search(SearchQuery query);
    }
}

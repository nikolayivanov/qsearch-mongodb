﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core
{
    public interface IQuestSearchService
    {
        List<SearchResult> Search(SearchQuery query);
    }
}

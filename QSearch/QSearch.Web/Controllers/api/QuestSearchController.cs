using QSearch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QSearch.Web.Controllers.api
{
    public class QuestSearchController : ApiController
    {        
        [Route("api/qsearch/search/{query}")]
        [HttpGet]
        public IHttpActionResult Search([FromUri]string query)
        {
            return Ok(new List<SearchResult>());
        }
    }
}
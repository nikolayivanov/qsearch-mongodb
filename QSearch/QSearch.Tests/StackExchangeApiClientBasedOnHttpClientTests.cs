using Microsoft.VisualStudio.TestTools.UnitTesting;
using QSearch.Core.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSearch.Core.Impl.Tests
{
    [TestClass()]
    public class StackExchangeApiClientBasedOnHttpClientTests
    {
        [TestMethod()]
        public void SearchTest()
        {
            var query = new SearchQuery()
            {
                QueryText = "java"
            };

            var srv = new StackExchangeApiClientBasedOnHttpClient();
            var res = srv.Search(query);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count() > 0);
        }
    }
}
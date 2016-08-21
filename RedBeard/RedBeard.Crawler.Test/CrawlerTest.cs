using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedBeard.Crawler.Test
{
    [TestClass]
    public class CrawlerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PaniniCrawler c = new PaniniCrawler(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055");
            c.GetLast();
        }
    }
}

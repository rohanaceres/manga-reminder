using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBeard.Crawler.Model;

namespace RedBeard.Crawler.Test
{
    [TestClass]
    public class CrawlerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Manga c = new PaniniCrawler(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055")
                .GetLast();

            PaniniAlert alert = new PaniniAlert("ceres.rohana@gmail.com");
            alert.SendIt(c);
        }
    }
}

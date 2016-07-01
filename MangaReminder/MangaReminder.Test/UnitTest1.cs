using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MangaReminder.Crawler;
using MangaReminder.Model;
using System.Diagnostics;

namespace MangaReminder.Test
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        public void Crawler_test()
        {
            MangaCrawler crawler = new MangaCrawler("http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=118925");
            //MangaCrawler crawler = new MangaCrawler("http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055");
            Manga last = crawler.GetLast();

            Debug.WriteLine(last.Name);
            Debug.WriteLine(last.Price);
            Debug.WriteLine(last.ReleaseDate);
        }
        //[TestMethod]
        public void Alert_test()
        {
            MangaAlert alert = new MangaAlert("ceres.rohana@gmail.com");
            //alert.SendIt();
        }
        [TestMethod]
        public void Crawler_and_Alert_Test()
        {
            MangaCrawler crawler = new MangaCrawler("http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055");
            Manga last = crawler.GetLast();

            MangaAlert alert = new MangaAlert("ceres.rohana@gmail.com");
            alert.SendIt(last);
        }
    }
}

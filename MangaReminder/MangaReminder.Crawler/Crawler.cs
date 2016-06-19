using MangaReminder.Model;
using MangaReminder.Model.Exceptions;
using NSoup;
using NSoup.Nodes;
using System;
using System.Globalization;

namespace MangaReminder.Crawler
{
    public class Crawler
    {
        internal const int HttpTimeOut = 30000;
        Uri Uri { get; set; }

        public Crawler(string uri)
        {
            this.Uri = new Uri(uri);
        }

        public Manga GetLast()
        {
            Uri newUri = GetMangaDetailsUri();

            IConnection connection = NSoupClient.Connect(newUri.AbsoluteUri);
            connection.Timeout(Crawler.HttpTimeOut);
            Document document = connection.Get();

            Manga self = new Manga();
            self.Name = document.GetElementsByClass("titles").First.GetElementsByTag("h1").Text;
            self.Price = this.GetMangaPriceFromDetails(document);
            self.ReleaseDate = this.GetMangaReleaseDateFromDetails(document);

            return self;
        }

        private Uri GetMangaDetailsUri()
        {
            IConnection connection = NSoupClient.Connect(this.Uri.AbsoluteUri);
            connection.Timeout(Crawler.HttpTimeOut);
            Document document = connection.Get();

            string redirect = document.GetElementsByClass("item").First
                .GetElementsByClass("item").First
                .GetElementsByAttribute("href").First
                .Attributes.GetValue("href");

            return new Uri(this.Uri, redirect);
        }
        private decimal GetMangaPriceFromDetails(Document details)
        {
            string priceStr = details.GetElementsByClass("price").First.Text().Split(' ')[8];
            decimal price;

            Decimal.TryParse(priceStr, NumberStyles.Any, new CultureInfo("EN-us"), out price);

            if (price == 0)
            {
                throw new CorruptedMangaException("price");
            }

            return price;
        }
        private DateTime GetMangaReleaseDateFromDetails(Document details)
        {
            string dateTimeStr = details.GetElementsByClass("price").First.Text().Split(' ')[5];
            DateTime releaseDate;

            DateTime.TryParse(dateTimeStr, out releaseDate);

            if (releaseDate == null)
            {
                throw new CorruptedMangaException("release date");
            }

            return releaseDate;
        }
    }
}

using MangaReminder.Model;
using MangaReminder.Model.Exceptions;
using NSoup;
using NSoup.Nodes;
using System;
using System.Globalization;

namespace MangaReminder.Crawler
{
    /// <summary>
    /// Crawler para o site http://www.paninicomics.com.br/web/guest/home
    /// </summary>
    public class MangaCrawler
    {
        /// <summary>
        /// Especifica o timeout do download da pagina HTML.
        /// </summary>
        internal const int HttpTimeOut = 30000;
        /// <summary>
        /// URL do mangá a ser crawleado.
        /// </summary>
        Uri Uri { get; set; }

        /// <summary>
        /// Define a propriedade <see cref="Uri"/> de acordo com a
        /// string recebida como parâmetro.
        /// </summary>
        /// <param name="url">URL do site.</param>
        public MangaCrawler(string url)
        {
            this.Uri = new Uri(url);
        }

        /// <summary>
        /// Abre a página do mangá e busca pela ultima edição
        /// lançada.
        /// </summary>
        /// <returns>A ultima edição lançada.</returns>
        public Manga GetLast()
        {
            Uri newUri = GetLastMangaDetailsUri();

            IConnection connection = NSoupClient.Connect(newUri.AbsoluteUri);
            connection.Timeout(MangaCrawler.HttpTimeOut);
            Document document = connection.Get();

            Manga self = new Manga();
            self.Name = document.GetElementsByClass("titles").First.GetElementsByTag("h1").Text;
            self.Price = this.GetMangaPriceFromDetails(document);
            self.ReleaseDate = this.GetMangaReleaseDateFromDetails(document);

            return self;
        }

        /// <summary>
        /// Retorna a URI de detalhes do ultimo mangá
        /// lançado.
        /// </summary>
        /// <returns>A URI que contem os detalhos do ultimo mangá lançado.</returns>
        private Uri GetLastMangaDetailsUri()
        {
            IConnection connection = NSoupClient.Connect(this.Uri.AbsoluteUri);
            connection.Timeout(MangaCrawler.HttpTimeOut);
            Document document = connection.Get();

            string redirect = document.GetElementsByClass("item").First
                .GetElementsByClass("item").First
                .GetElementsByAttribute("href").First
                .Attributes.GetValue("href");

            return new Uri(this.Uri, redirect);
        }
        /// <summary>
        /// De acordo com o HTML da página de detalhes do mangá,
        /// esse método busca o preço do mangá.
        /// </summary>
        /// <param name="details">A página HTML de detalhes do mangá.</param>
        /// <returns>O preço do mangá.</returns>
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
        /// <summary>
        /// De acordo com o HTML da página de detalhes do mangá,
        /// esse método busca a data de lançamento do mangá.
        /// </summary>
        /// <param name="details">A página HTML de detalhes do mangá.</param>
        /// <returns>A data de lançamento do mangá.</returns>
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

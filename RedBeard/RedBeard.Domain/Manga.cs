using System;

namespace RedBeard.Domain
{
    /// <summary>
    /// Representa as informações do site da Panini sobre o mangá.
    /// </summary>
    public class Manga
    {
        /// <summary>
        /// Nome do mangá.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Data de lançamento do mangá.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Preço do mangá.
        /// </summary>
        public decimal Price { get; set; }
    }
}

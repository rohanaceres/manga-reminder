namespace RedBeard.Crawler.Exception
{
    public class CorruptedMangaException : System.Exception
    {
        public CorruptedMangaException(string corruptedField) : base("Details extracted from Panini web page are corrupted. Field: " + corruptedField)
        {

        }
    }
}

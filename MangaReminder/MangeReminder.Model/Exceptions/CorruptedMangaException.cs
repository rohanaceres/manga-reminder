using System;

namespace MangaReminder.Model.Exceptions
{
    public class CorruptedMangaException : Exception
    {
        public CorruptedMangaException(string corruptedField) : base("Details extracted from Panini web page are corrupted. Field: " + corruptedField)
        {

        }
    }
}

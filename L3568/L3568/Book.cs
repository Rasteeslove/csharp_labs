using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace L3568
{

    class Book : Literature, ISeries
    {
        public string ISBN;

        public enum Genre
        {
            Fantasy,
            [Display(Name = "Sci-Fi")]
            SciFi,
            Mystery,
            Thriller,
            Romance,
            Westerns,
            Dystopian,
            Contemporary,
            [Display(Name = "Historical Novel")]
            HistoricalNovel,

        };
        public readonly Genre Genr;

        private string series;
        
        private static string SetISBN()
        {
            // smh gets isbn for the book from databases and stuff
            // depending on name, author, language etc

            return "978-3-16-148410-0";
        }   

        #region ISeries

        public string GetSeries()
        {
            return series;
        }

        public void JoinSeries(string seriesName)
        {
            series = seriesName;
        }

        public void LeaveSeries()
        {
            series = "";
        }

        #endregion

        #region Constructors

        public Book(string name, Language language, Genre genre) 
            : this(name, DateTime.Now, language, false, genre, "") { }

        public Book(string name, DateTime released, Language language, Genre genre)
            : this(name, released, language, false, genre, "") { }

        public Book(string name, DateTime released, Language language, bool isFiction, Genre genre, string series) 
            : base(name, released, language, isFiction)
        {
            Genr = genre;
            this.series = series;
            ISBN = SetISBN();
        }

        #endregion

        public string Tag
        {
            get 
            { 
                return (IsFiction ? "Fiction " : "Non-fiction ") + GetDisplayValue(Genr) + " \"" + Name + "\" by " + Author.Name +
                    " released " + Released.ToString("MMMM dd, yyyy") + " in " + Lang.ToString() + ", ISBN: " + ISBN + ".";
            }
        }
        

    }
}

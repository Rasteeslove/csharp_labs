using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace L3568
{
    class Film : Media, ISeries
    {
        public enum Genre
        {
            Action,
            Comedy,
            Drama,
            Fantasy,
            Horror,
            Mystery,
            Romance,
            Thriller,
            Western,
            [Display(Name = "Historical Drama")]
            HistoricalDrama
        };

        public Genre Genr;
        private string series;

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

        public Film(string name, Genre genre) 
            : this(name, DateTime.Now, genre, "") { }

        public Film(string name, Genre genre, string series)
            : this(name, DateTime.Now, genre, series) { }

        public Film(string name, DateTime released, Genre genre)
            : this(name, released, genre, "") { }

        public Film(string name, DateTime released, Genre genre, string series) 
            : base(name, released) 
        {
            Genr = genre;
            this.series = series;
        }

        #endregion

        public string Tag
        {
            get
            {
                return GetDisplayValue(Genr) + " film \"" + Name + "\" by " + Author.Name +
                    " released " + Released.ToString("MMMM dd, yyyy") + (series ?? " as part of " + series + " series") + ".";
            }
        }

    }
}

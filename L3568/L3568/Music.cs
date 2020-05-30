using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace L3568
{
    class Music : Media
    {

        public enum Genre
        {
            Pop,
            Rock,
            [Display(Name = "Art rock")]
            ArtRock,
            Jazz,
            Punk,
            Funk,
            Indie,
            [Display(Name = "Hip-hop")]
            HipHop,
            Rap,
            Electronic,
            Classical,

        }

        public enum ReleaseType
        {
            LP,
            EP,
            [Display(Name = "Double album")]
            DoubleAlbum,
            Composition,
            Single,
            Mixtape,

        }

        private Genre Genr;
        private ReleaseType Type;

        #region Constructors

        public Music(string name, Genre genre)
            : this(name, DateTime.Now, genre, ReleaseType.Composition) { }

        public Music(string name, Genre genre, ReleaseType type)
            : this(name, DateTime.Now, genre, type) { }

        public Music(string name, DateTime released, Genre genre)
               : this(name, released, genre, ReleaseType.Composition) { }

        public Music(string name, DateTime released, Genre genre, ReleaseType type)
            : base(name, released)
        {
            Genr = genre;
            Type = type;
        }

        #endregion

        public string Tag
        {
            get
            {
                return GetDisplayValue(Genr) + " " + GetDisplayValue(Type) + " \"" + Name + "\" by " + Author.Name +
                    " released " + Released.ToString("MMMM dd, yyyy") + ".";
            }
        }

    }
}

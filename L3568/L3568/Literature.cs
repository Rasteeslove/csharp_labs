using System;
using System.Collections.Generic;
using System.Text;

namespace L3568
{

    abstract class Literature : Media
    {
        public enum Language
        {
            English,
            Belarusian,
            Russian,      

        }

        public Language Lang;
        public bool IsFiction;

        public Literature(string name, Language language) 
            : this(name, language, false) { }

        public Literature(string name, DateTime released, Language language) 
            : this(name, released, language, false) { }

        public Literature(string name, Language language, bool isFiction)
            : this(name, DateTime.Now, language, isFiction) { }

        public Literature(string name, DateTime released, Language language, bool isFiction) 
            : base(name, released)
        {
            Lang = language;
            IsFiction = isFiction;
        }
    }
}

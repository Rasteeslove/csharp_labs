using System;
using System.Collections.Generic;
using System.Text;

namespace L3568
{
    class Review : Literature
    {
        public string Verdict;

        #region Constructors

        public Review(string name, Language language, string verdict) 
            : this(name, DateTime.Now, language, verdict) { }

        public Review(string name, DateTime released, Language language, string verdict) 
            : base(name, released, language)
        {
            Verdict = verdict;
        }

        #endregion

    }
}

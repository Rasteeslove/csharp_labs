using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L3568
{

    abstract class Media : IComparable<Media>
    {
        public List<Review> Acclaim;
        public User Author;

        public string Name;
        public DateTime Released;

        #region Constructors

        public Media(string name)
            : this(name, DateTime.Now) { }

        public Media(string name, DateTime released)
        {
            Name = name;           
            Released = released;
            Acclaim = new List<Review>();
        }

        #endregion

        public int CompareTo(Media other)
        {
            return Released.CompareTo(other.Released);
        }

        public void GetReviewed(Review review)
        {
            Acclaim.Add(review);

            Console.WriteLine(Author.Name + " received notification : your work " + this.Name
                + " was reviewed \"" + review.Verdict + "\" by " + review.Author.Name + ".");
        }
    
        public void GetReleased()
        {
            Released = DateTime.Now;
        }

        protected static string GetDisplayValue(object value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

    }
}

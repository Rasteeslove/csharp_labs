using System;
using System.Collections.Generic;
using System.Text;

namespace L3568
{

    class ReleaseArgument : EventArgs
    {
        public Media Release { get; set; }
        public ReleaseArgument(Media release)
        {
            Release = release;
        }
    }

    class User
    {
        public string Name;
        public List<Media> Works;

        #region Constructors

        public User()
        {
            Name = "no name";
            Works = new List<Media>();
        }

        public User(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Name = "no name";
            }
            else
            { 
                Name = name;
            }
            Works = new List<Media>();
        }

        #endregion

        #region Instance methods and events

        public void Review(Media media, Review review)
        {
            MakeRelease(review);
            media.GetReviewed(review);
        }

        #region Publisher

        public void MakeRelease(Media media)
        {
            Works.Add(media);
            media.Author = this;

            ReleaseNotification(media);
        }

        private event EventHandler Release;

        private void ReleaseNotification(Media release)
        {
            Release?.Invoke(this, new ReleaseArgument(release));
        }

        #endregion

        #region Subscriber

        public void Subscribe(User pub)
        {
            pub.Release += getNotified;
        }

        public void UnSubscribe(User pub)
        {
            pub.Release -= getNotified;
        }

        private void getNotified(object sender, EventArgs e)
        {
            // could be done differently

            #region Getting release from EventArgs

            ReleaseArgument relArg = (e as ReleaseArgument);
            if (relArg == null)
            {
                throw new System.ArgumentException("EventArgs must be ReleaseArgument.", "e");
            }

            Media release = relArg.Release;           
            if (release == null)
            {
                throw new System.Exception("ReleaseArgument must contain Media");
            }

            #endregion

            #region Getting release's type and name

            string releaseType = (release is Book ? "Book" :
                                  release is Film ? "Film" :
                                  release is Music ? "Music" :
                                  release is Review ? "Review" : "");
            
            string releaseName = relArg.Release.Name;

            #endregion

            #region Getting user-sender's name

            string authorName;
            if (sender is User)
            {
                authorName = ((User)sender).Name;
            }
            else
            {
                throw new Exception("sender is not a user.");
            }

            #endregion

            Console.WriteLine("{0} received notification : new {1} \"{2}\" by {3}.", this.Name, releaseType, releaseName, authorName);
        }

        #endregion
       
        #endregion
    }
}

using System;

namespace L3568
{
    class Program
    {
        static void Main(string[] args)
        {

            User leo = new User("Leo Tolstoy");
            User steven = new User("Steven Spielberg");
            User beatles = new User("The Beatles");

            User dev = new User("Rastsislau Kayko");

            steven.Subscribe(leo);
            beatles.Subscribe(leo);

            dev.Subscribe(leo);
            dev.Subscribe(steven);
            dev.Subscribe(beatles);

            leo.MakeRelease(new Book("War and Peace", Literature.Language.Russian, Book.Genre.HistoricalNovel));
            Console.WriteLine((leo.Works[0] as Book).Tag);

            steven.MakeRelease(new Film("Schindler's List", new DateTime(1993, 11, 30), Film.Genre.HistoricalDrama));
            Console.WriteLine((steven.Works[0] as Film).Tag);

            beatles.MakeRelease(new Music("Sgt. Pepper's Lonely Hearts Club Band", Music.Genre.Rock, Music.ReleaseType.LP));
            Console.WriteLine((beatles.Works[0] as Music).Tag);

            User kendrick = new User("Kendrick Lamar");
            User anthonyFantano = new User("theneedledrop");
            User anotherUser = new User();

            dev.Subscribe(kendrick);
            dev.Subscribe(anthonyFantano);
            anotherUser.Subscribe(anthonyFantano);
            anthonyFantano.Subscribe(kendrick);

            kendrick.MakeRelease(new Music("DAMN.", Music.Genre.HipHop, Music.ReleaseType.LP));

            // not really literature tbh
            anthonyFantano.Review(kendrick.Works[0], new Review("DAMN. review", Literature.Language.English, "7/10"));

            dev.Review(anthonyFantano.Works[0], new Review("comment", Literature.Language.English, "7??!"));
            dev.UnSubscribe(anthonyFantano);

            User radiohead = new User("Radiohead");

            dev.Subscribe(radiohead);
            anthonyFantano.Subscribe(radiohead);

            radiohead.MakeRelease(new Music("A Moon Shaped Pool", Music.Genre.ArtRock, Music.ReleaseType.LP));
            anthonyFantano.Review(radiohead.Works[0], new Review("AMSP review", Literature.Language.English, "8/10"));

            dev.Review(anthonyFantano.Works[1], new Review("comment", Literature.Language.English, "alright i agree"));
            dev.Subscribe(anthonyFantano);
        }

    }
}

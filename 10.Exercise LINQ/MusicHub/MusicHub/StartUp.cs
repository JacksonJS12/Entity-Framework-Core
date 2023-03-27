using System.Globalization;
using System.Text;

namespace MusicHub;

using System;

using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        MusicHubDbContext context =
            new MusicHubDbContext();

        DbInitializer.ResetDatabase(context);

        //Test your solutions here
        string result = ExportSongsAboveDuration(context, 4);
        Console.WriteLine(result);

    }

    public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
    {
        StringBuilder sb = new StringBuilder();
        var albumsInfo = context.Albums
            //.Include(a => a.Producer)
            .Where(a => a.ProducerId.HasValue &&
                        a.ProducerId.Value == producerId)
            .ToArray() // This is because of bug in EF
            .OrderByDescending(a => a.Price)
            .Select(a => new
            {
                a.Name,
                ReleaseDate = a.ReleaseDate
                    .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ProducerName = a.Producer.Name,
                Songs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToArray(),
                AlbumPrice = a.Price.ToString("f2")
            })
            .ToArray();

        //-AlbumName: Devil's advocate
        //    -ReleaseDate: 07 / 21 / 2018
        //    -ProducerName: Evgeni Dimitrov
        //    -Songs:
        //---#1
        //    ---SongName: Numb
        //    ---Price: 13.99
        //    ---Writer: Kara - lynn Sharpous
        //    ---#2
        //    ---SongName: Ibuprofen
        //    ---Price: 26.50
        //    ---Writer: Stanford Daykin
        //    -AlbumPrice: 40.49

        foreach (var a in albumsInfo)
        {
            sb
                .AppendLine($"-AlbumName: {a.Name}")
                .AppendLine($"-ReleaseDate: {a.ReleaseDate}")
                .AppendLine($"-ProducerName: {a.ProducerName}")
                .AppendLine($"-Songs:");

            int songNumber = 1;
            foreach (var s in a.Songs)
            {
                sb
                    .AppendLine($"---#{songNumber}")
                    .AppendLine($"---SongName: {s.SongName}")
                    .AppendLine($"---Price: {s.Price}")
                    .AppendLine($"---Writer: {s.Writer}");
                songNumber++;
            }

            sb
                .AppendLine($"-AlbumPrice: {a.AlbumPrice}");
        }

        return sb.ToString().TrimEnd();
    }

    public static string ExportSongsAboveDuration(MusicHubDbContext context, int durationSeconds)
    {
        StringBuilder sb = new StringBuilder();

        var songsInfo = context.Songs
            .AsEnumerable()
            .Where(s => s.Duration.TotalSeconds > durationSeconds)
            .Select(s => new
            {
                s.Name,
                Performers = s.SongPerformers
                    .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                    .OrderBy(p => p)
                    .ToArray(),
                WriterName = s.Writer.Name,
                AlbumProducer = s.Album!.Producer!.Name,
                Duration = s.Duration
                    .ToString("c")
            })
            .OrderBy(s => s.Name)
            .ThenBy(s => s.WriterName)
            .ToArray();

        /*
            -Song #1
            ---SongName: Away
            ---Writer: Norina Renihan
            ---Performer: Lula Zuan
            ---AlbumProducer: Georgi Milkov
            ---Duration: 00:05:35
            -Song #2
            ---SongName: Bentasil
            ---Writer: Mik Jonathan
            ---Performer: Zabrina Amor
            ---AlbumProducer: Dobromir Slavchev
            ---Duration: 00:04:03
            …

         */
        int songsNumber = 1;
        foreach (var s in songsInfo)
        {
            sb
                .AppendLine($"-Song #{songsNumber}")
                .AppendLine($"---SongName: {s.Name}")
                .AppendLine($"---Writer: {s.WriterName}");
            foreach (var performer in s.Performers)
            {
                sb
                    .AppendLine($"---Performer: {performer}");
            }

            sb
                .AppendLine($"---AlbumProducer: {s.AlbumProducer}")
                .AppendLine($"---Duration: {s.Duration}");

            songsNumber++;
        }

        return sb.ToString().TrimEnd();
    }
}


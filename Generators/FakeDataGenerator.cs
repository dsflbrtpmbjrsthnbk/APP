using Bogus;
using MusicApp.Models;

namespace MusicApp.Generators;

public class FakeDataGenerator
{
    private readonly int _masterSeed;
    private readonly string _locale;
    private readonly double _avgLikes;

    private static readonly string[] AudioFiles = new[]
    {
        "/audio/sample1",
        "/audio/sample2",
        "/audio/sample3",
        "/audio/sample4"
    };

    public FakeDataGenerator(int masterSeed = 42, string locale = "en_US", double avgLikes = 1.2)
    {
        _masterSeed = masterSeed;
        _locale = locale;
        _avgLikes = Math.Clamp(avgLikes, 0, 10);
    }

    public List<Song> GenerateSongs(int startIndex = 0, int count = 10, int maxReviews = 5)
    {
        var songs = new List<Song>();
        var songsRandom = new Random(_masterSeed);

        for (int i = 0; i < count; i++)
        {
            int globalIndex = startIndex + i;
            var songSeed = songsRandom.Next();
            var song = GenerateSong(globalIndex + 1, songSeed, maxReviews);

            // Добавляем реальный аудио-файл
            song.AudioUrl = AudioFiles[globalIndex % AudioFiles.Length];

            songs.Add(song);
        }

        return songs;
    }

    private Song GenerateSong(int id, int seed, int maxReviews)
    {
        var faker = new Faker { Random = new Randomizer(seed) };

        var song = new Song
        {
            Id = id,
            Title = GenerateSongTitle(seed),
            Artist = faker.Name.FullName(),
            Album = faker.Commerce.ProductName(),
            DurationSeconds = faker.Random.Int(120, 420),
            ReleaseDate = faker.Date.Past(10),
            Genre = faker.PickRandom(new[]
            {
                "Rock", "Pop", "Jazz", "Classical", "Electronic", "Hip Hop", "Country"
            })
        };

        // Likes (используем твой существующий метод)
        song.Likes = LikesGenerator.GenerateLikes(seed);

        // Reviews
        var reviewsRandom = new Random(seed);
        var reviewCount = reviewsRandom.Next(maxReviews);
        song.Reviews = ReviewGenerator.GenerateReviews(seed, reviewCount);

        // Cover (используем твой существующий метод)
        song.CoverImageUrl = CoverGenerator.GenerateCoverUrl(seed);

        // Audio info (если нужно)
        song.Audio = AudioGenerator.GenerateAudio(seed);

        return song;
    }

    private string GenerateSongTitle(int seed)
    {
        var faker = new Faker { Random = new Randomizer(seed) };

        var templates = new[]
        {
            $"{faker.Lorem.Word()} {faker.Lorem.Word()}",
            $"{faker.Hacker.Verb()} {faker.Commerce.ProductAdjective()}",
            $"{faker.Commerce.Color()} {faker.Commerce.ProductMaterial()}",
            $"{faker.Hacker.Adjective()} {faker.Hacker.Noun()}",
            faker.Company.Bs()
        };

        return faker.PickRandom(templates);
    }
}
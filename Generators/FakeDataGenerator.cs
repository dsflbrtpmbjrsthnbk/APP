using Bogus;
using MusicApp.Models;

namespace MusicApp.Generators;

/// <summary>
/// Генератор фейковых песен с воспроизводимыми результатами и независимыми параметрами
/// </summary>
public class FakeDataGenerator
{
    private readonly long _userSeed;        // основной seed от пользователя (64-bit)
    private readonly string _locale;        // локаль Bogus (en_US, de_DE, uk_UA и т.д.)
    private readonly double _avgLikes;      // желаемое среднее количество лайков

    // Список реальных аудиофайлов из wwwroot/audio/
    private static readonly string[] AudioFiles = new[]
    {
        "/audio/sample1",
        "/audio/sample2",
        "/audio/sample3",
        "/audio/sample4"
    };

    public FakeDataGenerator(long userSeed = 1, string locale = "en_US", double avgLikes = 1.2)
    {
        _userSeed = userSeed;
        _locale = locale;
        _avgLikes = Math.Clamp(avgLikes, 0, 10);
    }

    /// <summary>
    /// Генерирует список песен начиная с указанного индекса
    /// </summary>
    public List<Song> GenerateSongs(int startIndex = 0, int count = 10, int maxReviews = 5)
    {
        var songs = new List<Song>();
        var faker = new Faker(_locale);

        for (int i = 0; i < count; i++)
        {
            int globalIndex = startIndex + i;

            // Основной seed для структуры песни (название, артист, альбом, жанр, обложка)
            long structureSeed = CombineSeed(_userSeed, globalIndex);
            var structureRng = new Random((int)(structureSeed & 0x7FFFFFFF));
            faker.Random = new Randomizer(structureRng.Next());

            var song = new Song
            {
                Id = globalIndex + 1,
                Title = GenerateSongTitle(faker, structureRng),
                Artist = faker.Name.FullName() ?? faker.Company.CompanyName(),
                Album = structureRng.Next(5) == 0 ? "Single" : faker.Lorem.Sentence(2..4).TrimEnd('.'),
                Genre = faker.Music.Genre(),
                DurationSeconds = structureRng.Next(120, 421),
                ReleaseDate = faker.Date.Past(10),
                CoverImageUrl = CoverGenerator.GenerateCoverUrl(structureSeed), // или base64
                AudioUrl = AudioFiles[globalIndex % AudioFiles.Length]           // циклически берём из твоих файлов
            };

            // Likes — отдельный независимый слой
            long likesSeed = CombineSeed(structureSeed, 1);
            song.Likes = LikesGenerator.GenerateLikes((int)(likesSeed & 0x7FFFFFFF), _avgLikes);

            // Reviews — ещё один независимый слой
            long reviewsSeed = CombineSeed(likesSeed, 2);
            var reviewsRng = new Random((int)(reviewsSeed & 0x7FFFFFFF));
            int reviewCount = reviewsRng.Next(0, maxReviews + 1);
            song.Reviews = ReviewGenerator.GenerateReviews((int)reviewsSeed, reviewCount);

            songs.Add(song);
        }

        return songs;
    }

    private string GenerateSongTitle(Faker faker, Random rng)
    {
        var templates = new[]
        {
            $"{faker.Music.SongName()}",
            $"{faker.Hacker.Verb()} {faker.Commerce.ProductAdjective()}",
            $"{faker.Commerce.Color()} {faker.Commerce.ProductMaterial()}",
            $"{faker.Hacker.Adjective()} {faker.Hacker.Noun()}",
            faker.Company.Bs()
        };

        return templates[rng.Next(templates.Length)];
    }

    // Хороший способ комбинировать seed + индекс (SplitMix64-like)
    private static long CombineSeed(long baseSeed, int offset)
    {
        ulong z = (ulong)baseSeed + (ulong)offset * 0x9E3779B97F4A7C15UL;
        z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
        z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
        return (long)(z ^ (z >> 31));
    }
}
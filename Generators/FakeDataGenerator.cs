using Bogus;
using MusicApp.Models;

namespace MusicApp.Generators;

/// <summary>
/// Base generator for creating fake music data with reproducible results
/// </summary>
public class FakeDataGenerator
{
    private readonly int _masterSeed;
    
    public FakeDataGenerator(int masterSeed = 42)
    {
        _masterSeed = masterSeed;
    }
    
    /// <summary>
    /// Generates a list of songs with seeded randomization
    /// </summary>
    public List<Song> GenerateSongs(int count, int reviewAmount = 4)
    {
        var songs = new List<Song>();
        var songsRandom = new Random(_masterSeed);
        
        for (int i = 0; i < count; i++)
        {
            var songSeed = songsRandom.Next();
            var song = GenerateSong(i + 1, songSeed, reviewAmount);
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
            Genre = faker.PickRandom(new[] { "Rock", "Pop", "Jazz", "Classical", "Electronic", "Hip Hop", "Country" })
        };
        
        // Generate likes
        song.Likes = LikesGenerator.GenerateLikes(seed);
        
        // Generate reviews with seeded randomization
        var reviewsRandom = new Random(seed);
        var reviewCount = reviewsRandom.Next(maxReviews);
        song.Reviews = ReviewGenerator.GenerateReviews(seed, reviewCount);
        
        // Generate cover
        song.CoverImageUrl = CoverGenerator.GenerateCoverUrl(seed);
        
        // Generate audio info
        song.Audio = AudioGenerator.GenerateAudio(seed);
        
        return song;
    }
    
    private string GenerateSongTitle(int seed)
    {
        var faker = new Faker { Random = new Randomizer(seed) };
        var templates = new[]
        {
            $"{faker.Hacker.Verb()} {faker.Commerce.ProductAdjective()}",
            $"{faker.Commerce.Color()} {faker.Commerce.ProductMaterial()}",
            $"{faker.Company.CatchPhraseAdjective()} {faker.Hacker.Noun()}",
            faker.Company.Bs()
        };
        
        return faker.PickRandom(templates);
    }
}

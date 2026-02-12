using Bogus;

namespace MusicApp.Generators;

/// <summary>
/// Generates song-specific content like lyrics snippets and descriptions
/// </summary>
public static class SongContentGenerator
{
    /// <summary>
    /// Generates a song description with seeded randomization
    /// </summary>
    public static string GenerateDescription(int seed)
    {
        var faker = new Faker { Random = new Randomizer(seed) };
        
        var templates = new[]
        {
            $"A {faker.Commerce.ProductAdjective()} {faker.Music.Genre()} track that captures the essence of {faker.Commerce.Color()} vibes.",
            $"This {faker.Music.Genre()} masterpiece features {faker.Commerce.ProductAdjective()} melodies and {faker.Commerce.ProductAdjective()} rhythms.",
            $"An {faker.Commerce.ProductAdjective()} blend of {faker.Music.Genre()} and modern sounds.",
            $"Experience the {faker.Commerce.ProductAdjective()} journey through {faker.Music.Genre()} with this incredible track."
        };
        
        return faker.PickRandom(templates);
    }
    
    /// <summary>
    /// Generates a lyrics snippet
    /// </summary>
    public static string GenerateLyricsSnippet(int seed)
    {
        var faker = new Faker { Random = new Randomizer(seed) };
        
        return string.Join("\n", Enumerable.Range(0, 4).Select(_ => faker.Lorem.Sentence()));
    }
}

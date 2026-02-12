using Bogus;

namespace MusicApp.Generators;

public static class ReviewGenerator
{
    private static readonly string[] ReviewTemplates = new[]
    {
        "Amazing track! Really loved it.",
        "Not bad, but could be better.",
        "This song is a masterpiece!",
        "Interesting sound, very unique.",
        "Perfect for my playlist.",
        "The lyrics are so meaningful.",
        "Great beat and rhythm.",
        "One of my favorite songs this year.",
        "Disappointed, expected more.",
        "Absolutely brilliant composition!"
    };
    
    /// <summary>
    /// Generates reviews with deterministic seeding
    /// </summary>
    public static List<string> GenerateReviews(int baseSeed, int count)
    {
        var reviews = new List<string>();
        var reviewsRandom = new Random(baseSeed);
        
        for (int i = 0; i < count; i++)
        {
            var reviewValue = reviewsRandom.Next();
            var faker = new Faker { Random = new Randomizer(reviewValue) };
            
            var review = faker.PickRandom(ReviewTemplates);
            var rating = faker.Random.Int(1, 5);
            
            reviews.Add($"[{rating}★] {review}");
        }
        
        return reviews;
    }
}

namespace MusicApp.Generators;

public static class CoverGenerator
{
    private static readonly string[] CoverColors = new[]
    {
        "FF6B6B", "4ECDC4", "45B7D1", "FFA07A", "98D8C8",
        "F7DC6F", "BB8FCE", "85C1E2", "F8B500", "52B788"
    };
    
    /// <summary>
    /// Generates a deterministic cover image URL based on seed
    /// </summary>
    public static string GenerateCoverUrl(int seed)
    {
        var random = new Random(seed);
        var colorIndex = random.Next(CoverColors.Length);
        var color = CoverColors[colorIndex];
        
        // Using placeholder image service
        return $"https://via.placeholder.com/300x300/{color}/FFFFFF?text=Album+Cover";
    }
}

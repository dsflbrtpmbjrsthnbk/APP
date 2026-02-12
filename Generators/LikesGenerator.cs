namespace MusicApp.Generators;

public static class LikesGenerator
{
    /// <summary>
    /// Generates a deterministic number of likes based on seed
    /// </summary>
    public static int GenerateLikes(int seed)
    {
        var random = new Random(seed);
        return random.Next(100, 10000);
    }
}

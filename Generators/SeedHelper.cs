namespace MusicApp.Generators;

/// <summary>
/// Helper class for managing Random seeds to ensure reproducible data generation
/// </summary>
public static class SeedHelper
{
    /// <summary>
    /// Gets a seeded Random generator based on a parent seed value
    /// </summary>
    public static Random GetSeededRandom(int baseSeed, int index)
    {
        var parentRandom = new Random(baseSeed);
        
        // Skip to the index position
        for (int i = 0; i < index; i++)
        {
            parentRandom.Next();
        }
        
        // Use the next value as seed for child random
        return new Random(parentRandom.Next());
    }
    
    /// <summary>
    /// Generates a deterministic seed from a parent Random
    /// </summary>
    public static int GetNextSeed(Random random)
    {
        return random.Next();
    }
}

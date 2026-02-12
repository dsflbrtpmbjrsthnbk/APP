using MusicApp.Models;

namespace MusicApp.Generators;

public static class AudioGenerator
{
    private static readonly int[] Bitrates = new[] { 128, 192, 256, 320 };
    private static readonly int[] SampleRates = new[] { 44100, 48000, 96000 };
    
    /// <summary>
    /// Generates deterministic audio track information based on seed
    /// </summary>
    public static AudioTrack GenerateAudio(int seed)
    {
        var random = new Random(seed);
        
        var bitrate = Bitrates[random.Next(Bitrates.Length)];
        var sampleRate = SampleRates[random.Next(SampleRates.Length)];
        var durationSeconds = random.Next(120, 420);
        
        // Calculate approximate file size (bitrate * duration / 8)
        var fileSizeBytes = (long)(bitrate * 1024 * durationSeconds / 8);
        
        return new AudioTrack
        {
            Format = "mp3",
            Bitrate = bitrate,
            SampleRate = sampleRate,
            FilePath = $"/audio/track_{seed}.mp3",
            FileSizeBytes = fileSizeBytes
        };
    }
}

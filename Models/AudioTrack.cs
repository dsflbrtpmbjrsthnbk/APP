namespace MusicApp.Models;

public class AudioTrack
{
    public string Format { get; set; } = "mp3";
    public int Bitrate { get; set; }
    public int SampleRate { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
}

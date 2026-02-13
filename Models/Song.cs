namespace MusicApp.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public int DurationSeconds { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<string> Reviews { get; set; } = new();
    public string CoverImageUrl { get; set; } = string.Empty;
    public AudioTrack? Audio { get; set; }
}
public class Song
{
    // ... все существующие свойства

    public string AudioUrl { get; set; } = string.Empty;  // ← новое
}
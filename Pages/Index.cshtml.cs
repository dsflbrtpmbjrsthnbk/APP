using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Generators;
using MusicApp.Models;

namespace MusicApp.Pages;

public class IndexModel : PageModel
{
    public List<Song> Songs { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public long Seed { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string Locale { get; set; } = "en_US";

    [BindProperty(SupportsGet = true)]
    public double AvgLikes { get; set; } = 1.2;

    [BindProperty(SupportsGet = true)]
    public string ViewMode { get; set; } = "gallery";

    [BindProperty(SupportsGet = true)]
    public new int Page { get; set; } = 0;  // ← добавлен new, чтобы убрать предупреждение CS0108

    private const int PageSize = 10;

    public void OnGet()
    {
        AvgLikes = Math.Clamp(AvgLikes, 0, 10);
        Page = Math.Max(0, Page);

        // Используем существующий FakeDataGenerator вместо ReproducibleSongGenerator
        var generator = new FakeDataGenerator((int)Seed, Locale, AvgLikes);
        Songs = generator.GenerateSongs(Page * PageSize, PageSize);
    }
}
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
    public int Page { get; set; } = 0;

    private const int PageSize = 12;

    public void OnGet()
    {
        AvgLikes = Math.Clamp(AvgLikes, 0, 10);
        Page = Math.Max(0, Page);

        var generator = new ReproducibleSongGenerator(Seed, Locale, AvgLikes);
        Songs = generator.Generate(Page * PageSize, PageSize);
    }
}
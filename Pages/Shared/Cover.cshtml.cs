using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MusicApp.Pages.Shared;

public class CoverModel : PageModel
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    
    public void OnGet(string imageUrl, string title)
    {
        ImageUrl = imageUrl ?? string.Empty;
        Title = title ?? "Album Cover";
    }
}

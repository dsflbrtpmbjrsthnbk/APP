using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Generators;
using MusicApp.Models;
using System;
using System.Collections.Generic;

namespace MusicApp.Pages;

public class IndexModel : PageModel
{
    public List<Song> Songs { get; set; } = new();
    public int SongCount { get; set; } = 3;
    public int ReviewAmount { get; set; } = 4;
    
    public void OnGet(int songCount = 3, int reviewAmount = 4)
    {
        SongCount = Math.Max(1, Math.Min(20, songCount));
        ReviewAmount = Math.Max(0, Math.Min(10, reviewAmount));
        
        var generator = new FakeDataGenerator(masterSeed: 42);
        Songs = generator.GenerateSongs(SongCount, ReviewAmount);
    }
}

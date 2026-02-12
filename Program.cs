var builder = WebApplication.CreateBuilder(args);

// Добавляем Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Конфигурация HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Страница ошибок
    app.UseHsts();                     // HTTP Strict Transport Security
}

app.UseHttpsRedirection();  // Перенаправление HTTP на HTTPS
app.UseStaticFiles();       // Подключение wwwroot (CSS, JS, изображения)

app.UseRouting();           // Маршрутизация
app.UseAuthorization();     // Авторизация

app.MapRazorPages();        // Подключение Razor Pages

app.Run();                  // Запуск приложения

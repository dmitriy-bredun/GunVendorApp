
var builder = WebApplication.CreateBuilder(args);   // Для создания объекта WebApplication необходим специальный класс-строитель - WebApplicationBuilder.

var app = builder.Build(); // Класс WebApplication применяется для управления обработкой запроса, установки маршрутов, получения сервисов и т.д. 

app.MapGet("/", () => "Hello World!");
app.Run();



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// ===== WebApplicationBuilder
//
// Кроме создания объекта WebApplication класс WebApplicationBuilder выполняет еще ряд задач, среди которых можно выделить следующие:
// - Установка конфигурации приложения
// - Добавление сервисов
// - Настройка логгирования в приложении
// - Установка окружения приложения
// - Конфигурация объектов IHostBuilder и IWebHostBuilder, которые применяются для создания хоста приложения

/**
 *  Второй вариаент создания приложения, когда необходимо подключить доп. опции.
 */
// WebApplicationOptions options = new() { Args = args };
// WebApplicationBuilder builderWithOptions = WebApplication.CreateBuilder(options);
// WebApplication appWithOptions = builderWithOptions.Build();